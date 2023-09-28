using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LMS_ELibrary.Services
{
    public class TailieuService : ITailieuService
    {
        private readonly LMS_ELibraryContext _context;
        private readonly IMapper _mapper;
        public TailieuService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetAlltailieu()
        {
            try
            {
                var result=await _context.tailieu_Baigiang_Dbs.ToListAsync();
                if (result.Count > 0)
                {
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
                    }
                    List<Tailieu_Baigiang_Model> listtialieu = new List<Tailieu_Baigiang_Model>();
                    listtialieu = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                    foreach (var item in listtialieu)
                    {



                        if (item.Type == "0")
                        {
                            item.Type = "Tai Nguyen";
                        }
                        else if (item.Type == "1")
                        {
                            item.Type = "Bai Giang";
                        }

                        if (item.Status == "0")
                        {
                            item.Status = "Cho Duyet";
                        }
                        else if (item.Status == "1")
                        {
                            item.Status = "Da duyet";
                        }
                        else if (item.Status == "-1")
                        {
                            item.Status = "Chua duyet"; //chua gui phe duyet
                        }
                        if (item.User.Role == "0")
                        {
                            item.User.Role = "Quan ly";
                        }
                        else if (item.User.Role == "1")
                        {
                            item.User.Role = "Giao vien";
                        }
                        else
                        {
                            item.User.Role = "Hoc sinh";
                        }
                        if (item.User.Gioitinh == "True")
                        {
                            item.User.Gioitinh = "Nam";
                        }
                        else
                        {
                            item.User.Gioitinh = "Nu";
                        }
                    }

                    return listtialieu;
                }
                else
                {
                    throw new Exception("Not Found");
                }
            }
            catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message=e.Message;
                return kq;
            }
        }

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> Tailieucuatoi(int id)
        {
            try
            {
                var result = await (from tailieu in _context.tailieu_Baigiang_Dbs where tailieu.UserId == id select tailieu).ToListAsync();
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
                }
                List<Tailieu_Baigiang_Model> listtialieu = new List<Tailieu_Baigiang_Model>();
                listtialieu = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listtialieu)
                {



                    if (item.Type == "0")
                    {
                        item.Type = "Tai Nguyen";
                    }
                    else if (item.Type == "1")
                    {
                        item.Type = "Bai Giang";
                    }

                    if (item.Status == "0")
                    {
                        item.Status = "Cho Duyet";
                    }
                    else if (item.Status == "1")
                    {
                        item.Status = "Da duyet";
                    }
                    else if (item.Status == "-1")
                    {
                        item.Status = "Chua duyet"; //chua gui phe duyet
                    }
                    if (item.User.Role == "0")
                    {
                        item.User.Role = "Quan ly";
                    }
                    else if (item.User.Role == "1")
                    {
                        item.User.Role = "Giao vien";
                    }
                    else
                    {
                        item.User.Role = "Hoc sinh";
                    }
                    if (item.User.Gioitinh == "True")
                    {
                        item.User.Gioitinh = "Nam";
                    }
                    else
                    {
                        item.User.Gioitinh = "Nu";
                    }
                }

                return listtialieu;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> searchBaigiang(int id, string key)
        {
            try
            {
                var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
                                    where baigiang.Type == 1 && baigiang.UserId == id && baigiang.TenDoc.Contains(key)
                                    orderby baigiang.Sualancuoi descending
                                    select baigiang).ToListAsync();
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
                List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listbaigiang)
                {
                    item.Type = "Bai giang";
                    if (item.Status == "0")
                    {
                        item.Status = "Cho Duyet";
                    }
                    else if (item.Status == "1")
                    {
                        item.Status = "Da duyet";
                    }
                    if (item.User.Role == "0")
                    {
                        item.User.Role = "Quan ly";
                    }
                    else if (item.User.Role == "1")
                    {
                        item.User.Role = "Giao vien";
                    }
                    else
                    {
                        item.User.Role = "Hoc sinh";
                    }
                    if (item.User.Gioitinh == "True")
                    {
                        item.User.Gioitinh = "Nam";
                    }
                    else
                    {
                        item.User.Gioitinh = "Nu";
                    }
                }

                return listbaigiang;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };
        }

        public async Task<object> list_Tailieu_Monhoc(int monId, string status, string type)
        {
            try
            {
                int trangthai = int.Parse(status);
                int loai = int.Parse(type);
                
                if (monId > 0)
                {
                    
                     var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
                                             where baigiang.MonhocID==monId && baigiang.Status == trangthai && baigiang.Type == loai
                                             orderby baigiang.Sualancuoi descending
                                             select baigiang).ToListAsync();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            var col = _context.Entry(item);
                            await col.Reference(p => p.User).LoadAsync();
                            User_Db user = new User_Db();
                            user.UserFullname = item.User.UserFullname;
                            user.UserName = item.User.UserName;
                            user.Password ="***";
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
                            monhoc.UserId = item.Monhoc.UserId;
                            item.Monhoc = monhoc;

                        }
                        List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                        listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                        foreach (var item in listbaigiang)
                        {
                            
                            if (item.Type == "0")
                            {
                                item.Type = "Tai Nguyen";
                            }
                            else if (item.Type == "1")
                            {
                                item.Type = "Bai giang";
                            }
                            if (item.Status == "0")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Da duyet";
                            }
                            if (item.User.Role == "0")
                            {
                                item.User.Role = "Quan ly";
                            }
                            else if (item.User.Role == "1")
                            {
                                item.User.Role = "Giao vien";
                            }
                            else
                            {
                                item.User.Role = "Hoc sinh";
                            }
                            if (item.User.Gioitinh == "True")
                            {
                                item.User.Gioitinh = "Nam";
                            }
                            else
                            {
                                item.User.Gioitinh = "Nu";
                            }
                        }

                        return listbaigiang;
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
            };
        }

        public async Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu)
        {
            try
            {
                var _tailieu = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == id && p.Type == 0);
                KqJson kq = new KqJson();
                if (_tailieu != null)
                {
                    _tailieu.TenDoc = tailieu.TenDoc != null ? _tailieu.TenDoc = tailieu.TenDoc : _tailieu.TenDoc;
                    _tailieu.UserId = tailieu.UserID != null ? _tailieu.UserId = tailieu.UserID : _tailieu.UserId;
                    _tailieu.MonhocID = tailieu.MonhocID != null ? _tailieu.MonhocID = tailieu.MonhocID : _tailieu.MonhocID;
                    _tailieu.ChudeID = tailieu.ChudeID != null ? _tailieu.ChudeID = tailieu.ChudeID : _tailieu.ChudeID;
                    //_tailieu.Kichthuoc = tailieu.Kichthuoc != null ? _tailieu.Kichthuoc = tailieu.Kichthuoc : _tailieu.Kichthuoc;
                    //_tailieu.Path = tailieu.Path != null ? _tailieu.Path = tailieu.Path : _tailieu.Path;
                    //_tailieu.Status = tailieu.Status != null ? _tailieu.Status = int.Parse(tailieu.Status) : _tailieu.Status;
                    //_tailieu.Type = tailieu.Type != null ? _tailieu.Type = int.Parse(tailieu.Type) : _tailieu.Type;
                    _tailieu.Sualancuoi = DateTime.Now;

                    int row_edit = await _context.SaveChangesAsync();
                    if (row_edit > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Sua tai lieu thanh cong";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Sua tai lieu khong thanh cong";
                    }
                }
                else
                {
                    kq.Status = false;
                    kq.Message = "Khong tim thay tai lieu";
                }


                return kq;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> tai_len_Tai_Nguyen(int user_id, List<IFormFile> files)
        {
            try
            {
                if (user_id != null && files != null)
                {
                    KqJson kq = new KqJson();
                    List<Tailieu_Baigiang_Db> listadd = new List<Tailieu_Baigiang_Db>();
                    foreach (var file in files)
                    {
                        string path = "";
                        double size = file.Length;
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TaiNguyen\", fileName);
                        Tailieu_Baigiang_Db _tailieu = new Tailieu_Baigiang_Db();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                            path = filePath;
                        }
                        if (path != null)
                        {
                            _tailieu.UserId = user_id;
                            _tailieu.TenDoc = fileName;
                            _tailieu.Sualancuoi = DateTime.Now;
                            _tailieu.Status = -1; // status =-1 -> chua duyet ; 0 -> dang duyet ; 1 -> da duyet
                            _tailieu.Type = 0;  // type = 0 -> tainguyen ; 1-> baigiang
                            _tailieu.Path = path;
                            _tailieu.Kichthuoc = size;

                        }
                        listadd.Add(_tailieu);
                    }
                    await _context.tailieu_Baigiang_Dbs.AddRangeAsync(listadd);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Tai len thanh cong";
                    }
                    else
                    {
                        throw new Exception("Tai len that bai!");
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

        public async Task<KqJson> tai_len_Bai_Giang(int user_id, List<IFormFile> files)
        {
            try
            {
                if (user_id != null && files != null)
                {
                    KqJson kq = new KqJson();
                    List<Tailieu_Baigiang_Db> listadd = new List<Tailieu_Baigiang_Db>();
                    foreach (var file in files)
                    {
                        string path = "";
                        double size = file.Length;
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TaiNguyen\", fileName);
                        Tailieu_Baigiang_Db _tailieu = new Tailieu_Baigiang_Db();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                            path = filePath;
                        }
                        if (path != null)
                        {
                            _tailieu.UserId = user_id;
                            _tailieu.TenDoc = fileName;
                            _tailieu.Sualancuoi = DateTime.Now;
                            _tailieu.Status = -1; // status = -1 -> chua duyet ; 0 -> dang duyet ; 1 -> da duyet
                            _tailieu.Type = 1;  // type = 0 -> tainguyen ; 1-> baigiang
                            _tailieu.Path = path;
                            _tailieu.Kichthuoc = size;

                        }
                        listadd.Add(_tailieu);
                    }
                    await _context.tailieu_Baigiang_Dbs.AddRangeAsync(listadd);
                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Tai len thanh cong";
                    }
                    else
                    {
                        throw new Exception("Tai len that bai!");
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

        public async Task<KqJson> delTailieu(int id)
        {
            try
            {
                KqJson kq = new KqJson();

                var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == id && p.Type == 0);
                if (result != null)
                {
                    _context.tailieu_Baigiang_Dbs.Remove(result);
                    int num_row = await _context.SaveChangesAsync();
                    if (num_row > 0)
                    {
                        kq.Status = true;
                        kq.Message = "Xoa thanh cong";
                    }
                    else
                    {
                        kq.Status = false;
                        kq.Message = "Xoa that bai";
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

        public async Task<object> them_vao_Monhoc_va_Chude(Gui_pheduyet_tailieu_Request_DTO model)
        {
            try
            {
                if (model != null)
                {

                    var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == model.Tailieu_Id);
                    if (result != null)
                    {
                        result.MonhocID = model.Monhoc_Id;
                        result.ChudeID = model.Chude_Id;
                        result.TenDoc = model.TenTailieu != null ? result.TenDoc = model.TenTailieu : result.TenDoc;
                        result.Sualancuoi = DateTime.Now;
                        result.Status = 0;
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }

                    int row = await _context.SaveChangesAsync();
                    if (row > 0)
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("Gui that bai");
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

        public async Task<object> XemTailieutheoTrangthai(int status)
        {
            KqJson kq = new KqJson();
            try
            {
                if (status == -1 || status==0 || status == 1)
                {
                    var result = await (from tl in _context.tailieu_Baigiang_Dbs
                                        where tl.Status == status
                                        select tl).ToListAsync();
                    if (result.Count > 0)
                    {
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
                        }
                        List<Tailieu_Baigiang_Model> listtialieu = new List<Tailieu_Baigiang_Model>();
                        listtialieu = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                        foreach (var item in listtialieu)
                        {



                            if (item.Type == "0")
                            {
                                item.Type = "Tai Nguyen";
                            }
                            else if (item.Type == "1")
                            {
                                item.Type = "Bai Giang";
                            }

                            if (item.Status == "0")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Da duyet";
                            }
                            else if (item.Status == "-1")
                            {
                                item.Status = "Chua duyet"; //chua gui phe duyet
                            }
                            
                            if (item.User.Gioitinh == "True")
                            {
                                item.User.Gioitinh = "Nam";
                            }
                            else
                            {
                                item.User.Gioitinh = "Nu";
                            }
                        }

                        return listtialieu;
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

        public async Task<KqJson> XetDuyetTaiLieu(Xetduyet_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Status == -1 || model.Status == 1 && model.ID_Canduyet != 0)
                {
                    var result = await (from tl in _context.tailieu_Baigiang_Dbs
                                        where tl.DocId == model.ID_Canduyet && tl.Status == 0
                                        select tl).SingleOrDefaultAsync();
                    if (result != null)
                    {
                        if (model.Ghichu != "")
                        {
                            result.Ghichu = model.Ghichu;
                        }
                        result.Status=model.Status;
                        if (model.Status == 1 && model.ID_Nguoiduyet!=0)
                        {
                            result.NgayDuyet = DateTime.Now;
                            result.Nguoiduyet = model.ID_Nguoiduyet;
                        }
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Xet duyet thanh cong";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Xet duyet that bai");
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

        public async Task<object> list_Tailieu_Monhoc(int monId, string? type)
        {
            try
            {
               
                int loai = int.Parse(type);

                if (monId > 0)
                {

                    var result = await(from baigiang in _context.tailieu_Baigiang_Dbs
                                       where baigiang.MonhocID == monId && baigiang.Type == loai
                                       orderby baigiang.Sualancuoi descending
                                       select baigiang).ToListAsync();
                    if (result.Count > 0)
                    {
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
                            monhoc.UserId = item.Monhoc.UserId;
                            item.Monhoc = monhoc;

                        }
                        List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                        listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                        foreach (var item in listbaigiang)
                        {

                            if (item.Type == "0")
                            {
                                item.Type = "Tai Nguyen";
                            }
                            else if (item.Type == "1")
                            {
                                item.Type = "Bai giang";
                            }
                            if (item.Status == "0")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Da duyet";
                            }
                            if (item.User.Role == "0")
                            {
                                item.User.Role = "Quan ly";
                            }
                            else if (item.User.Role == "1")
                            {
                                item.User.Role = "Giao vien";
                            }
                            else
                            {
                                item.User.Role = "Hoc sinh";
                            }
                            if (item.User.Gioitinh == "True")
                            {
                                item.User.Gioitinh = "Nam";
                            }
                            else
                            {
                                item.User.Gioitinh = "Nu";
                            }
                        }

                        return listbaigiang;
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
            };
        }

        public async Task<object> list_Tailieu_Monhoc_status(int monId, string? status)
        {
            try
            {

                int trangthai = int.Parse(status);

                if (monId > 0)
                {

                    var result = await(from baigiang in _context.tailieu_Baigiang_Dbs
                                       where baigiang.MonhocID == monId && baigiang.Status == trangthai
                                       orderby baigiang.Sualancuoi descending
                                       select baigiang).ToListAsync();
                    if (result.Count > 0)
                    {
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
                            monhoc.UserId = item.Monhoc.UserId;
                            item.Monhoc = monhoc;

                        }
                        List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                        listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                        foreach (var item in listbaigiang)
                        {

                            if (item.Type == "0")
                            {
                                item.Type = "Tai Nguyen";
                            }
                            else if (item.Type == "1")
                            {
                                item.Type = "Bai giang";
                            }
                            if (item.Status == "0")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Da duyet";
                            }
                            if (item.User.Role == "0")
                            {
                                item.User.Role = "Quan ly";
                            }
                            else if (item.User.Role == "1")
                            {
                                item.User.Role = "Giao vien";
                            }
                            else
                            {
                                item.User.Role = "Hoc sinh";
                            }
                            if (item.User.Gioitinh == "True")
                            {
                                item.User.Gioitinh = "Nam";
                            }
                            else
                            {
                                item.User.Gioitinh = "Nu";
                            }
                        }

                        return listbaigiang;
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
            };
        }

        public async Task<object> list_Tailieu_Monhoc(int monId)
        {
            try
            {
                if (monId > 0)
                {

                    var result = await(from baigiang in _context.tailieu_Baigiang_Dbs
                                       where baigiang.MonhocID == monId 
                                       orderby baigiang.Sualancuoi descending
                                       select baigiang).ToListAsync();
                    if (result.Count > 0)
                    {
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
                            monhoc.UserId = item.Monhoc.UserId;
                            item.Monhoc = monhoc;

                        }
                        List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                        listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                        foreach (var item in listbaigiang)
                        {

                            if (item.Type == "0")
                            {
                                item.Type = "Tai Nguyen";
                            }
                            else if (item.Type == "1")
                            {
                                item.Type = "Bai giang";
                            }
                            if (item.Status == "0")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Da duyet";
                            }
                            if (item.User.Role == "0")
                            {
                                item.User.Role = "Quan ly";
                            }
                            else if (item.User.Role == "1")
                            {
                                item.User.Role = "Giao vien";
                            }
                            else
                            {
                                item.User.Role = "Hoc sinh";
                            }
                            if (item.User.Gioitinh == "True")
                            {
                                item.User.Gioitinh = "Nam";
                            }
                            else
                            {
                                item.User.Gioitinh = "Nu";
                            }
                        }

                        return listbaigiang;
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
            };
        }
    }
}
