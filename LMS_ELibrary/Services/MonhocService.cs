using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
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

        public async Task<IEnumerable<Monhoc_Model>> getAllMonhoc()
        {
            var monhoc = await (from x in _context.monhoc_Dbs orderby x.TenMonhoc ascending select x).ToListAsync();
            List<Monhoc_Model> modelmh = new List<Monhoc_Model>();

            foreach (var mon in monhoc)
            {
                var col = _context.Entry(mon);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                col.Collection(n => n.ListLopgiangday).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
                List<Lopgiangday_Db>listlop=new List<Lopgiangday_Db>();
                mon.ListTailieu_Baigiang.ForEach(e =>
                {
                    if (e.Status == 1)
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
                    }


                });
                mon.ListLopgiangday.ForEach(e =>
                {
                    Lopgiangday_Db lopgiangday = new Lopgiangday_Db();
                    lopgiangday.TenLop = e.TenLop;
                    lopgiangday.Thoigian=e.Thoigian;
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

                foreach (var x1 in model.ListTailieu_Baigiang)
                {
                    x1.Status = "Da duyet";
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

        public async Task<IEnumerable<Monhoc_Model>> locMonhoc(int option)
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
                foreach(var item in listmh)
                {
                    foreach (var x1 in item.ListTailieu_Baigiang)
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
                }
                

                return listmh;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public async Task<Monhoc_Model> chitietMonhoc(int id)
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
    }
}
