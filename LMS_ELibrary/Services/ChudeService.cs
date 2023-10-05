using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LMS_ELibrary.Services
{

    public class ChudeService : IChudeService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public ChudeService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Chude_Model> editChude(int id,Chude_Model chude)
        {
            
            var cd = await _context.chude_Dbs.SingleOrDefaultAsync(p => p.ChudeID == id);
            if (cd != null)
            {
                
                cd.Tenchude = chude.Tenchude;
                await _context.SaveChangesAsync();
            }

            Chude_Model model = new Chude_Model();
            model = _mapper.Map<Chude_Model>(cd);
            return model;
        }

        public async Task<KqJson>addChude(Chude_Model chude)
        {
            Chude_Db _chude = new Chude_Db();
            KqJson kq = new KqJson();
            _chude.Tenchude = chude.Tenchude;
            await _context.chude_Dbs.AddAsync(_chude);
            int row=await _context.SaveChangesAsync();
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
        }

        public async Task<IEnumerable<Chude_Model>> getAllchude(int monhoc_id)
        {
            var result = await (from cd in _context.chude_Dbs where cd.Monhoc_Id==monhoc_id select cd).ToListAsync();
            List<Chude_Model> kq = new List<Chude_Model>();
           
            foreach(var chude in result)
            {
                var col = _context.Entry(chude);
                col.Collection(n => n.ListTailieu_Baigiang).Load();
                
                if (chude.ListTailieu_Baigiang != null)
                {
                    List<Tailieu_Baigiang_Db> listtailieu = new List<Tailieu_Baigiang_Db>();
                    foreach (var item in chude.ListTailieu_Baigiang)
                    {
                        //if (item.Type==1)
                        //{
                        //    Tailieu_Baigiang_Db tailieu = new Tailieu_Baigiang_Db();
                        //    tailieu.UserId = item.UserId;
                        //    tailieu.TenDoc = item.TenDoc;
                        //    tailieu.Status = item.Status;
                        //    tailieu.MonhocID = item.MonhocID;
                        //    tailieu.Sualancuoi = item.Sualancuoi;
                        //    tailieu.Path = item.Path;
                        //    tailieu.Kichthuoc = item.Kichthuoc;
                        //    tailieu.ChudeID = item.ChudeID;
                        //    listtailieu.Add(tailieu);
                        //}
                    }
                    chude.ListTailieu_Baigiang = listtailieu;
                }
                
               
                
            }
            

            kq=_mapper.Map<List<Chude_Model>>(result);
            foreach (Chude_Model model in kq)
            {

                foreach (var x1 in model.ListTailieu_Baigiang)
                {
                    x1.Type = "Bai Giang";
                    if (x1.Status == "0")
                    {
                        x1.Status = "Cho Duyet";
                    }else if (x1.Status == "1")
                    {
                        x1.Status = "Da Duyet";
                    }
                }
            }

            return kq;
        }

        public async Task<KqJson> deletetChude(int id)
        {
            var result = await _context.chude_Dbs.SingleOrDefaultAsync(p => p.ChudeID == id);
            KqJson kq = new KqJson();
            if (result != null)
            {
                 _context.Remove(result);
                await _context.SaveChangesAsync();
               
                kq.Status = true;
                kq.Message = "Xoa thanh cong";
            }
            else
            {
                kq.Status = false;
                kq.Message = "Khong tim thay";
            }

            return kq;
        }

        public async Task<object> Xem_Chude_Monhoc(int monhoc_id)
        {
            try
            {
                if (monhoc_id > 0)
                {
                    var result = await (from cd in _context.chude_Dbs 
                                        where cd.Monhoc_Id == monhoc_id 
                                        select cd).ToListAsync();
                    if (result.Count > 0)
                    {
                        List<Chude_Model> list_chude = new List<Chude_Model>();
                        list_chude = _mapper.Map<List<Chude_Model>>(result);
                        return list_chude;
                    }
                    else
                    {
                        throw new Exception("Mon hoc nay khong co chu de nao");
                    }
                }
                else
                {
                    throw new Exception("Khong co mon hoc nay");
                }
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }
    }
}
