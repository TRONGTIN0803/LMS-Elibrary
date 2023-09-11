using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public interface ILopgiangService
    {
        Task<IEnumerable<Lopgiangday_Model>> getAllLopgiang();

        Task<Lopgiangday_Model> detailLopgiangday(int id);
    }
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
    }
}
