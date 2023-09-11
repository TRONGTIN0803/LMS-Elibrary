using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LMS_ELibrary.Services
{
    public interface IBaigiangService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getallbaigigang(int id);

        Task<IEnumerable<Tailieu_Baigiang_Model>> searchBaigiang(int id, string key);

        Task<IEnumerable<Tailieu_Baigiang_Model>> filterBaigiang(int id, int monId);

        Task<KqJson> addBaigiang(Tailieu_Baigiang_Db baigiang);

        Task<KqJson> changeMonhoc(int iddoc, int mon);
    }

    public class BaigiangService:IBaigiangService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public BaigiangService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> getallbaigigang(int id)
        {
            try
            {
                var result = await(from baigiang in _context.tailieu_Baigiang_Dbs where baigiang.Type == 1 && baigiang.UserId == id orderby baigiang.Sualancuoi descending select baigiang).ToListAsync();
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

                    await col.Reference(q => q.Monhoc).LoadAsync();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc = monhoc;

                }
                List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listbaigiang)
                {
                    item.Type = "Bai giang";
                    if (item.Status == "0")
                    {
                        item.Status = "Cho Duyet";
                    }
                    else if (item.Status == "1")
                    {
                        item.Status = "Da duyet";
                    }
                    if (item.User.Role == "0")
                    {
                        item.User.Role = "Quan ly";
                    }
                    else if (item.User.Role == "1")
                    {
                        item.User.Role = "Giao vien";
                    }
                    else
                    {
                        item.User.Role = "Hoc sinh";
                    }
                    if (item.User.Gioitinh == "True")
                    {
                        item.User.Gioitinh = "Nam";
                    }
                    else
                    {
                        item.User.Gioitinh = "Nu";
                    }
                }

                return listbaigiang;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> searchBaigiang(int id, string key)
        {
            try
            {
                var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
                                    where baigiang.Type == 1 && baigiang.UserId == id && baigiang.TenDoc.Contains(key)
                                    orderby baigiang.Sualancuoi descending
                                    select baigiang).ToListAsync();
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

                    await col.Reference(q => q.Monhoc).LoadAsync();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc = monhoc;

                }
                List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listbaigiang)
                {
                    item.Type = "Bai giang";
                    if (item.Status == "0")
                    {
                        item.Status = "Cho Duyet";
                    }
                    else if (item.Status == "1")
                    {
                        item.Status = "Da duyet";
                    }
                    if (item.User.Role == "0")
                    {
                        item.User.Role = "Quan ly";
                    }
                    else if (item.User.Role == "1")
                    {
                        item.User.Role = "Giao vien";
                    }
                    else
                    {
                        item.User.Role = "Hoc sinh";
                    }
                    if (item.User.Gioitinh == "True")
                    {
                        item.User.Gioitinh = "Nam";
                    }
                    else
                    {
                        item.User.Gioitinh = "Nu";
                    }
                }

                return listbaigiang;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> filterBaigiang(int id,int monId)
        {
            try
            {
                var result = await(from baigiang in _context.tailieu_Baigiang_Dbs
                                   where baigiang.Type == 1 && baigiang.UserId == id && baigiang.MonhocID==monId
                                   orderby baigiang.Sualancuoi descending
                                   select baigiang).ToListAsync();
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

                    await col.Reference(q => q.Monhoc).LoadAsync();
                    Monhoc_Db monhoc = new Monhoc_Db();
                    monhoc.TenMonhoc = item.Monhoc.TenMonhoc;
                    monhoc.MaMonhoc = item.Monhoc.MaMonhoc;
                    monhoc.Mota = item.Monhoc.Mota;
                    monhoc.Tinhtrang = item.Monhoc.Tinhtrang;
                    monhoc.TobomonId = item.Monhoc.TobomonId;

                    item.Monhoc = monhoc;

                }
                List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listbaigiang)
                {
                    item.Type = "Bai giang";
                    if (item.Status == "0")
                    {
                        item.Status = "Cho Duyet";
                    }
                    else if (item.Status == "1")
                    {
                        item.Status = "Da duyet";
                    }
                    if (item.User.Role == "0")
                    {
                        item.User.Role = "Quan ly";
                    }
                    else if (item.User.Role == "1")
                    {
                        item.User.Role = "Giao vien";
                    }
                    else
                    {
                        item.User.Role = "Hoc sinh";
                    }
                    if (item.User.Gioitinh == "True")
                    {
                        item.User.Gioitinh = "Nam";
                    }
                    else
                    {
                        item.User.Gioitinh = "Nu";
                    }
                }

                return listbaigiang;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        public async Task<KqJson> addBaigiang(Tailieu_Baigiang_Db baigiang)
        {
            try
            {
                Tailieu_Baigiang_Db addhbaigiang = new Tailieu_Baigiang_Db();
                addhbaigiang.ChudeID = baigiang.ChudeID;
                addhbaigiang.UserId = baigiang.UserId;
                addhbaigiang.TenDoc = baigiang.TenDoc;
                addhbaigiang.MonhocID = baigiang.MonhocID;
                addhbaigiang.Sualancuoi = DateTime.Now;
                addhbaigiang.Status = 0;
                addhbaigiang.Type = 1;
                await _context.tailieu_Baigiang_Dbs.AddAsync(addhbaigiang);
                int row = await _context.SaveChangesAsync();
                KqJson kq = new KqJson();
                if (row > 0)
                {
                    kq.Status = true;
                    kq.Message = "Them thanh cong";
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Them That bai";
                }

                return kq;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<KqJson> changeMonhoc(int iddoc, int mon)
        {
            try
            {
                KqJson kq = new KqJson();

                var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p=>p.DocId==iddoc);
                if (result != null)
                {
                    result.MonhocID = mon;
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Thanh cong";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "That bai";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay";
                }


                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
