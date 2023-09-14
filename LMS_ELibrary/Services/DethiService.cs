using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LMS_ELibrary.Services
{
    public class DethiService : IDethiService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public DethiService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // dethi.Status = -1 -> nhap ; 0 -> cho duyet ; 1 => da duyet ; 2 -> huy yeu cau

        public async Task<Dethi_Model> chitietDethi(int id)
        {
            try
            {
                Dethi_Model dethi_Model = new Dethi_Model();
                var result = await _context.dethi_Dbs.SingleOrDefaultAsync(p => p.DethiID == id);

                var col = _context.Entry(result);
                await col.Reference(p => p.User).LoadAsync();
                User_Db user = new User_Db();
                user.UserFullname = result.User.UserFullname;
                user.UserName = result.User.UserName;
                user.Password = "***";
                user.Email = result.User.Email;
                user.Role = result.User.Role;
                user.Avt = result.User.Avt;
                user.Gioitinh = result.User.Gioitinh;
                user.Sdt = result.User.Sdt;
                user.Diachi = result.User.Diachi;

                result.User = user;

                await col.Reference(q => q.Monhoc).LoadAsync();
                Monhoc_Db monhoc = new Monhoc_Db();
                monhoc.TenMonhoc = result.Monhoc.TenMonhoc;
                monhoc.MaMonhoc = result.Monhoc.MaMonhoc;
                monhoc.Mota = result.Monhoc.Mota;
                monhoc.Tinhtrang = result.Monhoc.Tinhtrang;
                monhoc.TobomonId = result.Monhoc.TobomonId;

                result.Monhoc = monhoc;

                col.Collection(x => x.ListExQA).Load();
                List<Ex_QA_Db> listcauhoi = new List<Ex_QA_Db>();
                foreach (var collec in result.ListExQA)
                {

                    Ex_QA_Db model = new Ex_QA_Db();

                    model.QAID = collec.QAID;
                    model.DethiID = collec.DethiID;

                    listcauhoi.Add(model);

                }
                result.ListExQA = listcauhoi;




                dethi_Model = _mapper.Map<Dethi_Model>(result);

                if (dethi_Model.Status == "0")
                {
                    dethi_Model.Status = "Cho Duyet";
                }
                else if (dethi_Model.Status == "1")
                {
                    dethi_Model.Status = "Da Duyet";
                }

                foreach (var cauhoi in dethi_Model.ListExQA)
                {
                    var qa = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == cauhoi.QAID);
                    cauhoi.Cauhoi = qa.Cauhoi;
                    cauhoi.DapAn = qa.Cautrl;

                }



                return dethi_Model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Dethi_Model>> filterDethitheoMon(int id)
        {
            try
            {
                var result = await (from dethi in _context.dethi_Dbs where dethi.MonhocID == id orderby dethi.Ngaytao descending select dethi).ToListAsync();
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

                    col.Collection(x => x.ListExQA).Load();
                    List<Ex_QA_Db> listcauhoi = new List<Ex_QA_Db>();
                    foreach (var collec in item.ListExQA)
                    {

                        Ex_QA_Db model = new Ex_QA_Db();

                        model.QAID = collec.QAID;
                        model.DethiID = collec.DethiID;

                        listcauhoi.Add(model);

                    }
                    item.ListExQA = listcauhoi;


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

                    foreach (var cauhoi in dethi.ListExQA)
                    {
                        var qa = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == cauhoi.QAID);
                        cauhoi.Cauhoi = qa.Cauhoi;
                        cauhoi.DapAn = qa.Cautrl;

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
                var result = await (from dethi in _context.dethi_Dbs
                                    join mon in _context.monhoc_Dbs
                                   on dethi.MonhocID equals mon.MonhocID
                                    where mon.TobomonId == id
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
                var result = await (from dethi in _context.dethi_Dbs orderby dethi.Ngaytao descending select dethi).ToListAsync();
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

                    await col.Collection(x => x.ListExQA).LoadAsync();
                    List<Ex_QA_Db> listcauhoi = new List<Ex_QA_Db>();
                    foreach (var collec in item.ListExQA)
                    {

                        Ex_QA_Db model = new Ex_QA_Db();

                        model.QAID = collec.QAID;
                        model.DethiID = collec.DethiID;

                        listcauhoi.Add(model);

                    }
                    item.ListExQA = listcauhoi;
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

                    foreach (var cauhoi in dethi.ListExQA)
                    {
                        var qa = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == cauhoi.QAID);
                        cauhoi.Cauhoi = qa.Cauhoi;
                        cauhoi.DapAn = qa.Cautrl;

                    }
                }

                return listdethi;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Dethi_Model>> searchDethi(string madethi)
        {
            try
            {
                var result = await (from de in _context.dethi_Dbs where de.Madethi.Contains(madethi) select de).ToArrayAsync();
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

                    await col.Collection(x => x.ListExQA).LoadAsync();
                    List<Ex_QA_Db> listcauhoi = new List<Ex_QA_Db>();
                    foreach (var collec in item.ListExQA)
                    {

                        Ex_QA_Db model = new Ex_QA_Db();

                        model.QAID = collec.QAID;
                        model.DethiID = collec.DethiID;

                        listcauhoi.Add(model);

                    }
                    item.ListExQA = listcauhoi;
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

                    foreach (var cauhoi in dethi.ListExQA)
                    {
                        var qa = await _context.qA_Dbs.SingleOrDefaultAsync(p => p.QAID == cauhoi.QAID);
                        cauhoi.Cauhoi = qa.Cauhoi;
                        cauhoi.DapAn = qa.Cautrl;

                    }
                }

                return listdethi;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> tao_dethi_nganhangcauhoi(Dethi_Model dethi, List<int> idQues)
        {
            try
            {
                KqJson kq = new KqJson();
                Dethi_Db _dethi = new Dethi_Db();
                _dethi.Madethi = dethi.Madethi;
                _dethi.Status = 0;  // 0 -> cho duyet ; 1 -> da duyet
                _dethi.UserID = dethi.UserID;
                _dethi.Ngaytao = DateTime.Now;
                _dethi.MonhocID = dethi.MonhocID;

                _context.dethi_Dbs.Add(_dethi);
                await _context.SaveChangesAsync();
                int idinsert = _dethi.DethiID;

                List<Ex_QA_Db> listadd = new List<Ex_QA_Db>();

                foreach (int id in idQues)
                {
                    Ex_QA_Db QA = new Ex_QA_Db();

                    QA.DethiID = idinsert;
                    QA.QAID = id;

                    await _context.ex_QA_Dbs.AddAsync(QA);



                }

                int row = await _context.SaveChangesAsync();
                if (row > 0)
                {
                    kq.Status = true;
                    kq.Message = "Them De thi thanh cong";
                }
                else
                {

                    kq.Status = false;
                    kq.Message = "Them De thi khong thanh cong";

                }

                return kq;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> doiMadethi(int iddethi, Dethi_Model dethi)
        {
            try
            {
                KqJson kq = new KqJson();
                var result = await (from dt in _context.dethi_Dbs where dt.DethiID == iddethi && dt.Status == 1 || dt.Status == -1 select dt).SingleOrDefaultAsync();
                if (result != null)
                {
                    result.Madethi = dethi.Madethi;
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Thanh cong!";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Thay doi thay bai!";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay";
                }

                return kq;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> guiPheduyet(int iddethi)
        {
            try
            {
                KqJson kq = new KqJson();
                var result = await _context.dethi_Dbs.SingleOrDefaultAsync(p => p.DethiID == iddethi && p.Status == -1);
                if (result != null)
                {
                    result.Status = 0;
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Gui Phe Duyet Thanh cong!";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Gui That Bai!";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay";
                }

                return kq;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> deleteDethi(int id)
        {
            try
            {
                KqJson kq = new KqJson();
                var result = await (from dt in _context.dethi_Dbs 
                                    where dt.DethiID==id && dt.Status==-1 || dt.DethiID == id && dt.Status == 2
                                    select dt).SingleOrDefaultAsync();
                if (result != null)
                {
                    _context.dethi_Dbs.Remove(result);
                    int row =await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "thanh cong";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "that bai";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay";
                }
                return kq;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> tai_len_Dethi(int user_id, List<IFormFile> files)
        {
            KqJson kq = new KqJson();
            try
            {
                if(user_id!=null && files != null)
                {
                    var user = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == user_id);
                    if(user != null)
                    {
                        long size = 0;
                        string path = "";
                        List<File_Dethi_Db> listadd = new List<File_Dethi_Db>();
                        foreach (var file in files)
                        {
                            size = file.Length;
                            if (size > 0)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TaiNguyen\", fileName);

                                using (var stream = System.IO.File.Create(filePath))
                                {
                                    await file.CopyToAsync(stream);
                                    path = filePath;
                                }
                                if (path != null)
                                {
                                    File_Dethi_Db filedb = new File_Dethi_Db();
                                    filedb.Path= path;
                                    filedb.Size = size;
                                    filedb.User_Id = user_id;
                                    filedb.NgayTao = DateTime.Now;

                                    listadd.Add(filedb);
                                }
                                else
                                {
                                    throw new Exception("Tai file len that bai");
                                }
                            }
                            else
                            {
                                throw new Exception("Khong the tai file nay");
                            }
                        }
                        if (listadd.Count == files.Count)
                        {
                            await _context.file_Dethi_Db.AddRangeAsync(listadd);
                            int row = await _context.SaveChangesAsync();
                            if (row > 0)
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
                            throw new Exception("Co file khong the tai le");
                        }
                    }
                    else
                    {
                        throw new Exception("Nguoi dungn ay khong ton tai!");
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

        public async Task<KqJson> them_File_vao_Dethi(int dethi_id, File_Dethi_Db file)
        {
            KqJson kq = new KqJson();
            try
            {
                if(dethi_id!=null && file != null)
                {
                    var result = await _context.dethi_Dbs.SingleOrDefaultAsync(p=>p.DethiID==dethi_id);
                    if (result != null)
                    {
                        result.FileId = file.FileId;
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Thanh cong";

                            return kq;
                        }
                        else
                        {
                            throw new Exception("Them vao that bai");
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

        public async Task<KqJson> tao_dethi_tructiep(Tao_cauhoi_tructiep_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                
                
                if (model!=null)
                {
                    List<QA_Model> listcauhoi = model.listcauhoi;
                    Dethi_Db dt = new Dethi_Db();
                    dt.Madethi = model.Madethi;
                    dt.Status = -1;
                    dt.UserID = model.UserId;
                    dt.Ngaytao = DateTime.Now;
                    dt.MonhocID = model.MonhocId;

                    await _context.AddAsync(dt);
                    await _context.SaveChangesAsync();
                    int id_dethi = dt.DethiID;

                    List<int> list_idcauhoi = new List<int>();
                    foreach (QA_Model cauhoi in listcauhoi)
                    {
                        QA_Db qa = new QA_Db();
                        qa.Cauhoi = cauhoi.Cauhoi;
                        qa.Cautrl = cauhoi.Cautrl;
                        qa.MonhocID = model.MonhocId;
                        qa.Lancuoisua = DateTime.Now;

                        await _context.qA_Dbs.AddAsync(qa);
                        await _context.SaveChangesAsync();
                        list_idcauhoi.Add(qa.QAID);
                    }


                    List<Ex_QA_Db> list_exqa = new List<Ex_QA_Db>();
                    
                    foreach (int id in list_idcauhoi)
                    {
                        Ex_QA_Db exqa = new Ex_QA_Db();
                        exqa.DethiID = id_dethi;
                        exqa.QAID = id;

                        list_exqa.Add(exqa);
                    }

                    await _context.ex_QA_Dbs.AddRangeAsync(list_exqa);


                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        
                        
                        kq.Status = true;
                        kq.Message = "Thanh cong";

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
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }
    }
}
