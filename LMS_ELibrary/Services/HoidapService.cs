using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Services
{
    public class HoidapService : IHoidapService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;

        public HoidapService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> DatcauhoiBaigang(CauhoiVandap_Model model)
        {
            KqJson kqJson = new KqJson();
            try
            {
                if (model != null)
                {
                    CauhoiVandap_Db ch = new CauhoiVandap_Db();
                    ch.Tieude = model.Tieude;
                    ch.Noidung= model.Noidung;
                    ch.Ngaytao = DateTime.Now;
                    ch.UserId = model.UserId;
                    ch.TailieuId=model.TailieuId;

                    await _context.AddAsync(ch);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kqJson.Status = true;
                        kqJson.Message = "Them thanh cong";

                        return kqJson;
                    }
                    else
                    {
                        throw new Exception("them cau hoi that bai");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                kqJson.Status = false;
                kqJson.Message=e.Message;

                return kqJson;
            }
        }

        public async Task<KqJson> TrlCauhoi(Cautrl_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model != null)
                {
                    Cautrl_Db cautrl = new Cautrl_Db();
                    cautrl.Cautrl = model.Cautrl;
                    cautrl.Ngaytao = DateTime.Now;
                    cautrl.UserId = model.UserId;
                    cautrl.CauhoiId = model.CauhoiId;

                    await _context.cautrl_Dbs.AddAsync(cautrl);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Trl cau hoi thanh cong";

                        return kq;
                    }
                    else
                    {
                        throw new Exception("Trl cau hoi htat bai");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message=e.Message;

                return kq;
            }
        }

        public async Task<object> XemhoidapBaigiang(int baigiangId)
        {
            KqJson kq = new KqJson();
            try
            {
                if (baigiangId != null)
                {
                    var listcauhoi = await (from listch in _context.cauhoiVandap_Dbs
                                            where listch.TailieuId == baigiangId
                                            select listch).ToListAsync();
                    if (listcauhoi != null)
                    {
                        foreach(var cauhoi in listcauhoi)
                        {
                            var col = _context.Entry(cauhoi);
                            col.Collection(p => p.list_Cautrl).Load();
                            List<Cautrl_Db> listcautrl = new List<Cautrl_Db>();
                            foreach (var cautrl in cauhoi.list_Cautrl)
                            {
                                
                                Cautrl_Db cautrl_db = new Cautrl_Db();
                                cautrl_db.Cautrl = cautrl.Cautrl;
                                cautrl_db.Ngaytao = cautrl.Ngaytao;
                                cautrl_db.CauhoiId = cautrl.CauhoiId;
                                cautrl_db.UserId = cautrl.UserId;

                                listcautrl.Add(cautrl_db);
                            }
                            cauhoi.list_Cautrl = listcautrl;

                        }
                        
                        List<CauhoiVandap_Model> listmodel = new List<CauhoiVandap_Model>();
                        listmodel = _mapper.Map<List<CauhoiVandap_Model>>(listcauhoi);

                        return listmodel;
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
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message=e.Message;

                return kq;
            }
        }
    }
}
