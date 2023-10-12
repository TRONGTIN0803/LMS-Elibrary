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
        /*
         * Status = 1 -> Luu nhap
         * Status = 2 -> Gui yeu cau duyet
         * Status = 3 -> Da phe duyet
         * Status = 4 -> Admin tu choi duyet
         */
        public async Task<object> getAllMonhoc(int user_id, int page)
        {
            try
            {
                if (user_id > 0 && page >= 0)
                {
                    int skip = (page - 1) * 5;
                    var monhoc = await (from x in _context.monhoc_Dbs orderby x.TenMonhoc ascending select x).Skip(skip).Take(5).ToListAsync();
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
                                tailieu.DocId = e.DocId;
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
                            if (model.Tinhtrang == "1")
                            {
                                model.Tinhtrang = "Luu Nhap";
                            }
                            else if (model.Tinhtrang == "2")
                            {
                                model.Tinhtrang = "Cho Duyet";
                            }
                            else if (model.Tinhtrang == "3")
                            {
                                model.Tinhtrang = "Da Duyet";
                            }
                            else if (model.Tinhtrang == "4")
                            {
                                model.Tinhtrang = "Bi tu choi duyet";
                            }
                            foreach (var x1 in model.ListTailieu_Baigiang)
                            {
                                if (x1.Status == "3")
                                {
                                    x1.Status = "Da duyet";
                                    tailieudaduyet++;
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
                    else
                    {
                        throw new Exception("Khong co mon hoc nao");
                    }

                }
                else
                {
                    throw new Exception("Khong tim thay hoc vien dang truy cap");
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
                                tailieu.ChudeID = e.ChudeID;
                                tailieu.Mota = e.Mota;
                                tailieu.Ghichu = e.Ghichu;
                                tailieu.NgayDuyet = e.NgayDuyet;
                                tailieu.Nguoiduyet = e.Nguoiduyet;
                                tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
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
                            int tongtailieu = monhoc_model.ListTailieu_Baigiang.Count;
                            int tailieudaduyet = 0;
                            if (monhoc_model.Tinhtrang == "1")
                            {
                                monhoc_model.Tinhtrang = "Luu Nhap";
                            }
                            else if (monhoc_model.Tinhtrang == "2")
                            {
                                monhoc_model.Tinhtrang = "Cho Duyet";
                            }
                            else if (monhoc_model.Tinhtrang == "3")
                            {
                                monhoc_model.Tinhtrang = "Da Duyet";
                            }
                            else if (monhoc_model.Tinhtrang == "4")
                            {
                                monhoc_model.Tinhtrang = "Bi tu choi duyet";
                            }
                            foreach (var x1 in monhoc_model.ListTailieu_Baigiang)
                            {
                                if (x1.Status == "3")
                                {
                                    x1.Status = "Da duyet";
                                    tailieudaduyet++;
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
                            monhoc_model.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;

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

        public async Task<object> locMonhoc(int option, int user_id)
        {
            /*
             * option = 0 => ten ;
             * option = 1 => truycapgannhat
             */

            try
            {
                if (user_id > 0)
                {
                    if (option < 0 || option > 1)
                    {
                        throw new Exception("Option khong phu hop");
                    }
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
                    if (listmh.Count < 0)
                    {
                        throw new Exception("Not Found");
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
                            tailieu.ChudeID = e.ChudeID;
                            tailieu.Mota = e.Mota;
                            tailieu.Ghichu = e.Ghichu;
                            tailieu.NgayDuyet = e.NgayDuyet;
                            tailieu.Nguoiduyet = e.Nguoiduyet;
                            tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
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
                            if (x1.Status == "3")
                            {
                                x1.Status = "Da duyet";
                                tailieudaduyet++;
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

        public async Task<object> chitietMonhoc(int id, int user_id)
        {
            try
            {
                if (id > 0 && user_id > 0)
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
                            tailieu.ChudeID = e.ChudeID;
                            tailieu.Mota = e.Mota;
                            tailieu.Ghichu = e.Ghichu;
                            tailieu.NgayDuyet = e.NgayDuyet;
                            tailieu.Nguoiduyet = e.Nguoiduyet;
                            tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
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
                        if (mon.Tinhtrang == "1")
                        {
                            mon.Tinhtrang = "Luu Nhap";
                        }
                        else if (mon.Tinhtrang == "2")
                        {
                            mon.Tinhtrang = "Cho Duyet";
                        }
                        else if (mon.Tinhtrang == "3")
                        {
                            mon.Tinhtrang = "Da Duyet";
                        }
                        else if (mon.Tinhtrang == "4")
                        {
                            mon.Tinhtrang = "Bi tu choi duyet";
                        }
                        foreach (var x1 in mon.ListTailieu_Baigiang)
                        {
                            if (x1.Status == "3")
                            {
                                x1.Status = "Da duyet";
                                tailieudaduyet++;
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

        public async Task<KqJson> editMonhoc(Edit_Monhoc_Request_DTO model)
        {
            try
            {
                KqJson kq = new KqJson();
                if (model.User_Id > 0 && model.Monhoc_Id > 0)
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
                        Monhoc_Db result = new Monhoc_Db();
                        result = await _context.monhoc_Dbs.FirstOrDefaultAsync(p => p.MonhocID == model.Monhoc_Id);
                        if (quyen.Phanquyen == 2) //Giang vien -> edit mon hoc cua ban than
                        {
                            if (result.UserId != model.User_Id)
                            {
                                throw new Exception("Khong Du quyen de sua mon hoc nay");
                            }
                        }
                        if (result != null)
                        {
                            result.TenMonhoc = model.Monhoc_Name != "" ? result.TenMonhoc = model.Monhoc_Name : result.TenMonhoc;
                            result.MaMonhoc = model.MaMonhoc != "" ? result.MaMonhoc = model.MaMonhoc : result.MaMonhoc;
                            result.Mota = model.Mota != "" ? result.Mota = model.Mota : result.Mota;
                            int row = await _context.SaveChangesAsync();
                            if (row > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Thay doi thanh cong";
                                return kq;
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
                        throw new Exception("Khong du quyen the hien");
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

        public async Task<KqJson> setTrangthai(Guiyeucau_Huyyeucau_Monhoc_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.List_Monhoc_Id.Count > 0 && model.User_Id > 0)
                {
                    if (model.Status == 2 || model.Status == 1)
                    {
                        int indexx = 0;
                        foreach (int monoc in model.List_Monhoc_Id)
                        {
                            indexx++;
                            var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == monoc && p.UserId == model.User_Id);
                            if (result != null)
                            {
                                if (model.Status == 2)
                                {
                                    if (result.Tinhtrang == 1 || result.Tinhtrang == 4)
                                    {
                                        result.Tinhtrang = 2;
                                    }
                                    else
                                    {
                                        throw new Exception("Phan tu [" + indexx + "] khong the gui yeu cau");
                                    }
                                }
                                else if (model.Status == 1)
                                {
                                    if (result.Tinhtrang == 2)
                                    {
                                        result.Tinhtrang = 1;
                                    }
                                    else
                                    {
                                        throw new Exception("phần tử " + indexx + " không ở trạng thái chờ....");
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Not Found");
                            }
                        }
                        int row = await _context.SaveChangesAsync();
                        if (row == model.List_Monhoc_Id.Count)
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
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> addMonhoc(Monhoc_Model monhoc)
        {
            try
            {
                if (monhoc.TenMonhoc != "" && monhoc.MaMonhoc != "" && monhoc.Mota != "" && monhoc.UserId > 0 && monhoc.TobomonId > 0)
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
                if (status > 0 && giangvien_id > 0)
                {
                    var gv = await (from nd in _context.user_Dbs
                                    join role in _context.role_Dbs
                                    on nd.Role equals role.RoleId
                                    where nd.UserID == giangvien_id &&
                                    role.Phanquyen == 1 || role.Phanquyen == 2
                                    select nd).FirstOrDefaultAsync();
                    if (gv != null)
                    {

                        if (status < 1 || status > 4)
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
                                        tailieu.ChudeID = e.ChudeID;
                                        tailieu.Mota = e.Mota;
                                        tailieu.Ghichu = e.Ghichu;
                                        tailieu.NgayDuyet = e.NgayDuyet;
                                        tailieu.Nguoiduyet = e.Nguoiduyet;
                                        tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
                                        list.Add(tailieu);
                                    });
                                }
                                monhoc = _mapper.Map<List<Monhoc_Model>>(result);
                                foreach (var m in monhoc)
                                {
                                    int tongtailieu = m.ListTailieu_Baigiang.Count;
                                    int tailieudaduyet = 0;
                                    if (m.Tinhtrang == "1")
                                    {
                                        m.Tinhtrang = "Luu Nhap";
                                    }
                                    else if (m.Tinhtrang == "2")
                                    {
                                        m.Tinhtrang = "Cho Duyet";
                                    }
                                    else if (m.Tinhtrang == "3")
                                    {
                                        m.Tinhtrang = "Da Duyet";
                                    }
                                    else if (m.Tinhtrang == "4")
                                    {
                                        m.Tinhtrang = "Bi tu choi duyet";
                                    }
                                    foreach (var x1 in m.ListTailieu_Baigiang)
                                    {

                                        if (x1.Status == "3")
                                        {
                                            x1.Status = "Da duyet";
                                            tailieudaduyet++;
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
                                    m.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;
                                }
                                return monhoc;
                            }
                            else
                            {
                                throw new Exception("Not Found");
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("Khong du quyen");
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

        public async Task<KqJson> deleteMonhoc(Delete_Entity_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.EntityId > 0 && model.User_Id > 0)
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
                        var result = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == model.EntityId);
                        if (result != null)
                        {

                            if (result.Tinhtrang == 1 || result.Tinhtrang == 4)
                            {
                                if (quyen.Phanquyen == 2)
                                {
                                    if (result.UserId != model.User_Id)
                                    {
                                        throw new Exception("Khong du quyen de xoa mon hoc nay");
                                    }
                                }
                                _context.monhoc_Dbs.Remove(result);
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
                                throw new Exception("Khong the xoa Monhoc o tinh trang nay");
                            }
                        }
                        else
                        {
                            throw new Exception("Not Found");
                        }
                    }
                    else
                    {
                        throw new Exception("Nguoi dung phai la Admin hoac Giang vien");
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
                if (hocvien_id > 0)
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
                                tailieu.ChudeID = e.ChudeID;
                                tailieu.Mota = e.Mota;
                                tailieu.Ghichu = e.Ghichu;
                                tailieu.NgayDuyet = e.NgayDuyet;
                                tailieu.Nguoiduyet = e.Nguoiduyet;
                                tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
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
                            if (model.Tinhtrang == "1")
                            {
                                model.Tinhtrang = "Luu Nhap";
                            }
                            else if (model.Tinhtrang == "2")
                            {
                                model.Tinhtrang = "Cho Duyet";
                            }
                            else if (model.Tinhtrang == "3")
                            {
                                model.Tinhtrang = "Da Duyet";
                            }
                            else if (model.Tinhtrang == "4")
                            {
                                model.Tinhtrang = "Bi tu choi duyet";
                            }
                            foreach (var x1 in model.ListTailieu_Baigiang)
                            {
                                if (x1.Status == "3")
                                {
                                    x1.Status = "Da duyet";
                                    tailieudaduyet++;
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

        public async Task<KqJson> themTongquanMonhoc(Them_Tongquan_Monhoc_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Giangvien_Id > 0 && model.Monhoc_Id > 0 && model.list_Tong_Quan_Mon_Hoc.Count > 0)
                {
                    List<Tongquan_Db> listAdd = new List<Tongquan_Db>();
                    foreach (var tongquan in model.list_Tong_Quan_Mon_Hoc)
                    {
                        var mon = await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == model.Monhoc_Id);
                        var user = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.Giangvien_Id);

                        if (mon != null && user != null)
                        {
                            var quyen = await (from nd in _context.user_Dbs
                                               join role in _context.role_Dbs
                                               on nd.Role equals role.RoleId
                                               where nd.UserID == model.Giangvien_Id
                                               select role).FirstOrDefaultAsync();
                            //chi nguoi them monhoc hoac role="ADMIN" moi them duoc tong quan
                            if (mon.UserId == model.Giangvien_Id || quyen.Phanquyen == 1)
                            {
                                Tongquan_Db tq = new Tongquan_Db();
                                tq.Tieude = tongquan.Tieude;
                                tq.Noidung = tongquan.Noidung;
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
                    if (row == model.list_Tong_Quan_Mon_Hoc.Count)
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
                if (model.ID_Nguoiduyet > 0)
                {
                    //check User la Admin
                    var checkUser = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.ID_Nguoiduyet &&
                                           role.Phanquyen == 1
                                           select nd).FirstOrDefaultAsync();
                    if (checkUser != null)
                    {
                        //duyet -> 3 ; khong duyet -> 4 
                        if (model.Status == 3 || model.Status == 4 && model.ID_Canduyet > 0)
                        {
                            var result = await (from mh in _context.monhoc_Dbs
                                                where mh.MonhocID == model.ID_Canduyet && mh.Tinhtrang == 2
                                                select mh).SingleOrDefaultAsync();
                            if (result != null)
                            {

                                result.Ghichu = model.Ghichu != null ? model.Ghichu :  null;
                                result.Tinhtrang = model.Status;
                                result.Ngayduyet = DateTime.Now;
                                result.Nguoiduyet = model.ID_Nguoiduyet;

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
                    else
                    {
                        throw new Exception("Chi co ADMIN duoc su dung chuc nang nay");
                    }
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung nay");
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
                                    tailieu.ChudeID = e.ChudeID;
                                    tailieu.Mota = e.Mota;
                                    tailieu.Ghichu = e.Ghichu;
                                    tailieu.NgayDuyet = e.NgayDuyet;
                                    tailieu.Nguoiduyet = e.Nguoiduyet;
                                    tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
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
                                if (model.Tinhtrang == "1")
                                {
                                    model.Tinhtrang = "Luu Nhap";
                                }
                                else if (model.Tinhtrang == "2")
                                {
                                    model.Tinhtrang = "Cho Duyet";
                                }
                                else if (model.Tinhtrang == "3")
                                {
                                    model.Tinhtrang = "Da Duyet";
                                }
                                else if (model.Tinhtrang == "4")
                                {
                                    model.Tinhtrang = "Bi tu choi duyet";
                                }
                                foreach (var x1 in model.ListTailieu_Baigiang)
                                {
                                    if (model.Tinhtrang == "1")
                                    {
                                        model.Tinhtrang = "Luu Nhap";
                                    }
                                    else if (model.Tinhtrang == "2")
                                    {
                                        model.Tinhtrang = "Cho Duyet";
                                    }
                                    else if (model.Tinhtrang == "3")
                                    {
                                        model.Tinhtrang = "Da Duyet";
                                    }
                                    else if (model.Tinhtrang == "4")
                                    {
                                        model.Tinhtrang = "Bi tu choi duyet";
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
                                tailieu.ChudeID = e.ChudeID;
                                tailieu.Mota = e.Mota;
                                tailieu.Ghichu = e.Ghichu;
                                tailieu.NgayDuyet = e.NgayDuyet;
                                tailieu.Nguoiduyet = e.Nguoiduyet;
                                tailieu.File_Baigiang_Id = e.File_Baigiang_Id;
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
                            if (model.Tinhtrang == "1")
                            {
                                model.Tinhtrang = "Luu Nhap";
                            }
                            else if (model.Tinhtrang == "2")
                            {
                                model.Tinhtrang = "Cho Duyet";
                            }
                            else if (model.Tinhtrang == "3")
                            {
                                model.Tinhtrang = "Da Duyet";
                            }
                            else if (model.Tinhtrang == "4")
                            {
                                model.Tinhtrang = "Bi tu choi duyet";
                            }
                            foreach (var x1 in model.ListTailieu_Baigiang)
                            {
                                if (x1.Status == "3")
                                {
                                    x1.Status = "Da duyet";
                                    tailieudaduyet++;
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
                            model.TailieuPheduyet = tailieudaduyet + "/" + tongtailieu;

                            if (option == 1)
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
