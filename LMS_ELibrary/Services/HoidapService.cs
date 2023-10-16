using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                if (model.CautrlID>0 && model.Cautrl!="" && model.UserId>0)
                {
                    var cautrl = await _context.cautrl_Dbs.SingleOrDefaultAsync(p => p.CautrlID == model.CautrlID);
                    if (cautrl != null)
                    {
                        if (cautrl.UserId != model.UserId)
                        {
                            throw new Exception("Khong the chinh sua cua trl cua nguoi khac");
                        }
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
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> DatcauhoiBaigang(Datcauhoitronglop_Request_DTO model)
        {
            KqJson kqJson = new KqJson();
            try
            {
                if (model.Tieude !="" && model.User_Id >0 && model.TailieuId >0 && model.ChudeId >0 && model.LopgiangId >0)
                {
                    CauhoiVandap_Db ch = new CauhoiVandap_Db();
                    ch.Tieude = model.Tieude;
                    ch.Noidung = model.Noidung;
                    ch.Ngaytao = DateTime.Now;
                    ch.UserId = model.User_Id;
                    ch.TailieuId = model.TailieuId;
                    ch.LopgiangId = model.LopgiangId;
                    ch.ChudeId = model.ChudeId;

                    await _context.cauhoiVandap_Dbs.AddAsync(ch);
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
            }
            catch (Exception e)
            {
                kqJson.Status = false;
                kqJson.Message = e.Message;

                return kqJson;
            }
        }

        public async Task<KqJson> YeuthichCauhoi(Yeuthich_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.WishEntityId > 0 && model.UserId > 0)
                {
                    var check = await (from cauhoi in _context.cauhoiYeuthich_Dbs
                                       where cauhoi.CauhoiId == model.WishEntityId && 
                                       cauhoi.UserId == model.UserId
                                       select cauhoi).SingleOrDefaultAsync();
                    if (check != null)  //Da yeu thich ->xoa yeu thich
                    {
                        _context.cauhoiYeuthich_Dbs.Remove(check);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Huy yeu thich thanh cong";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("Huy yeu thich that bai");
                        }
                    }
                    else //Chua yeu thich -> them yeu thich
                    {
                        CauhoiYeuthich_Db like = new CauhoiYeuthich_Db();
                        like.CauhoiId = model.WishEntityId;
                        like.UserId = model.UserId;

                        await _context.cauhoiYeuthich_Dbs.AddAsync(like);
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
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

        public async Task<KqJson> TrlCauhoi(Cautrl_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Cautrl!="" && model.UserId>0 && model.CauhoiId>0)
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
                        throw new Exception("Trl cau hoi that bai");
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

        public async Task<object> XemcauhoiYeuthich(int user_id)
        {
            try
            {
                if (user_id >0)
                {
                    var result = await (from wishch in _context.cauhoiYeuthich_Dbs
                                        where wishch.UserId == user_id
                                        select wishch).ToListAsync();
                    if (result.Count > 0)
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

        public async Task<object> XemhoidapBaigiang(int user_id, int baigiangId, int lop_Id, int typeCauhoi, int filt)
        {
            /*
             * typeCauhoi = 0(khong chon loai cau hoi) -> loi ; 1 -> cau hoi gan nhat ; 2 -> Cau hoi chua trl ; 3 -> Cau hoi da trl
             * filt = 0(khong chon) -> tat ca ; 1 -> Cau hoi cua toi ; 2 -> Cau hoi yeu thich
             */
            KqJson kq = new KqJson();
            try
            {
                if (user_id > 0)
                {
                    if (lop_Id > 0)
                    {
                        List<CauhoiVandap_Db> listcauhoi = new List<CauhoiVandap_Db>();
                        if (baigiangId > 0) //Co chon bai giang cu the
                        {
                            var list_cauhoi_baigiang = await (from bg in _context.cauhoiVandap_Dbs
                                                              where bg.LopgiangId == lop_Id &&
                                                              bg.TailieuId == baigiangId
                                                              select bg).ToListAsync();
                            if (typeCauhoi == 1) // Cau hoi gan nhat
                            {
                                var list_cauhoi_typeCauhoi = (from bg in list_cauhoi_baigiang
                                                              orderby bg.Ngaytao descending
                                                              select bg).ToList();
                                if (filt == 1) // Cau hoi cua toi
                                {
                                    listcauhoi = (from listch in list_cauhoi_typeCauhoi
                                                  where listch.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 2) //cau hoi yeu thich
                                {
                                    listcauhoi = (from listch in list_cauhoi_typeCauhoi
                                                  join wishques in _context.cauhoiYeuthich_Dbs
                                                  on listch.CauhoiId equals wishques.CauhoiId
                                                  where wishques.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 0) // tat ca
                                {
                                    listcauhoi = (from listch in list_cauhoi_typeCauhoi
                                                  select listch).ToList();
                                }
                            }
                            else if (typeCauhoi == 2) //Cau hoi chua trl
                            {
                                List<CauhoiVandap_Db> list_cauhoi_chuatrl = new List<CauhoiVandap_Db>();
                                var list_ctrl = await _context.cautrl_Dbs.ToListAsync();
                                foreach (var ch in list_cauhoi_baigiang)
                                {

                                    foreach (var ctrl in list_ctrl)
                                    {
                                        if (ch.CauhoiId != ctrl.CauhoiId)
                                        {
                                            list_cauhoi_chuatrl.Add(ch);
                                            break;
                                        }
                                    }
                                }
                                if (filt == 1) // Cau hoi cua toi
                                {
                                    listcauhoi = (from listch in list_cauhoi_chuatrl
                                                  where listch.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 2) //cau hoi yeu thich
                                {
                                    var list_chyt = await _context.cauhoiYeuthich_Dbs.ToListAsync();
                                    foreach (var chctrl in list_cauhoi_chuatrl)
                                    {
                                        foreach (var chyt in list_chyt)
                                        {
                                            if (chyt.CauhoiId == chctrl.CauhoiId)
                                            {
                                                listcauhoi.Add(chctrl);
                                            }
                                        }
                                    }
                                }
                                else if (filt == 0) // tat ca
                                {
                                    listcauhoi = (from listch in list_cauhoi_chuatrl
                                                  select listch).ToList();
                                }

                            }
                            else if (typeCauhoi == 3) //Cau hoi da trl
                            {
                                List<CauhoiVandap_Db> list_cauhoi_datrl = new List<CauhoiVandap_Db>();
                                var list_dtrl = await _context.cautrl_Dbs.ToListAsync();
                                foreach (var ch in list_cauhoi_baigiang)
                                {

                                    foreach (var ctrl in list_dtrl)
                                    {
                                        if (ch.CauhoiId == ctrl.CauhoiId && ctrl.UserId == user_id)
                                        {
                                            list_cauhoi_datrl.Add(ch);
                                            break;
                                        }
                                    }
                                }
                                if (filt == 1) // Cau hoi cua toi
                                {
                                    listcauhoi = (from listch in list_cauhoi_datrl
                                                  where listch.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 2) //cau hoi yeu thich
                                {
                                    var list_chyt = await _context.cauhoiYeuthich_Dbs.ToListAsync();
                                    foreach (var chdtrl in list_cauhoi_datrl)
                                    {
                                        foreach (var chyt in list_chyt)
                                        {
                                            if (chyt.CauhoiId == chdtrl.CauhoiId)
                                            {
                                                listcauhoi.Add(chdtrl);
                                            }
                                        }
                                    }
                                }
                                else if (filt == 0) // tat ca
                                {
                                    listcauhoi = (from listch in list_cauhoi_datrl
                                                  select listch).ToList();
                                }
                            }
                            else
                            {
                                throw new Exception("Phai chon loai cau hoi cu the");
                            }
                        }
                        else // tat ca bai giang
                        {
                            var list_cauhoi_baigiang = await (from bg in _context.cauhoiVandap_Dbs
                                                              where bg.LopgiangId == lop_Id
                                                              select bg).ToListAsync();
                            if (typeCauhoi == 1) // Cau hoi gan nhat
                            {
                                var list_cauhoi_typeCauhoi = (from bg in list_cauhoi_baigiang
                                                              orderby bg.Ngaytao descending
                                                              select bg).ToList();
                                if (filt == 1) // Cau hoi cua toi
                                {
                                    listcauhoi = (from listch in list_cauhoi_typeCauhoi
                                                  where listch.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 2) //cau hoi yeu thich
                                {
                                    listcauhoi = (from listch in list_cauhoi_typeCauhoi
                                                  join wishques in _context.cauhoiYeuthich_Dbs
                                                  on listch.CauhoiId equals wishques.CauhoiId
                                                  where wishques.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 0) // tat ca
                                {
                                    listcauhoi = (from listch in list_cauhoi_typeCauhoi
                                                  select listch).ToList();
                                }
                            }
                            else if (typeCauhoi == 2) //Cau hoi chua trl
                            {
                                List<CauhoiVandap_Db> list_cauhoi_chuatrl = new List<CauhoiVandap_Db>();
                                var list_ctrl = await _context.cautrl_Dbs.ToListAsync();
                                foreach (var ch in list_cauhoi_baigiang)
                                {

                                    foreach (var ctrl in list_ctrl)
                                    {
                                        if (ch.CauhoiId != ctrl.CauhoiId)
                                        {
                                            list_cauhoi_chuatrl.Add(ch);
                                            break;
                                        }
                                    }
                                }
                                if (filt == 1) // Cau hoi cua toi
                                {
                                    listcauhoi = (from listch in list_cauhoi_chuatrl
                                                  where listch.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 2) //cau hoi yeu thich
                                {
                                    var list_chyt = await _context.cauhoiYeuthich_Dbs.ToListAsync();
                                    foreach (var chctrl in list_cauhoi_chuatrl)
                                    {
                                        foreach (var chyt in list_chyt)
                                        {
                                            if (chyt.CauhoiId == chctrl.CauhoiId)
                                            {
                                                listcauhoi.Add(chctrl);
                                            }
                                        }
                                    }
                                }
                                else if (filt == 0) // tat ca
                                {
                                    listcauhoi = (from listch in list_cauhoi_chuatrl
                                                  select listch).ToList();
                                }

                            }
                            else if (typeCauhoi == 3) //Cau hoi da trl
                            {
                                List<CauhoiVandap_Db> list_cauhoi_datrl = new List<CauhoiVandap_Db>();
                                var list_dtrl = await _context.cautrl_Dbs.ToListAsync();
                                foreach (var ch in list_cauhoi_baigiang)
                                {

                                    foreach (var ctrl in list_dtrl)
                                    {
                                        if (ch.CauhoiId == ctrl.CauhoiId && ctrl.UserId == user_id)
                                        {
                                            list_cauhoi_datrl.Add(ch);
                                            break;
                                        }
                                    }
                                }
                                if (filt == 1) // Cau hoi cua toi
                                {
                                    listcauhoi = (from listch in list_cauhoi_datrl
                                                  where listch.UserId == user_id
                                                  select listch).ToList();
                                }
                                else if (filt == 2) //cau hoi yeu thich
                                {
                                    var list_chyt = await _context.cauhoiYeuthich_Dbs.ToListAsync();
                                    foreach (var chdtrl in list_cauhoi_datrl)
                                    {
                                        foreach (var chyt in list_chyt)
                                        {
                                            if (chyt.CauhoiId == chdtrl.CauhoiId)
                                            {
                                                listcauhoi.Add(chdtrl);
                                            }
                                        }
                                    }
                                }
                                else if (filt == 0) // tat ca
                                {
                                    listcauhoi = (from listch in list_cauhoi_datrl
                                                  select listch).ToList();
                                }
                            }
                            else
                            {
                                throw new Exception("Phai chon loai cau hoi cu the");
                            }
                        }

                        if (listcauhoi.Count > 0)
                        {
                            foreach (var cauhoi in listcauhoi)
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
                            foreach (var cauhoi in listmodel)
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
                        throw new Exception("Phai chon 1 lop cu the");
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

        public async Task<KqJson> Xoacautrl(Delete_Entity_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.EntityId >0 && model.User_Id >0)
                {
                    var cautrl = await _context.cautrl_Dbs.SingleOrDefaultAsync(p => p.CautrlID == model.EntityId);
                    if (cautrl != null)
                    {
                        if (cautrl.UserId == model.User_Id)  //Chi nguoi trl moi duoc xoa
                        {
                            _context.Remove(cautrl);
                        }
                        else
                        {
                            //check User la Admin 
                            var checkUser = await (from nd in _context.user_Dbs
                                                   join role in _context.role_Dbs
                                                   on nd.Role equals role.RoleId
                                                   where nd.UserID == model.User_Id &&
                                                   role.Phanquyen == 1 
                                                   select nd).FirstOrDefaultAsync();
                            if (checkUser!=null)  //Nguoi quan ly duoc phep xoa bat ky cautrl nao
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
