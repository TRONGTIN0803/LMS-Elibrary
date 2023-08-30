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
            var monhoc =await (from x in _context.monhoc_Dbs orderby x.TenMonhoc ascending select x).ToListAsync();
            List<Monhoc_Model> modelmh = new List<Monhoc_Model>();

            foreach (var mon in monhoc)
            {
                var col = _context.Entry(mon);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
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
                mon.ListTailieu_Baigiang = list;
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
                var monhoc =await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MaMonhoc == key || p.TenMonhoc == key);

                var col = _context.Entry(monhoc);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
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
                monhoc.ListTailieu_Baigiang = list;
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

        public async Task<IEnumerable<Monhoc_Db>> locMonhoc(int sort)
        {
            //0=>ten ; 1=> truycapgannhat
            try
            {
                List<Monhoc_Db> list = new List<Monhoc_Db>();
                // var result = await _context.monhoc_Dbs.Join(_context.lopgiangday_Dbs,m=>m.MonhocID,lop=>lop.MonhocID)
                if (sort == 1)
                {
                    var result = await (from m in _context.monhoc_Dbs
                                        join l in _context.lopgiangday_Dbs on
                                     m.MonhocID equals l.MonhocID
                                        orderby l.Truycapgannhat descending
                                        select m).ToListAsync();
                    return result;
                }else if (sort == 0)
                {
                    var result = await (from m in _context.monhoc_Dbs
                                        join l in _context.lopgiangday_Dbs on
                                     m.MonhocID equals l.MonhocID
                                        orderby m.TenMonhoc ascending
                                        select m).ToListAsync();
                    return result;
                }
                
                return null;
                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            
        }

        public async Task<Monhoc_Model> chitietMonhoc(int id)
        {
            try
            {
                var result =await _context.monhoc_Dbs.SingleOrDefaultAsync(p => p.MonhocID == id);
                var col = _context.Entry(result);
                col.Collection(m => m.ListTailieu_Baigiang).Load();
                List<Tailieu_Baigiang_Db> list = new List<Tailieu_Baigiang_Db>();
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
                result.ListTailieu_Baigiang = list;
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
