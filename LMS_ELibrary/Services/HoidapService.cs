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

        public async Task<KqJson> ChinhsuaCautrl(Cautrl_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model != null)
                {
                    var cautrl = await _context.cautrl_Dbs.SingleOrDefaultAsync(p=>p.CautrlID==model.CautrlID);
                    if (cautrl != null)
                    {
                        cautrl.Cautrl = model.Cautrl;
                        cautrl.Ngaytao = DateTime.Now;

                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Chinh sua thanh cong";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("Chinh sua that bai");
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
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
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

        public async Task<KqJson> ThemCauhoiYeuthich(CauhoiVandap_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.CauhoiId != null && model.UserId!=null)
                {
                    CauhoiYeuthich_Db like = new CauhoiYeuthich_Db();
                    like.CauhoiId=model.CauhoiId; 
                    like.UserId=model.UserId;

                    await _context.cauhoiYeuthich_Dbs.AddAsync(like);
                    int row=await _context.SaveChangesAsync();
                    if(row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Them yeu thich thanh cong";

                        return kq;
                    }
                    else
                    {
                        throw new Exception("Them yeu thich that bai");
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

        public async Task<object> XemcauhoiYeuthich(int user_id)
        {
            try
            {
                if (user_id != null)
                {
                    var result = await (from wishch in _context.cauhoiYeuthich_Dbs
                                        where wishch.UserId == user_id
                                        select wishch).ToListAsync();
                    if (result.Count>0)
                    {
                        foreach (var ch in result)
                        {
                            var col = _context.Entry(ch);
                            col.Reference(p => p.Cauhoi).Load();
                            CauhoiVandap_Db chdb = new CauhoiVandap_Db();
                            chdb.CauhoiId = ch.Cauhoi.CauhoiId;
                            chdb.Tieude = ch.Cauhoi.Tieude;
                            chdb.Noidung = ch.Cauhoi.Noidung;
                            chdb.Ngaytao = ch.Cauhoi.Ngaytao;
                            chdb.UserId = ch.Cauhoi.UserId;
                            chdb.TailieuId = ch.Cauhoi.TailieuId;
                            chdb.LopgiangId = ch.Cauhoi.LopgiangId;
                            chdb.ChudeId = ch.Cauhoi.ChudeId;

                            ch.Cauhoi = chdb;

                        }
                        List<CauhoiYeuthich_Model> listcauhoi = new List<CauhoiYeuthich_Model>();
                        listcauhoi = _mapper.Map<List<CauhoiYeuthich_Model>>(result);
                        //foreach (var cauhoi in listcauhoi)
                        //{
                        //    int tongcautrl = cauhoi.list_Cautrl.Count;
                        //    cauhoi.TongsoCautrl = tongcautrl;
                        //}
                        return listcauhoi;
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
                                cautrl_db.CautrlID = cautrl.CautrlID;
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
                        foreach(var cauhoi in listmodel)
                        {
                            int tongcautrl = cauhoi.list_Cautrl.Count;
                            cauhoi.TongsoCautrl = tongcautrl;
                        }
                        
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

        public async Task<KqJson> Xoacautrl(Cautrl_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.CautrlID != null && model.UserId!=null)
                {
                    var cautrl = await _context.cautrl_Dbs.SingleOrDefaultAsync(p=>p.CautrlID==model.CautrlID);
                    if (cautrl != null)
                    {
                        if (cautrl.UserId == model.UserId)  //Chi nguoi trl moi duoc xoa
                        {
                            _context.Remove(cautrl);
                        }
                        else
                        {
                            var user = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == model.UserId);
                            if (user.Role == 0)  //Nguoi quan ly duoc phep xoa bat ky cautrl nao
                            {
                                _context.Remove(cautrl);
                            }
                            else
                            {
                                throw new Exception("Khong du quyen de thuc hien");
                            }
                            
                        }
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
                kq.Message = e.Message;

                return kq;
            }
        }
    }
}
