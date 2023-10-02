using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
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
                col.Collection(p => p.list_Tongquan).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                List<Tongquan_Db> listtongquan = new List<Tongquan_Db>();
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
                mon.list_Tongquan.ForEach(e =>
                {
                    Tongquan_Db tongquan = new Tongquan_Db();
                    tongquan.Tieude = e.Tieude;
                    tongquan.Noidung = e.Noidung;
                    tongquan.Monhoc_Id = e.Monhoc_Id;
                    tongquan.TongquanID = e.TongquanID;
                    listtongquan.Add(tongquan);
                });
                mon.ListTailieu_Baigiang = list;
                mon.ListLopgiangday = listlop;
                mon.list_Tongquan = listtongquan;
            }
            modelmh = _mapper.Map<List<Monhoc_Model>>(monhoc);

            foreach (Monhoc_Model model in modelmh)
            {
                var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.UserId);
                model.Giangvien = gv.UserFullname;
                int tongtailieu = model.ListTailieu_Baigiang.Count;
                int tailieudaduyet = 0;
                if (model.Tinhtrang == "-1")
                {
                    model.Tinhtrang = "Luu Nhap";
                }
                else if (model.Tinhtrang == "0")
                {
                    model.Tinhtrang = "Cho Duyet";
                }
                else if (model.Tinhtrang == "1")
                {
                    model.Tinhtrang = "Da Duyet";
                }
                else if (model.Tinhtrang == "2")
                {
                    model.Tinhtrang = "Bi tu choi duyet";
                }
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

        public async Task<object> searchMonhoc(string key)
        {
            try
            {
                if (key != "")
                {
                    var monhoc = await (from mh in _context.monhoc_Dbs
                                        join giangvien in _context.user_Dbs
                                        on mh.UserId equals giangvien.UserID
                                        where mh.MaMonhoc.Contains(key) ||
                                        mh.TenMonhoc.Contains(key) ||
                                        giangvien.UserFullname.Contains(key)
                                        select mh).ToListAsync();
                    if (monhoc.Count > 0)
                    {
                        List<Monhoc_Model> list_monhoc_model = new List<Monhoc_Model>();
                        foreach (var m in monhoc)
                        {
                            var col = _context.Entry(m);
                            col.Collection(m => m.ListTailieu_Baigiang).Load();
                            col.Collection(n => n.ListLopgiangday).Load();
                            col.Collection(p => p.list_Tongquan).Load();
                            List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                            List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                            List<Tongquan_Db> listtongquan = new List<Tongquan_Db>();
                            m.ListTailieu_Baigiang.ForEach(e =>
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
                            m.ListLopgiangday.ForEach(e =>
                            {
                                Lopgiangday_Db lopgiangday = new Lopgiangday_Db();
                                lopgiangday.TenLop = e.TenLop;
                                lopgiangday.Thoigian = e.Thoigian;
                                lopgiangday.Truycapgannhat = e.Truycapgannhat;
                                lopgiangday.UserID = e.UserID;
                                lopgiangday.MonhocID = e.MonhocID;
                                listlop.Add(lopgiangday);
                            });
                            m.list_Tongquan.ForEach(e =>
                            {
                                Tongquan_Db tongquan = new Tongquan_Db();
                                tongquan.Tieude = e.Tieude;
                                tongquan.Noidung = e.Noidung;
                                tongquan.Monhoc_Id = e.Monhoc_Id;
                                tongquan.TongquanID = e.TongquanID;
                                listtongquan.Add(tongquan);
                            });
                            m.ListTailieu_Baigiang = list;
                            m.ListLopgiangday = listlop;
                            m.list_Tongquan = listtongquan;
                        }

                        list_monhoc_model = _mapper.Map<List<Monhoc_Model>>(monhoc);
                        foreach (var monhoc_model in list_monhoc_model)
                        {
                            foreach (var monh in monhoc_model.ListTailieu_Baigiang)
                            {
                                if (monh.Status == "0")
                                {
                                    monh.Status = "Chua duyet";
                                }
                                else if (monh.Status == "1")
                                {
                                    monh.Status = "Da duyet";
                                }
                            }
                        }
                        return list_monhoc_model;
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
                                        orderby m.Truycapgannhat descending
                                        select m).ToListAsync();
                }
                else if (option == 0)
                {
                    listmonhoc = await (from m in _context.monhoc_Dbs
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

        public async Task<object> chitietMonhoc(int id, int user_id)
        {
            try
            {
                if (id != null)
                {
                    var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == id);
                    if (result != null)
                    {
                        result.Truycapgannhat = DateTime.Now;
                        await _context.SaveChangesAsync();
                        var col = _context.Entry(result);
                        col.Collection(m => m.ListTailieu_Baigiang).Load();
                        col.Collection(n => n.ListLopgiangday).Load();
                        col.Collection(p => p.list_Tongquan).Load();
                        List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                        List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                        List<Tongquan_Db> listtongquan = new List<Tongquan_Db>();
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
                            lopgiangday.LopgiangdayID = e.LopgiangdayID;
                            lopgiangday.TenLop = e.TenLop;
                            lopgiangday.Thoigian = e.Thoigian;
                            lopgiangday.Truycapgannhat = e.Truycapgannhat;
                            lopgiangday.UserID = e.UserID;
                            lopgiangday.MonhocID = e.MonhocID;
                            listlop.Add(lopgiangday);
                        });
                        result.list_Tongquan.ForEach(e =>
                        {
                            Tongquan_Db tongquan = new Tongquan_Db();
                            tongquan.Tieude = e.Tieude;
                            tongquan.Noidung = e.Noidung;
                            tongquan.Monhoc_Id = e.Monhoc_Id;
                            tongquan.TongquanID = e.TongquanID;
                            listtongquan.Add(tongquan);
                        });
                        result.ListTailieu_Baigiang = list;
                        result.ListLopgiangday = listlop;
                        result.list_Tongquan = listtongquan;
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

        //Status = 0 => gui yeu cau (cho duyet) ; -1 => huy yeu cau (tro lai luu nhap)
        public async Task<KqJson> setTrangthai(List<int> monhoc_id, int status)
        {
            try
            {
                if (monhoc_id.Count > 0 && status == 0 || status == -1)
                {
                    if (status == 0 || status == -1)
                    {
                        KqJson kq = new KqJson();
                        foreach (int monoc in monhoc_id)
                        {
                            var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == monoc);
                            if (result != null)
                            {
                                if (status == 0)
                                {
                                    if (result.Tinhtrang == -1 || result.Tinhtrang == 2)
                                    {
                                        result.Tinhtrang = 0;
                                    }
                                    else
                                    {
                                        throw new Exception("Tinh trang khong phu hop de gui yeu cau");
                                    }
                                }
                                else if (status == -1)
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
                        throw new Exception("Request Params not Suitable!");
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
                    mh.Truycapgannhat = DateTime.Now;

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

        public async Task<KqJson> YeuthichMonhoc(Yeuthich_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.UserId > 0 && model.WishEntityId > 0)
                {
                    var check = await (from monhoc in _context.monhocYeuthich_Dbs
                                       where monhoc.MonhocId == model.WishEntityId && monhoc.UserId == model.UserId
                                       select monhoc).SingleOrDefaultAsync();
                    if (check != null)
                    {
                        _context.monhocYeuthich_Dbs.Remove(check);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Da xoa thich mon hoc nay";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("Xoa yeu thich that bai");
                        }
                    }
                    else
                    {
                        MonhocYeuthich_Db wish = new MonhocYeuthich_Db();
                        wish.MonhocId = model.WishEntityId;
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
                            throw new Exception("Them yeu thich that bai");
                        }

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

        public async Task<object> xemMonhocDanghoc(int hocvien_id)
        {
            try
            {
                if (hocvien_id >0)
                {
                    List<Monhoc_Db> result = new List<Monhoc_Db>();

                    var list_lopdanghoc = await (from lop in _context.lopgiangday_Dbs
                                                 join hvl in _context.hocvien_Lop_Dbs
                                                 on lop.LopgiangdayID equals hvl.Lopgiang_Id
                                                 where hvl.User_Id == hocvien_id
                                                 select lop).ToListAsync();

                    foreach (var lop in list_lopdanghoc)
                    {
                        var monh = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == lop.MonhocID);
                        result.Add(monh);
                    }

                    if (result.Count > 0)
                    {
                        List<Monhoc_Model> modelmh = new List<Monhoc_Model>();

                        foreach (var mon in result)
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
                                lopgiangday.LopgiangdayID = e.LopgiangdayID;
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
                        modelmh = _mapper.Map<List<Monhoc_Model>>(result);

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
                                               where c.MonhocId == model.MonhocID && c.UserId == hocvien_id
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
                    else
                    {
                        throw new Exception("Khong co mon dang hoc");
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

        public async Task<KqJson> themTongquanMonhoc(List<Them_Tongquan_Monhoc_Request_DTO> list_model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (list_model.Count > 0)
                {
                    List<Tongquan_Db> listAdd = new List<Tongquan_Db>();
                    foreach (var model in list_model)
                    {
                        var mon = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == model.Monhoc_Id);
                        var user = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.Giangvien_Id);
                        if (mon != null && user != null)
                        {
                            //chi nguoi them monhoc hoac role="ADMIN" moi them duoc tong quan
                            if (mon.UserId == model.Giangvien_Id || user.Role == 1)
                            {
                                Tongquan_Db tq = new Tongquan_Db();
                                tq.Tieude = model.Tieude;
                                tq.Noidung = model.Noidung;
                                tq.Monhoc_Id = model.Monhoc_Id;

                                listAdd.Add(tq);
                            }
                            else
                            {
                                throw new Exception("Khong du quyen thuc hien thao tac nay");
                            }
                        }
                        else
                        {
                            throw new Exception("Khong tim thay mon hoc muon them tong quan");
                        }
                    }

                    await _context.tongquan_Dbs.AddRangeAsync(listAdd);
                    int row = await _context.SaveChangesAsync();
                    if (row == list_model.Count)
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

        public async Task<KqJson> xetduyetMonhoc(Xetduyet_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                //duyet -> 1 ; khong duyet -> 2 
                if (model.Status == 1 || model.Status == 2 && model.ID_Canduyet != null)
                {
                    var result = await (from mh in _context.monhoc_Dbs
                                        where mh.MonhocID == model.ID_Canduyet && mh.Tinhtrang == 0
                                        select mh).SingleOrDefaultAsync();
                    if (result != null)
                    {
                        if (model.Ghichu != "")
                        {
                            result.Ghichu = model.Ghichu;
                        }
                        result.Tinhtrang = model.Status;
                        if (model.Status == 1 && model.ID_Nguoiduyet != 0)
                        {
                            result.Ngayduyet = DateTime.Now;
                            result.Nguoiduyet = model.ID_Nguoiduyet;
                        }

                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Phe duyet thanh cong";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("Phe duyet that bai");
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

        public async Task<object> Mondangday(int giangvien_Id)
        {
            try
            {
                if (giangvien_Id > 0)
                {
                    var giangvien = await (from gv in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on gv.Role equals role.RoleId
                                           where gv.UserID == giangvien_Id &&
                                           role.Phanquyen == 2
                                           select gv).SingleOrDefaultAsync();
                    if (giangvien != null)
                    {
                        var monhoc = await (from mon in _context.monhoc_Dbs
                                            where mon.UserId == giangvien_Id
                                            select mon).ToListAsync();
                        if (monhoc.Count > 0)
                        {
                            List<Monhoc_Model> modelmh = new List<Monhoc_Model>();

                            foreach (var mon in monhoc)
                            {

                                var col = _context.Entry(mon);
                                col.Collection(m => m.ListTailieu_Baigiang).Load();
                                col.Collection(n => n.ListLopgiangday).Load();
                                col.Collection(p => p.list_Tongquan).Load();
                                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                                List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                                List<Tongquan_Db> listtongquan = new List<Tongquan_Db>();
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
                                mon.list_Tongquan.ForEach(e =>
                                {
                                    Tongquan_Db tongquan = new Tongquan_Db();
                                    tongquan.Tieude = e.Tieude;
                                    tongquan.Noidung = e.Noidung;
                                    tongquan.Monhoc_Id = e.Monhoc_Id;
                                    tongquan.TongquanID = e.TongquanID;
                                    listtongquan.Add(tongquan);
                                });
                                mon.ListTailieu_Baigiang = list;
                                mon.ListLopgiangday = listlop;
                                mon.list_Tongquan = listtongquan;
                            }
                            modelmh = _mapper.Map<List<Monhoc_Model>>(monhoc);

                            foreach (Monhoc_Model model in modelmh)
                            {
                                var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.UserId);
                                model.Giangvien = gv.UserFullname;
                                int tongtailieu = model.ListTailieu_Baigiang.Count;
                                int tailieudaduyet = 0;
                                if (model.Tinhtrang == "-1")
                                {
                                    model.Tinhtrang = "Luu Nhap";
                                }
                                else if (model.Tinhtrang == "0")
                                {
                                    model.Tinhtrang = "Cho Duyet";
                                }
                                else if (model.Tinhtrang == "1")
                                {
                                    model.Tinhtrang = "Da Duyet";
                                }
                                else if (model.Tinhtrang == "2")
                                {
                                    model.Tinhtrang = "Bi tu choi duyet";
                                }
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

                            }


                            return modelmh;
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
            }
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> Xem_List_monhoc_Yeuthich(int hocvien_id, int option)
        {
            try
            {
                //option = 1 -> Gan sao ; 2 - > khong gan sao
                if (hocvien_id > 0 && option == 1 || option == 2)
                {
                    var check_hocvien = await (from hocvien in _context.user_Dbs
                                               join role in _context.role_Dbs
                                               on hocvien.Role equals role.RoleId
                                               where hocvien.UserID == hocvien_id &&
                                               role.Phanquyen == 3
                                               select hocvien).SingleOrDefaultAsync();
                    if (check_hocvien != null)
                    {

                        List<Monhoc_Model> modelmh = new List<Monhoc_Model>();
                        List<Monhoc_Db> monhoc = new List<Monhoc_Db>();
                        if (option == 1)
                        {
                            monhoc = await (from mh in _context.monhoc_Dbs
                                            join wish in _context.monhocYeuthich_Dbs
                                            on mh.MonhocID equals wish.MonhocId
                                            where wish.UserId == hocvien_id
                                            select mh).ToListAsync();
                        }
                        else
                        {
                            monhoc = await (from mh in _context.monhoc_Dbs
                                            join wish in _context.monhocYeuthich_Dbs
                                            on mh.MonhocID equals wish.MonhocId
                                            where wish.UserId != hocvien_id
                                            select mh).ToListAsync();
                        }
                        foreach (var mon in monhoc)
                        {

                            var col = _context.Entry(mon);
                            col.Collection(m => m.ListTailieu_Baigiang).Load();
                            col.Collection(n => n.ListLopgiangday).Load();
                            col.Collection(p => p.list_Tongquan).Load();
                            List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                            List<Lopgiangday_Db> listlop = new List<Lopgiangday_Db>();
                            List<Tongquan_Db> listtongquan = new List<Tongquan_Db>();
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
                            mon.list_Tongquan.ForEach(e =>
                            {
                                Tongquan_Db tongquan = new Tongquan_Db();
                                tongquan.Tieude = e.Tieude;
                                tongquan.Noidung = e.Noidung;
                                tongquan.Monhoc_Id = e.Monhoc_Id;
                                tongquan.TongquanID = e.TongquanID;
                                listtongquan.Add(tongquan);
                            });
                            mon.ListTailieu_Baigiang = list;
                            mon.ListLopgiangday = listlop;
                            mon.list_Tongquan = listtongquan;
                        }
                        modelmh = _mapper.Map<List<Monhoc_Model>>(monhoc);

                        foreach (Monhoc_Model model in modelmh)
                        {
                            var gv = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.UserId);
                            model.Giangvien = gv.UserFullname;
                            int tongtailieu = model.ListTailieu_Baigiang.Count;
                            int tailieudaduyet = 0;
                            if (model.Tinhtrang == "-1")
                            {
                                model.Tinhtrang = "Luu Nhap";
                            }
                            else if (model.Tinhtrang == "0")
                            {
                                model.Tinhtrang = "Cho Duyet";
                            }
                            else if (model.Tinhtrang == "1")
                            {
                                model.Tinhtrang = "Da Duyet";
                            }
                            else if (model.Tinhtrang == "2")
                            {
                                model.Tinhtrang = "Bi tu choi duyet";
                            }
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
                            
                            if (option==1)
                            {
                                model.TrangthaiYeuthich = "Yeu thich";
                            }
                            else
                            {
                                model.TrangthaiYeuthich = "Chua Yeu thich";
                            }
                        }


                        return modelmh;
                    }
                    else
                    {
                        throw new Exception("Khong tim thay hoc vien nay");
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
    }
}
