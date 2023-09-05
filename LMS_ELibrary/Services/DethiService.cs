using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public class DethiService:IDethiService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public DethiService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dethi_Model>> filterDethitheoMon(int id)
        {
            try
            {
                var result = await(from dethi in _context.dethi_Dbs where dethi.MonhocID==id orderby dethi.Ngaytao descending select dethi).ToListAsync();
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
                List<Dethi_Model> listdethi = new List<Dethi_Model>();
                listdethi = _mapper.Map<List<Dethi_Model>>(result);
                foreach (Dethi_Model dethi in listdethi)
                {
                    if (dethi.Status == "0")
                    {
                        dethi.Status = "Cho Duyet";
                    }
                    else if (dethi.Status == "1")
                    {
                        dethi.Status = "Da Duyet";
                    }
                }

                return listdethi;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Dethi_Model>> filterDethitheoTohomon(int id)
        {
            /*
             from m in _context.monhoc_Dbs
                                        join l in _context.lopgiangday_Dbs on
                                     m.MonhocID equals l.MonhocID
                                        orderby l.Truycapgannhat descending
                                        select m*/
            try
            {
                var result = await(from dethi in _context.dethi_Dbs join mon in _context.monhoc_Dbs
                                   on dethi.MonhocID equals mon.MonhocID
                                   where mon.TobomonId==id
                                   select dethi).ToListAsync();
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
                List<Dethi_Model> listdethi = new List<Dethi_Model>();
                listdethi = _mapper.Map<List<Dethi_Model>>(result);
                foreach (Dethi_Model dethi in listdethi)
                {
                    if (dethi.Status == "0")
                    {
                        dethi.Status = "Cho Duyet";
                    }
                    else if (dethi.Status == "1")
                    {
                        dethi.Status = "Da Duyet";
                    }
                }

                return listdethi;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Dethi_Model>> getalldethi()
        {
            try
            {
                var result=await (from dethi in _context.dethi_Dbs orderby dethi.Ngaytao descending select dethi).ToListAsync();
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
                List<Dethi_Model> listdethi = new List<Dethi_Model>();
                listdethi = _mapper.Map<List<Dethi_Model>>(result);
                foreach(Dethi_Model dethi in listdethi)
                {
                    if (dethi.Status == "0")
                    {
                        dethi.Status = "Cho Duyet";
                    }else if (dethi.Status == "1")
                    {
                        dethi.Status = "Da Duyet";
                    }
                }

                return listdethi;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
