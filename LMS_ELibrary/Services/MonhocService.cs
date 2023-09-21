using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace LMS_ELibrary.Services
{
    public class MonhocService : IMonhocService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public MonhocService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        //Status = -1 => luu nhap ; 0 => cho duyet ; 1 => da duyet ; 2 => huy yeu cau
        public async Task<IEnumerable<Monhoc_Model>> getAllMonhoc(int user_id)
        {
            var monhoc = await (from x in _context.monhoc_Dbs orderby x.TenMonhoc ascending select x).ToListAsync();
            List<Monhoc_Model> modelmh = new List<Monhoc_Model>();

            foreach (var mon in monhoc)
            {

                var col = _context.Entry(mon);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                col.Collection(n => n.ListLopgiangday).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                mon.ListTailieu_Baigiang.ForEach(e =>
                {

                    Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                    tailieu.UserId = e.UserId;
                    tailieu.TenDoc = e.TenDoc;
                    tailieu.Status = e.Status;
                    tailieu.MonhocID = e.MonhocID;
                    tailieu.Sualancuoi = e.Sualancuoi;
                    tailieu.Path = e.Path;
                    tailieu.Kichthuoc = e.Kichthuoc;
                    tailieu.ChudeID = e.ChudeID;
                    list.Add(tailieu);



                });
                mon.ListLopgiangday.ForEach(e =>
                {
                    Lopgiangday_Db lopgiangday = new Lopgiangday_Db();
                    lopgiangday.TenLop = e.TenLop;
                    lopgiangday.Thoigian = e.Thoigian;
                    lopgiangday.Truycapgannhat = e.Truycapgannhat;
                    lopgiangday.UserID = e.UserID;
                    lopgiangday.MonhocID = e.MonhocID;
                    listlop.Add(lopgiangday);
                });
                mon.ListTailieu_Baigiang = list;
                mon.ListLopgiangday = listlop;
            }
            modelmh = _mapper.Map<List<Monhoc_Model>>(monhoc);

            foreach (Monhoc_Model model in modelmh)
            {
                var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.UserId);
                model.Giangvien = gv.UserFullname;
                int tongtailieu = model.ListTailieu_Baigiang.Count;
                int tailieudaduyet = 0;

                foreach (var x1 in model.ListTailieu_Baigiang)
                {
                    if (x1.Status == "1")
                    {
                        x1.Status = "Da duyet";
                        tailieudaduyet++;
                    }
                    else if (x1.Status == "0")
                    {
                        x1.Status = "Cho duyet";
                    }

                }
                model.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;
                var check = await (from c in _context.monhocYeuthich_Dbs
                                   where c.MonhocId == model.MonhocID && c.UserId == user_id
                                   select c).ToListAsync();
                if (check.Count > 0)
                {
                    model.TrangthaiYeuthich = "Yeu thich";
                }
                else
                {
                    model.TrangthaiYeuthich = null;
                }
            }


            return modelmh;

        }

        public async Task<Monhoc_Model> searchMonhoc(string key)
        {
            try
            {
                var monhoc = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MaMonhoc == key || p.TenMonhoc == key);

                var col = _context.Entry(monhoc);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                col.Collection(n => n.ListLopgiangday).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                monhoc.ListTailieu_Baigiang.ForEach(e =>
                {
                    Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                    tailieu.UserId = e.UserId;
                    tailieu.TenDoc = e.TenDoc;
                    tailieu.Status = e.Status;
                    tailieu.MonhocID = e.MonhocID;
                    tailieu.Sualancuoi = e.Sualancuoi;
                    tailieu.Path = e.Path;
                    tailieu.Kichthuoc = e.Kichthuoc;
                    tailieu.ChudeID = e.ChudeID;
                    list.Add(tailieu);
                });
                monhoc.ListLopgiangday.ForEach(e =>
                {
                    Lopgiangday_Db lopgiangday = new Lopgiangday_Db();
                    lopgiangday.TenLop = e.TenLop;
                    lopgiangday.Thoigian = e.Thoigian;
                    lopgiangday.Truycapgannhat = e.Truycapgannhat;
                    lopgiangday.UserID = e.UserID;
                    lopgiangday.MonhocID = e.MonhocID;
                    listlop.Add(lopgiangday);
                });
                monhoc.ListTailieu_Baigiang = list;
                monhoc.ListLopgiangday = listlop;
                Monhoc_Model mon = new Monhoc_Model();
                mon = _mapper.Map<Monhoc_Model>(monhoc);
                foreach (var x1 in mon.ListTailieu_Baigiang)
                {
                    if (x1.Status == "0")
                    {
                        x1.Status = "Chua duyet";
                    }
                    else if (x1.Status == "1")
                    {
                        x1.Status = "Da duyet";
                    }
                }
                return mon;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Monhoc_Model>> locMonhoc(int option, int user_id)
        {
            //0=>ten ; 1=> truycapgannhat
            try
            {
                List<Monhoc_Db> listmonhoc = new List<Monhoc_Db>();
                List<Monhoc_Model> listmh = new List<Monhoc_Model>();

                // var result = await _context.monhoc_Dbs.Join(_context.lopgiangday_Dbs,m=>m.MonhocID,lop=>lop.MonhocID)
                if (option == 1)
                {
                    listmonhoc = await (from m in _context.monhoc_Dbs
                                        join l in _context.lopgiangday_Dbs on
                                     m.MonhocID equals l.MonhocID
                                        orderby l.Truycapgannhat descending
                                        select m).ToListAsync();
                }
                else if (option == 0)
                {
                    listmonhoc = await (from m in _context.monhoc_Dbs
                                        join l in _context.lopgiangday_Dbs on
                                     m.MonhocID equals l.MonhocID
                                        orderby m.TenMonhoc ascending
                                        select m).ToListAsync();
                }
                foreach (var mon in listmonhoc)
                {
                    var col = _context.Entry(mon);
                    col.Collection(m => m.ListTailieu_Baigiang).Load();
                    col.Collection(n => n.ListLopgiangday).Load();
                    List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                    List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                    mon.ListTailieu_Baigiang.ForEach(e =>
                    {
                        Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                        tailieu.UserId = e.UserId;
                        tailieu.TenDoc = e.TenDoc;
                        tailieu.Status = e.Status;
                        tailieu.MonhocID = e.MonhocID;
                        tailieu.Sualancuoi = e.Sualancuoi;
                        tailieu.Path = e.Path;
                        tailieu.Kichthuoc = e.Kichthuoc;
                        tailieu.ChudeID = e.ChudeID;
                        list.Add(tailieu);
                    });
                    mon.ListLopgiangday.ForEach(e =>
                    {
                        Lopgiangday_Db lopgiangday = new Lopgiangday_Db();
                        lopgiangday.TenLop = e.TenLop;
                        lopgiangday.Thoigian = e.Thoigian;
                        lopgiangday.Truycapgannhat = e.Truycapgannhat;
                        lopgiangday.UserID = e.UserID;
                        lopgiangday.MonhocID = e.MonhocID;
                        listlop.Add(lopgiangday);
                    });
                    mon.ListTailieu_Baigiang = list;
                    mon.ListLopgiangday = listlop;
                }
                listmh = _mapper.Map<List<Monhoc_Model>>(listmonhoc);

                foreach (var item in listmh)
                {
                    int tongtailieu = item.ListTailieu_Baigiang.Count;
                    int tailieudaduyet = 0;
                    foreach (var x1 in item.ListTailieu_Baigiang)
                    {
                        if (x1.Status == "0")
                        {
                            x1.Status = "Chua duyet";
                        }
                        else if (x1.Status == "1")
                        {
                            x1.Status = "Da duyet";
                            tailieudaduyet++;
                        }
                    }
                    var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == item.UserId);
                    item.Giangvien = gv.UserFullname;

                    item.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;

                    var check = await (from c in _context.monhocYeuthich_Dbs
                                       where c.MonhocId == item.MonhocID && c.UserId == user_id
                                       select c).SingleOrDefaultAsync();
                    if (check != null)
                    {
                        item.TrangthaiYeuthich = "Yeu thich";
                    }
                    else
                    {
                        item.TrangthaiYeuthich = null;
                    }
                }


                return listmh;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public async Task<Monhoc_Model> chitietMonhoc(int id, int user_id)
        {
            try
            {
                var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == id);
                var col = _context.Entry(result);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                col.Collection(n => n.ListLopgiangday).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                result.ListTailieu_Baigiang.ForEach(e =>
                {
                    Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                    tailieu.UserId = e.UserId;
                    tailieu.TenDoc = e.TenDoc;
                    tailieu.Status = e.Status;
                    tailieu.MonhocID = e.MonhocID;
                    tailieu.Sualancuoi = e.Sualancuoi;
                    tailieu.Path = e.Path;
                    tailieu.Kichthuoc = e.Kichthuoc;
                    tailieu.ChudeID = e.ChudeID;
                    list.Add(tailieu);
                });
                result.ListLopgiangday.ForEach(e =>
                {
                    Lopgiangday_Db lopgiangday = new Lopgiangday_Db();
                    lopgiangday.TenLop = e.TenLop;
                    lopgiangday.Thoigian = e.Thoigian;
                    lopgiangday.Truycapgannhat = e.Truycapgannhat;
                    lopgiangday.UserID = e.UserID;
                    lopgiangday.MonhocID = e.MonhocID;
                    listlop.Add(lopgiangday);
                });
                result.ListTailieu_Baigiang = list;
                result.ListLopgiangday = listlop;
                Monhoc_Model mon = new Monhoc_Model();
                mon = _mapper.Map<Monhoc_Model>(result);
                int tongtailieu = mon.ListTailieu_Baigiang.Count;
                int tailieudaduyet = 0;
                foreach (var x1 in mon.ListTailieu_Baigiang)
                {

                    if (x1.Status == "0")
                    {
                        x1.Status = "Chua duyet";
                    }
                    else if (x1.Status == "1")
                    {
                        x1.Status = "Da duyet";
                        tailieudaduyet++;
                    }
                }

                var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == result.UserId);
                mon.Giangvien = gv.UserFullname;

                mon.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;


                var check = await (from c in _context.monhocYeuthich_Dbs
                                   where c.MonhocId == mon.MonhocID && c.UserId == user_id
                                   select c).SingleOrDefaultAsync();
                if (check != null)
                {
                    mon.TrangthaiYeuthich = "Yeu thich";
                }
                else
                {
                    mon.TrangthaiYeuthich = null;
                }
                return mon;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> editMonhoc(int monhoc_id, Monhoc_Model monhoc)
        {
            try
            {
                KqJson kq = new KqJson();
                if (monhoc_id != null && monhoc != null)
                {
                    var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == monhoc_id);
                    if (result != null)
                    {
                        result.TenMonhoc = monhoc.TenMonhoc != null ? result.TenMonhoc = monhoc.TenMonhoc : result.TenMonhoc;
                        result.MaMonhoc = monhoc.MaMonhoc != null ? result.MaMonhoc = monhoc.MaMonhoc : result.MaMonhoc;
                        result.Mota = monhoc.Mota != null ? result.Mota = monhoc.Mota : result.Mota;
                        result.TobomonId = monhoc.TobomonId != null ? result.TobomonId = monhoc.TobomonId : result.TobomonId;

                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Thay doi thanh cong";
                        }
                        else
                        {
                            throw new Exception("Thay doi khong thanh cong");
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


                return kq;
            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        //Status = 0 => cho duyet (gui yeu cau) ; 2 => huy yeu cau
        public async Task<KqJson> setTrangthai(List<int> monhoc_id, int status)
        {
            try
            {
                if (monhoc_id.Count > 0 && status != null)
                {
                    if (status > 2 || status < -1)
                    {
                        throw new Exception("Request Params not Suitable!");
                    }
                    KqJson kq = new KqJson();
                    foreach (int monoc in monhoc_id)
                    {
                        var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == monoc);
                        if (result != null)
                        {
                            if (status == 0)
                            {
                                if (result.Tinhtrang == -1)
                                {
                                    result.Tinhtrang = 0;
                                }
                                else
                                {
                                    throw new Exception("Tinh trang khong phu hop de gui yeu cau");
                                }
                            }
                            else if (status == 2)
                            {
                                if (result.Tinhtrang == 0)
                                {
                                    result.Tinhtrang = 2;
                                }
                                else
                                {
                                    throw new Exception("Đang có phần tử không ở trạng thái chờ....");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Not Found");
                        }
                    }
                    int row = await _context.SaveChangesAsync();
                    if (row == monhoc_id.Count)
                    {
                        kq.Status = true;
                        kq.Message = "Cap nhat trang thai thanh cong";
                    }
                    else
                    {
                        throw new Exception("Cap nhat trang thai that bai");
                    }

                    return kq;
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

        public async Task<KqJson> addMonhoc(Monhoc_Model monhoc)
        {
            try
            {
                if (monhoc != null)
                {
                    KqJson kq = new KqJson();
                    Monhoc_Db mh = new Monhoc_Db();
                    mh.TenMonhoc = monhoc.TenMonhoc;
                    mh.MaMonhoc = monhoc.MaMonhoc;
                    mh.Mota = monhoc.Mota;
                    mh.Tinhtrang = -1;
                    mh.TobomonId = monhoc.TobomonId;
                    mh.UserId = monhoc.UserId;

                    _context.monhoc_Dbs.Add(mh);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Them thanh cong";
                    }
                    else
                    {
                        throw new Exception("Them thai bai");
                    }


                    return kq;
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

        public async Task<object> locMonhoc_theo_Tinhtrang(int status, int giangvien_id)
        {
            try
            {
                if (status != null && giangvien_id != null)
                {
                    var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == giangvien_id);
                    if (gv != null)
                    {
                        if (gv.Role == 0 || gv.Role == 1)
                        {
                            if (status < -1 || status > 2)
                            {
                                throw new Exception("Request Params not Suitable!");
                            }
                            else
                            {
                                List<Monhoc_Model> monhoc = new List<Monhoc_Model>();
                                var result = await (from mh in _context.monhoc_Dbs where mh.Tinhtrang == status select mh).ToListAsync();
                                if (result.Count > 0)
                                {
                                    foreach (var tm in result)
                                    {
                                        var col = _context.Entry(tm);
                                        await col.Reference(p => p.Tobomon).LoadAsync();

                                        Tobomon_Db _tbm = new Tobomon_Db();
                                        _tbm.TobomonName = tm.Tobomon.TobomonName;

                                        tm.Tobomon = _tbm;

                                        await col.Collection(m => m.ListTailieu_Baigiang).LoadAsync();
                                        List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                                        tm.ListTailieu_Baigiang.ForEach(e =>
                                        {
                                            Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                                            tailieu.UserId = e.UserId;
                                            tailieu.TenDoc = e.TenDoc;
                                            tailieu.Status = e.Status;
                                            tailieu.MonhocID = e.MonhocID;
                                            tailieu.Sualancuoi = e.Sualancuoi;
                                            tailieu.Path = e.Path;
                                            tailieu.Kichthuoc = e.Kichthuoc;
                                            tailieu.ChudeID = e.ChudeID;
                                            list.Add(tailieu);
                                        });
                                    }
                                    monhoc = _mapper.Map<List<Monhoc_Model>>(result);
                                    foreach (var m in monhoc)
                                    {
                                        int tongtailieu = m.ListTailieu_Baigiang.Count;
                                        int tailieudaduyet = 0;
                                        if (m.Tinhtrang == "0")
                                        {
                                            m.Tinhtrang = "Cho Duyet";
                                        }
                                        else if (m.Tinhtrang == "1")
                                        {
                                            m.Tinhtrang = "Da Duyet";
                                            
                                        }
                                        foreach (var x1 in m.ListTailieu_Baigiang)
                                        {

                                            if (x1.Status == "0")
                                            {
                                                x1.Status = "Chua duyet";
                                            }
                                            else if (x1.Status == "1")
                                            {
                                                x1.Status = "Da duyet";
                                                tailieudaduyet++;
                                            }
                                        }
                                        m.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;
                                    }
                                }
                                else
                                {
                                    throw new Exception("Not Found");
                                }

                                return monhoc;
                            }
                        }
                        else
                        {
                            throw new Exception("Khong du quyen");
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

            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> deleteMonhoc(int monhoc_id)
        {
            try
            {
                if (monhoc_id != null)
                {
                    KqJson kq = new KqJson();
                    var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == monhoc_id);
                    if (result != null)
                    {
                        // chi xoa duoc mon co tinhtrang = 0 (nhap) or 2 (huy)
                        if (result.Tinhtrang == -1 || result.Tinhtrang == 2)
                        {
                            _context.monhoc_Dbs.Remove(result);
                            int row = await _context.SaveChangesAsync();
                            if (row > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Xoa thanh cong";
                            }
                            else
                            {
                                throw new Exception("Xoa that bai");
                            }
                        }
                        else
                        {
                            throw new Exception("Khong the xoa phan tu nay");
                        }
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }

                    return kq;
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

        public async Task<KqJson> ThemYeuthichMonhoc(MonhocYeuthich_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model != null)
                {
                    MonhocYeuthich_Db wish = new MonhocYeuthich_Db();
                    wish.MonhocId = model.MonhocId;
                    wish.UserId = model.UserId;

                    await _context.monhocYeuthich_Dbs.AddAsync(wish);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Da thich mon hoc nay";

                        return kq;
                    }
                    else
                    {
                        throw new Exception("That bai");
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

        public async Task<KqJson> HuyYeuthichMonhoc(MonhocYeuthich_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model != null)
                {
                    var result = await (from yt in _context.monhocYeuthich_Dbs
                                        where yt.MonhocId == model.MonhocId && yt.UserId == model.UserId
                                        select yt).SingleOrDefaultAsync();
                    if (result != null)
                    {
                        _context.monhocYeuthich_Dbs.Remove(result);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Huy yeu thich thanh cong";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("That bai");
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
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }
    }
}
