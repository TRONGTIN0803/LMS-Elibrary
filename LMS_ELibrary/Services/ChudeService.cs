using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LMS_ELibrary.Services
{

    public class ChudeService : IChudeService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public ChudeService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> editChude(Edit_Baigiang_Tainguyen_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if(model.Id_Edit>0 && model.Name != "")
                {
                    var cd = await _context.chude_Dbs.SingleOrDefaultAsync(p => p.ChudeID == model.Id_Edit);
                    if (cd != null)
                    {
                        cd.Tenchude = model.Name;
                        int row=await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Sua thanh cong";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Sua that bai");
                        }
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
           
        }

        public async Task<KqJson>addChude(Chude_Model chude)
        {
            KqJson kq = new KqJson();
            try
            {
                if(chude.Tenchude!="" && chude.Monhoc_Id > 0)
                {
                    var checkMonhoc = await _context.monhoc_Dbs.SingleOrDefaultAsync(p=>p.MonhocID==chude.Monhoc_Id);
                    if (checkMonhoc != null)
                    {
                        Chude_Db _chude = new Chude_Db();
                        _chude.Tenchude = chude.Tenchude;
                        _chude.Monhoc_Id = chude.Monhoc_Id;
                        await _context.chude_Dbs.AddAsync(_chude);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Them thanh cong";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Them that bai");
                        }
                    }
                    else
                    {
                        throw new Exception("Khong tim thay mon hoc nay");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
            
        }

        public async Task<object> getAllchude(int monhoc_id)
        {
            try
            {
                if (monhoc_id > 0)
                {
                    var result = await (from cd in _context.chude_Dbs where cd.Monhoc_Id == monhoc_id select cd).ToListAsync();
                    if (result.Count > 0)
                    {
                        List<Chude_Model> list_chude = new List<Chude_Model>();

                        foreach (var chude in result)
                        {
                            var col = _context.Entry(chude);
                            col.Collection(n => n.ListTailieu_Baigiang).Load();
                            List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                            if (chude.ListTailieu_Baigiang != null)
                            {
                                List<Tailieu_Baigiang_Db> listtailieu = new List<Tailieu_Baigiang_Db>();
                                foreach (var e in chude.ListTailieu_Baigiang)
                                {
                                    Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                                    tailieu.UserId = e.UserId;
                                    tailieu.TenDoc = e.TenDoc;
                                    tailieu.Status = e.Status;
                                    tailieu.MonhocID = e.MonhocID;
                                    tailieu.Sualancuoi = e.Sualancuoi;
                                    tailieu.ChudeID = e.ChudeID;
                                    tailieu.Mota = e.Mota;
                                    tailieu.Ghichu = e.Ghichu;
                                    tailieu.NgayDuyet = e.NgayDuyet;
                                    tailieu.Nguoiduyet = e.Nguoiduyet;
                                    tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
                                    list.Add(tailieu);
                                }
                                chude.ListTailieu_Baigiang = listtailieu;
                            }
                        }
                        list_chude = _mapper.Map<List<Chude_Model>>(result);
                        foreach (Chude_Model model in list_chude)
                        {
                            
                            foreach (var x1 in model.ListTailieu_Baigiang)
                            {
                                if (x1.Status == "3")
                                {
                                    x1.Status = "Da duyet";
                                    
                                }
                                else if (x1.Status == "2")
                                {
                                    x1.Status = "Cho duyet";
                                }
                                else if (x1.Status == "1")
                                {
                                    x1.Status = "Luu nhap";
                                }
                                else if (x1.Status == "4")
                                {
                                    x1.Status = "Da huy";
                                }
                            }
                        }

                        return list_chude;
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> deletetChude(Delete_Entity_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if(model.EntityId>0 && model.User_Id > 0)
                {
                    //check User la Admin || Giang vien
                    var checkUser = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.EntityId &&
                                           role.Phanquyen == 1 || role.Phanquyen==2
                                           select nd).FirstOrDefaultAsync();
                    if (checkUser != null)
                    {
                        var result = await _context.chude_Dbs.SingleOrDefaultAsync(p => p.ChudeID == model.EntityId);

                        if (result != null)
                        {
                            _context.Remove(result);
                            int row= await _context.SaveChangesAsync();
                            if (row > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Xoa thanh cong";
                                return kq;
                            }
                            else
                            {
                                throw new Exception("Xoa that bai");
                            }
                        }
                        else
                        {
                            throw new Exception("Not Found");
                        }
                    }
                    else
                    {
                        throw new Exception("Khong du quyen de thuc hien");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> Xem_Chude_Monhoc(int monhoc_id)
        {
            try
            {
                if (monhoc_id > 0)
                {
                    var result = await (from cd in _context.chude_Dbs 
                                        where cd.Monhoc_Id == monhoc_id 
                                        select cd).ToListAsync();
                    if (result.Count > 0)
                    {
                        List<Chude_Model> list_chude = new List<Chude_Model>();
                        list_chude = _mapper.Map<List<Chude_Model>>(result);
                        return list_chude;
                    }
                    else
                    {
                        throw new Exception("Mon hoc nay khong co chu de nao");
                    }
                }
                else
                {
                    throw new Exception("Khong co mon hoc nay");
                }
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }
    }
}
