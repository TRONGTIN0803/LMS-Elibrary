using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public interface ITailieuService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu(int id);
        Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu);

        Task<KqJson> addTailieu(Tailieu_Baigiang_Model tailieu);

        Task<KqJson> delTailieu(int id);
    }
    public class TailieuService : ITailieuService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public TailieuService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu(int id)
        {
            try
            {
                var result = await (from tailieu in _context.tailieu_Baigiang_Dbs where tailieu.Type == 0 && tailieu.UserId==id select tailieu).ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    col.Reference(p => p.User).Load();
                    User_Db user = new User_Db();
                    user.UserFullname = item.User.UserFullname;
                    user.UserName=item.User.UserName;
                    user.Password = "***";
                    user.Email = item.User.Email;
                    user.Role = item.User.Role;
                    user.Avt = item.User.Avt;
                    user.Gioitinh = item.User.Gioitinh;
                    user.Sdt = item.User.Sdt;
                    user.Diachi = item.User.Diachi;

                    item.User= user;

                }
                List<Tailieu_Baigiang_Model> listtialieu = new List<Tailieu_Baigiang_Model>();
                listtialieu = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listtialieu)
                {
                    item.Type = "Tai lieu";
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
                    if(item.User.Gioitinh == "True")
                    {
                        item.User.Gioitinh = "Nam";
                    }
                    else
                    {
                        item.User.Gioitinh = "Nu";
                    }
                }

                return listtialieu;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu)
        {
            try
            {
                var _tailieu = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p=>p.DocId==id && p.Type==0);
                KqJson kq = new KqJson();
                if (_tailieu != null)
                {
                    _tailieu.TenDoc = tailieu.TenDoc!=null ? _tailieu.TenDoc = tailieu.TenDoc : _tailieu.TenDoc;
                    _tailieu.UserId = tailieu.UserID != null ? _tailieu.UserId = tailieu.UserID : _tailieu.UserId;
                    _tailieu.MonhocID = tailieu.MonhocID != null ? _tailieu.MonhocID = tailieu.MonhocID : _tailieu.MonhocID;
                    _tailieu.ChudeID = tailieu.ChudeID != null ? _tailieu.ChudeID = tailieu.ChudeID : _tailieu.ChudeID;
                    _tailieu.Kichthuoc = tailieu.Kichthuoc != null ? _tailieu.Kichthuoc = tailieu.Kichthuoc : _tailieu.Kichthuoc;
                    _tailieu.Path = tailieu.Path != null ? _tailieu.Path = tailieu.Path : _tailieu.Path;
                    _tailieu.Status = tailieu.Status != null ? _tailieu.Status = int.Parse(tailieu.Status) : _tailieu.Status;
                    _tailieu.Type = tailieu.Type != null ? _tailieu.Type = int.Parse(tailieu.Type) : _tailieu.Type;
                    _tailieu.Sualancuoi = DateTime.Now;

                    int row_edit =await _context.SaveChangesAsync();
                    if (row_edit > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Sua tai lieu thanh cong";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Sua tai lieu khong thanh cong";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay tai lieu";
                }
                

                return kq;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson>addTailieu(Tailieu_Baigiang_Model tailieu)
        {
            try
            {
                Tailieu_Baigiang_Db _tailieu = new Tailieu_Baigiang_Db();
                KqJson kq = new KqJson();

                _tailieu.UserId = tailieu.UserID;
                _tailieu.TenDoc = tailieu.TenDoc;
                _tailieu.MonhocID=tailieu.MonhocID;
                _tailieu.ChudeID=tailieu.ChudeID;
                _tailieu.Sualancuoi = DateTime.Now;
                _tailieu.Status = 0; // status =0 -> dang duyet ; 1 -> da duyet
                _tailieu.Type = 0;  // type = 0 -> tailieu ; 1-> baigiang
                await _context.tailieu_Baigiang_Dbs.AddAsync(_tailieu);
                int row =await _context.SaveChangesAsync();
                if (row > 0)
                {
                    kq.Status = true;
                    kq.Message = "Them thanh cong";
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Them khong thanh cong";
                }

                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson>delTailieu(int id)
        {
            try
            {
                KqJson kq = new KqJson();

                var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p=>p.DocId==id && p.Type==0);
                if (result != null)
                {
                    _context.tailieu_Baigiang_Dbs.Remove(result);
                    int num_row = await _context.SaveChangesAsync();
                    if (num_row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Xoa thanh cong";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Xoa that bai";
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
