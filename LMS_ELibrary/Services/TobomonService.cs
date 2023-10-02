using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public class TobomonService : ITobomonService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public TobomonService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> AddTobomon(Tobomon_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if(model.Quanly_Id>0 && model.TobomonName != "")
                {
                    var check_Quanly = await (from ql in _context.user_Dbs
                                              join role in _context.role_Dbs
                                              on ql.Role equals role.RoleId
                                              where ql.UserID == model.Quanly_Id &&
                                              role.Phanquyen == 1
                                              select ql).SingleOrDefaultAsync();
                    if (check_Quanly != null)
                    {
                        Tobomon_Db tbm = new Tobomon_Db();
                        tbm.TobomonName = model.TobomonName;
                        await _context.tobomon_Dbs.AddAsync(tbm);
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

        public async Task<KqJson> deleteTobomon(Tobomon_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Quanly_Id > 0 && model.Tobomon_Id > 0)
                {
                    var check_Quanly = await(from ql in _context.user_Dbs
                                             join role in _context.role_Dbs
                                             on ql.Role equals role.RoleId
                                             where ql.UserID == model.Quanly_Id &&
                                             role.Phanquyen == 1
                                             select ql).SingleOrDefaultAsync();
                    if (check_Quanly != null)
                    {
                        var tobomon = await _context.tobomon_Dbs.SingleOrDefaultAsync(p => p.TobomonId == model.Tobomon_Id);
                        if (tobomon != null)
                        {
                            _context.tobomon_Dbs.Remove(tobomon);
                            int row = await _context.SaveChangesAsync();
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
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> editTobomon(Tobomon_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Quanly_Id > 0 && model.TobomonName != "" && model.Tobomon_Id>0)
                {
                    var check_Quanly = await(from ql in _context.user_Dbs
                                             join role in _context.role_Dbs
                                             on ql.Role equals role.RoleId
                                             where ql.UserID == model.Quanly_Id &&
                                             role.Phanquyen == 1
                                             select ql).SingleOrDefaultAsync();
                    if (check_Quanly != null)
                    {
                        var tobomon = await _context.tobomon_Dbs.SingleOrDefaultAsync(p=>p.TobomonId==model.Tobomon_Id);
                        if (tobomon != null)
                        {
                            tobomon.TobomonName = model.TobomonName;
                            int row = await _context.SaveChangesAsync();
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
                        throw new Exception("Khong du quyen de thuc hien");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> Getall()
        {
            try
            {
                var result = await _context.tobomon_Dbs.ToListAsync();
                if (result.Count > 0)
                {
                    List<Tobomon_Model> listtbm = new List<Tobomon_Model>();
                    listtbm = _mapper.Map<List<Tobomon_Model>>(result);
                    return listtbm;
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> Xem_list_Tobomon_Giangvien(int giangvien_Id)
        {
            try
            {
                if (giangvien_Id > 0)
                {
                    var check_giangvien = await (from gv in _context.user_Dbs
                                                 join role in _context.role_Dbs
                                                 on gv.Role equals role.RoleId
                                                 where gv.UserID == giangvien_Id &&
                                                 role.Phanquyen == 2
                                                 select gv).SingleOrDefaultAsync();
                    if (check_giangvien != null)
                    {
                        var list_tbm = await (from tbm in _context.tobomon_Dbs
                                              join monhoc in _context.monhoc_Dbs
                                              on tbm.TobomonId equals monhoc.TobomonId
                                              where monhoc.UserId == giangvien_Id
                                              select tbm).ToListAsync();
                        if (list_tbm.Count > 0)
                        {
                            List<Tobomon_Model> listtbm = new List<Tobomon_Model>();
                            listtbm = _mapper.Map<List<Tobomon_Model>>(list_tbm);
                            return listtbm;
                        }
                        else
                        {
                            throw new Exception("Not Found");
                        }
                    }
                    else
                    {
                        throw new Exception("Khong phai giang vien");
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
    }
}
