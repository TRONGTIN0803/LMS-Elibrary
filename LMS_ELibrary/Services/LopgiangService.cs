using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public class LopgiangService : ILopgiangService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public LopgiangService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Lopgiangday_Model>> getAllLopgiang()
        {
            try
            {
                var result = await _context.lopgiangday_Dbs.ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    await col.Reference(p => p.User).LoadAsync();
                    User_Db user = new User_Db();
                    user.UserFullname = item.User.UserFullname;
                    user.UserName = item.User.UserName;
                    user.Password = "***";
                    user.Email = item.User.Email;
                    user.Role = item.User.Role;
                    user.Avt = item.User.Avt;
                    user.Gioitinh = item.User.Gioitinh;
                    user.Sdt = item.User.Sdt;
                    user.Diachi = item.User.Diachi;

                    item.User = user;

                    await col.Reference(q=>q.Monhoc).LoadAsync();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang= item.Monhoc.Tinhtrang;
                    monhoc.TobomonId=item.Monhoc.TobomonId;

                    item.Monhoc = monhoc;
                }
                List<Lopgiangday_Model> listmonhoc = new List<Lopgiangday_Model>();
                listmonhoc = _mapper.Map<List<Lopgiangday_Model>>(result);

                return listmonhoc;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Lopgiangday_Model> detailLopgiangday(int id)
        {
            try
            {
                var result = await _context.lopgiangday_Dbs.SingleOrDefaultAsync(p => p.LopgiangdayID == id);
                if (result != null)
                {
                    //cap nhat lai thoi giang truy cap
                    result.Truycapgannhat = DateTime.Now;

                    var col = _context.Entry(result);
                    await col.Reference(p => p.User).LoadAsync();
                    User_Db user = new User_Db();
                    user.UserFullname = result.User.UserFullname;
                    user.UserName = result.User.UserName;
                    user.Password = "***";
                    user.Email = result.User.Email;
                    user.Role = result.User.Role;
                    user.Avt = result.User.Avt;
                    user.Gioitinh = result.User.Gioitinh;
                    user.Sdt = result.User.Sdt;
                    user.Diachi = result.User.Diachi;

                    result.User = user;

                    await col.Reference(q => q.Monhoc).LoadAsync();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.TenMonhoc = result.Monhoc.TenMonhoc;
                    monhoc.MaMonhoc = result.Monhoc.MaMonhoc;
                    monhoc.Mota = result.Monhoc.Mota;
                    monhoc.Tinhtrang = result.Monhoc.Tinhtrang;
                    monhoc.TobomonId = result.Monhoc.TobomonId;

                    result.Monhoc = monhoc;

                    Lopgiangday_Model lop = new Lopgiangday_Model();
                    lop = _mapper.Map<Lopgiangday_Model>(result);

                    return lop;
                }
                else
                {
                    throw new Exception("khong tim thay");
                }

                return null;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> addLopgiang(Lopgiangday_Model lopgiangday_Model)
        {
            try
            {
                if (lopgiangday_Model != null)
                {
                    Lopgiangday_Db lopgiangday_Db = new Lopgiangday_Db();
                    lopgiangday_Db.TenLop = lopgiangday_Model.TenLop;
                    lopgiangday_Db.UserID=lopgiangday_Model.UserID;
                    lopgiangday_Db.MonhocID=lopgiangday_Model.MonhocID;
                    lopgiangday_Db.Malop = lopgiangday_Model.Malop;
                    lopgiangday_Db.Thoigian = DateTime.Now; //ngay tao
                    lopgiangday_Db.Truycapgannhat = DateTime.Now;

                    await _context.lopgiangday_Dbs.AddAsync(lopgiangday_Db);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        KqJson kq=new KqJson();
                        kq.Status = true;
                        kq.Message = "them thanh cong";

                        return kq;
                    }
                    else
                    {
                        throw new Exception("Them thai bai");
                    }
                }
                else
                {
                    throw new Exception("Bad Request!");
                }
            } catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> editLopgiang(int lopgiang_id, Lopgiangday_Model lopgiang)
        {
            try
            {
                if(lopgiang_id!=null && lopgiang != null)
                {
                    var result = await _context.lopgiangday_Dbs.SingleOrDefaultAsync(p=>p.LopgiangdayID==lopgiang_id);
                    if (result != null)
                    {
                        result.TenLop = lopgiang.TenLop != null ? result.TenLop = lopgiang.TenLop : result.TenLop;
                        result.UserID = lopgiang.UserID != null ? result.UserID = lopgiang.UserID : result.UserID;
                        result.MonhocID = lopgiang.MonhocID != null ? result.MonhocID = lopgiang.MonhocID : result.MonhocID;
                        result.Malop = lopgiang.Malop != null ? result.Malop = lopgiang.Malop : result.Malop;
                        result.Thoigian = DateTime.Now;

                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            KqJson kq = new KqJson();
                            kq.Status = true;
                            kq.Message = "Cap nhat thanh cong";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("Cap nhat that bai");
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
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> deleteLopgiang(int lopgiang_id)
        {
            try
            {
                if (lopgiang_id != null)
                {
                    var result = await _context.lopgiangday_Dbs.SingleOrDefaultAsync(p=>p.LopgiangdayID==lopgiang_id);
                    if (result != null)
                    {
                        _context.lopgiangday_Dbs.Remove(result);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            KqJson kq = new KqJson();
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
                        throw new Exception("Not Fuond");
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

        public async Task<KqJson> xepLopChoHocVien(Hocvien_Lop_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.list_Hocvien_Id.Count > 0 && model.Lopgiang_Id!=null)
                {
                    int row = 0;
                    foreach (var hocvien_id in model.list_Hocvien_Id)
                    {
                        Hocvien_Lop_Db hv = new Hocvien_Lop_Db();
                        hv.Lopgiang_Id = model.Lopgiang_Id;
                        hv.User_Id = hocvien_id;

                        await _context.hocvien_Lop_Dbs.AddAsync(hv);
                    }
                    row = await _context.SaveChangesAsync();
                    if (row == model.list_Hocvien_Id.Count)
                    {
                        kq.Status = true;
                        kq.Message = "Thanh cong";

                        return kq;
                    }
                    else
                    {
                        throw new Exception("Co hoc vien khong the xep lop");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message=e.Message;

                return kq;
            }
        }

        public async Task<object> lopDangTheoHoc(int user_id)
        {
            try
            {
                if (user_id != null)
                {
                    var result = await (from lop in _context.lopgiangday_Dbs
                                        join hvlop in _context.hocvien_Lop_Dbs
                                      on lop.LopgiangdayID equals hvlop.Lopgiang_Id
                                        where hvlop.User_Id == user_id
                                        select lop).ToListAsync();
                    if (result.Count > 0)
                    {
                        List<Lopgiangday_Model> list_lop = new List<Lopgiangday_Model>();
                        list_lop = _mapper.Map<List<Lopgiangday_Model>>(result);
                        return list_lop;
                    }
                    else
                    {
                        throw new Exception("Khong tim thay lop");
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

        public async Task<KqJson> themHocvienVaolop(them_Hocvien_vao_Lop_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                int lopgiang_id =(int) model.Lopgiang_Id;
                if(model.Lopgiang_Id!=null && model.list_Hocvien_Id.Count > 0)
                {
                    List<Hocvien_Lop_Db> list_hocvien_lop = new List<Hocvien_Lop_Db>();
                    foreach(var hocvien_id in model.list_Hocvien_Id)
                    {
                        //hoc vien da co trong lop thi khong the them
                        var result = await (from _hvl in _context.hocvien_Lop_Dbs
                                            where _hvl.Lopgiang_Id == lopgiang_id && _hvl.User_Id == hocvien_id
                                            select _hvl).SingleOrDefaultAsync();
                        if (result == null)
                        {
                            Hocvien_Lop_Db hvl = new Hocvien_Lop_Db();
                            hvl.Lopgiang_Id = lopgiang_id;
                            hvl.User_Id = hocvien_id;
                            list_hocvien_lop.Add(hvl);
                        }
                        else
                        {
                            throw new Exception("Co hoc vien da o trong lop");
                        }
                        
                    }
                    await _context.hocvien_Lop_Dbs.AddRangeAsync(list_hocvien_lop);
                    int row = await _context.SaveChangesAsync();
                    if (row == model.list_Hocvien_Id.Count)
                    {
                        kq.Status = true;
                        kq.Message = "Thanh cong";
                        return kq;
                    }
                    else
                    {
                        throw new Exception("Co hoc vien da o trong lop");
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
