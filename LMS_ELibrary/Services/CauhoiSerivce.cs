using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    
    public class CauhoiService : ICauhoiService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public CauhoiService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> addCauhoi(Taocauhoi_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Nguoitao_Id > 0 && model.MonhocID > 0 && model.Cauhoi != "" & model.CauTrl != "" && model.Macauhoi != "")
                {
                    if (model.Dokho == 1 || model.Dokho == 2 || model.Dokho == 3)
                    {
                        QA_Db ch = new QA_Db();
                        ch.Cauhoi = model.Cauhoi;
                        ch.Cautrl = model.CauTrl;
                        ch.MonhocID = model.MonhocID;
                        ch.Nguoitao_Id = model.Nguoitao_Id;
                        ch.Dokho = model.Dokho;
                        ch.Macauhoi = model.Macauhoi;
                        ch.Lancuoisua = DateTime.Now;

                        _context.qA_Dbs.Add(ch);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Them thanh cong!";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Them that bai");
                        }
                    }
                    else
                    {
                        throw new Exception("Do kho khong phu hop");
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

        public async Task<object> chitietCauhoi(int idcauhoi)
        {
            try
            {
                QA_Model cauhoi_model = new QA_Model();
                var result = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == idcauhoi);
                if (result != null)
                {
                    var reff= _context.Entry(result);
                    reff.Reference(p => p.Monhoc).Load();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.MaMonhoc = result.Monhoc.MaMonhoc;
                    monhoc.TenMonhoc = result.Monhoc.TenMonhoc;
                    monhoc.Mota = result.Monhoc.Mota;
                    monhoc.Tinhtrang = result.Monhoc.Tinhtrang;
                    monhoc.TobomonId = result.Monhoc.TobomonId;

                    result.Monhoc = monhoc;

                    cauhoi_model = _mapper.Map<QA_Model>(result);

                }
                else
                {
                    throw new Exception("Not found");
                }
                

                return cauhoi_model;
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> editCauhoi(Edit_Cauhoi_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if(model.Cauhoi_Id>0 && model.User_Id > 0)
                {
                    //check User la Admin hoac Giang vien
                    var checkUser = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.User_Id &&
                                           role.Phanquyen == 1 || role.Phanquyen == 2
                                           select nd).FirstOrDefaultAsync();
                    if (checkUser != null)
                    {
                        var quyen = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.User_Id
                                           select role).FirstOrDefaultAsync();
                        var result = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == model.Cauhoi_Id);
                        if (result != null)
                        {
                            if (quyen.Phanquyen == 2)
                            {
                                if (result.Nguoitao_Id != model.User_Id)
                                {
                                    throw new Exception("Khong du quyen thuc hien");
                                }
                            }
                            result.Cauhoi = model.Cauhoi != null ? model.Cauhoi : result.Cauhoi;
                            result.Cautrl = model.Cautrl != null ? model.Cautrl : result.Cautrl;
                            result.Lancuoisua = DateTime.Now;
                            int row = await _context.SaveChangesAsync();
                            if (row > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Sua thanh cong!";
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
                        throw new Exception("Khong tim thay nguoi dung nay");
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

        public async Task<object> getAllCauhoi()
        {
            try
            {
                var result = await (from cauhoi in _context.qA_Dbs orderby cauhoi.Lancuoisua descending select cauhoi).ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    col.Reference(p => p.Monhoc).Load();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc=monhoc;
                    
                }
                List<QA_Model> listcauhoi = new List<QA_Model>();
                listcauhoi = _mapper.Map<List<QA_Model>>(result);


                return listcauhoi;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<object> xemCauhoitheoMon(int idmon)
        {
            try
            {
                if (idmon > 0)
                {
                    var result = await (from cauhoi in _context.qA_Dbs where cauhoi.MonhocID == idmon orderby cauhoi.Lancuoisua descending select cauhoi).ToListAsync();
                    if (result.Count > 0)
                    {
                        //foreach (var item in result)
                        //{
                        //    var col = _context.Entry(item);
                        //    col.Reference(p => p.Monhoc).Load();
                        //    Monhoc_Db monhoc = new Monhoc_Db();
                        //    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                        //    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                        //    monhoc.Mota = item.Monhoc.Mota;
                        //    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                        //    monhoc.TobomonId = item.Monhoc.TobomonId;

                        //    item.Monhoc = monhoc;

                        //}
                        List<QA_Model> listcauhoi = new List<QA_Model>();
                        listcauhoi = _mapper.Map<List<QA_Model>>(result);


                        return listcauhoi;
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
            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> xemCauHoitheoToMon(int idtomon)
        {
            try
            {
                if (idtomon > 0)
                {
                    var result = await (from cauhoi in _context.qA_Dbs
                                        join monhoc in _context.monhoc_Dbs
                                        on cauhoi.MonhocID equals monhoc.MonhocID
                                        where monhoc.TobomonId == idtomon
                                        orderby cauhoi.Lancuoisua descending
                                        select cauhoi).ToListAsync();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            var col = _context.Entry(item);
                            col.Reference(p => p.Monhoc).Load();
                            Monhoc_Db monhoc = new Monhoc_Db();
                            monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                            monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                            monhoc.Mota = item.Monhoc.Mota;
                            monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                            monhoc.TobomonId = item.Monhoc.TobomonId;

                            item.Monhoc = monhoc;

                        }
                        List<QA_Model> listcauhoi = new List<QA_Model>();
                        listcauhoi = _mapper.Map<List<QA_Model>>(result);


                        return listcauhoi;
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
                
            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> xoaCauhoi(Delete_Entity_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
               if(model.EntityId>0 && model.User_Id > 0)
                {
                    //check User la Admin hoac Giang vien
                    var checkUser = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.User_Id &&
                                           role.Phanquyen == 1 || role.Phanquyen == 2
                                           select nd).FirstOrDefaultAsync();
                    if (checkUser != null)
                    {
                        var quyen = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.User_Id
                                           select role).FirstOrDefaultAsync();
                        var result = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == model.EntityId);
                        if (result != null)
                        {
                            if (quyen.Phanquyen == 2)
                            {
                                if (result.Nguoitao_Id != model.User_Id)
                                {
                                    throw new Exception("Khong du quyen thuc hien");
                                }
                            }
                            _context.qA_Dbs.Remove(result);
                            int row = await _context.SaveChangesAsync();
                            if (row > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Xoa thanh cong!";
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
                        throw new Exception("Khong tim thay nguoi dung nay");
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
    }
}
