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
                var result = await _context.tailieu_Baigiang_Dbs.ToListAsync();
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
            catch (Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
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
                                    where baigiang.UserId == id && baigiang.TenDoc.Contains(key)
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

        public async Task<KqJson> TaomoiBaigiang(Taomoi_Baigiang_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Nguoitao_Id > 0 && model.Monhoc_Id > 0 && model.Chude_Id > 0 && model.File_Baigiang_Id > 0 && model.Tieude != "")
                {
                    Tailieu_Baigiang_Db _baigiang = new Tailieu_Baigiang_Db();
                    _baigiang.UserId = model.Nguoitao_Id;
                    _baigiang.TenDoc = model.Tieude;
                    _baigiang.MonhocID = model.Monhoc_Id;
                    _baigiang.ChudeID = model.Chude_Id;
                    _baigiang.File_Baigiang_Id = model.File_Baigiang_Id;
                    _baigiang.Mota = model.Mota != "" ? _baigiang.Mota = model.Mota : _baigiang.Mota = null;
                    _baigiang.Status = -1;
                    await _context.tailieu_Baigiang_Dbs.AddAsync(_baigiang);
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

        public async Task<KqJson> Taotainguyen_cho_Baigiang(Taotainguyen_Baigiang_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.User_Id > 0 && model.Baigiang_Id > 0 && model.List_Tainguyen_Id.Count > 0)
                {
                    List<Tainguyen_Db> listadd = new List<Tainguyen_Db>();
                    foreach (int tainguyen_id in model.List_Tainguyen_Id)
                    {
                        Tainguyen_Db _tainguyen = new Tainguyen_Db();
                        _tainguyen.Baigiang_Id = model.Baigiang_Id;
                        _tainguyen.File_Tainguyen_Id = tainguyen_id;
                        _tainguyen.Nguoitao_Id = model.User_Id;
                        _tainguyen.Ngaytao = DateTime.Now;
                        listadd.Add(_tainguyen);
                    }
                    await _context.tainguyen_Dbs.AddRangeAsync(listadd);
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
        public async Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu)
        {
            try
            {
                var _tailieu = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == id);
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
        //Type= 1 -> Baigiang ; 2 -> Tainguyen
        public async Task<KqJson> tai_len_Tai_Nguyen(int user_id, List<IFormFile> files)
        {
            KqJson kq = new KqJson();
            try
            {
                if (user_id > 0 && files != null)
                {

                    List<File_Tailen_Db> listadd = new List<File_Tailen_Db>();
                    foreach (var file in files)
                    {
                        string path = "";
                        double size = file.Length;
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TaiNguyen\", fileName);
                        File_Tailen_Db _filetailen = new File_Tailen_Db();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                            path = filePath;
                        }
                        if (path != null)
                        {
                            _filetailen.Nguoitailen_Id = user_id;
                            _filetailen.Tenfile = fileName;
                            _filetailen.Ngaytailen = DateTime.Now;
                            _filetailen.Status = -1; // status =-1 -> chua duyet ; 0 -> dang duyet ; 1 -> da duyet
                            _filetailen.Type = 2;  // type = 2 -> tainguyen ; 1-> baigiang
                            _filetailen.Path = path;
                            _filetailen.Size = size;

                        }
                        listadd.Add(_filetailen);
                    }
                    await _context.file_Tailen_Dbs.AddRangeAsync(listadd);
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

                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> tai_len_Bai_Giang(int user_id, List<IFormFile> files)
        {
            KqJson kq = new KqJson();
            try
            {
                if (user_id > 0 && files != null)
                {

                    List<File_Tailen_Db> listadd = new List<File_Tailen_Db>();
                    foreach (var file in files)
                    {
                        string path = "";
                        double size = file.Length;
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TaiNguyen\", fileName);
                        File_Tailen_Db _filetailen = new File_Tailen_Db();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                            path = filePath;
                        }
                        if (path != null)
                        {
                            _filetailen.Nguoitailen_Id = user_id;
                            _filetailen.Tenfile = fileName;
                            _filetailen.Ngaytailen = DateTime.Now;
                            _filetailen.Status = -1; // status =-1 -> chua duyet ; 0 -> dang duyet ; 1 -> da duyet
                            _filetailen.Type = 1;  // type = 2 -> tainguyen ; 1-> baigiang
                            _filetailen.Path = path;
                            _filetailen.Size = size;

                        }
                        listadd.Add(_filetailen);
                    }
                    await _context.file_Tailen_Dbs.AddRangeAsync(listadd);
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

                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<KqJson> delTailieu(Delete_Entity_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.EntityId > 0)
                {
                    var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == model.EntityId);
                    if (result != null)
                    {
                        _context.tailieu_Baigiang_Dbs.Remove(result);
                        int num_row = await _context.SaveChangesAsync();
                        if (num_row > 0)
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
                    throw new Exception("Khong co doi tuong nay");
                }
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
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
                if (status == -1 || status == 0 || status == 1)
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
            }
            catch (Exception e)
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
                        result.Status = model.Status;

                        result.NgayDuyet = DateTime.Now;
                        result.Nguoiduyet = model.ID_Nguoiduyet;

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
            }
            catch (Exception e)
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

                    var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
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

        public async Task<object> list_Tailieu_Monhoc_status(int monId, string? status)
        {
            try
            {

                int trangthai = int.Parse(status);

                if (monId > 0)
                {

                    var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
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

                    var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
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

        public async Task<object> Xem_File_theo_Mon(int monhoc_id)
        {
            try
            {
                if (monhoc_id > 0)
                {
                    var listchude = await (from cd in _context.chude_Dbs 
                                           where cd.Monhoc_Id == monhoc_id 
                                           select cd).ToListAsync();
                    if (listchude.Count > 0)
                    {
                        List<Tailieu_Baigiang_Db> listbaigiang = new List<Tailieu_Baigiang_Db>();
                        foreach(var chude in listchude)
                        {
                            listbaigiang = await (from bg in _context.tailieu_Baigiang_Dbs
                                                where bg.ChudeID == chude.ChudeID
                                                select bg).ToListAsync();
                            
                        }
                        if (listbaigiang.Count > 0)
                        {
                            List<File_Tailen_Db> listfile = new List<File_Tailen_Db>();
                            List<int?> list_file_id = new List<int?>();
                            foreach(var baig in listbaigiang)
                            {
                                list_file_id.Add(baig.File_Baigiang_Id);
                                var listtn = await (from tn in _context.tainguyen_Dbs
                                                    where tn.Baigiang_Id == baig.DocId
                                                    select tn).ToListAsync();
                                foreach(var tain in listtn)
                                {
                                    list_file_id.Add(tain.File_Tainguyen_Id);
                                }
                            }
                            //return list_file_id;
                            foreach (int id in list_file_id)
                            {
                                var filetailen = await (from file in _context.file_Tailen_Dbs
                                                        where file.File_Tailen_Id == id
                                                        select file).SingleOrDefaultAsync();
                                listfile.Add(filetailen);
                            }
                            if (listfile.Count > 0)
                            {
                                //List<File_Tailen_Model> list_file_model = new List<File_Tailen_Model>();
                                //list_file_model = _mapper.Map<List<File_Tailen_Model>>(listfile);
                                //foreach (var file in list_file_model)
                                //{
                                //    if (file.Type == "1")
                                //    {
                                //        file.Type = "Bai giang";
                                //    }
                                //    else if (file.Type == "2")
                                //    {
                                //        file.Type = "Tai nguyen";
                                //    }

                                //    if (file.Status == "-1")
                                //    {
                                //        file.Status = "Luu nhap";
                                //    }
                                //    else if (file.Status == "0")
                                //    {
                                //        file.Status = "Cho duyet";
                                //    }
                                //    else if (file.Status == "1")
                                //    {
                                //        file.Status = "Da duyet";
                                //    }
                                //    else if (file.Status == "2")
                                //    {
                                //        file.Status = "Da huy";
                                //    }
                                //}
                                return listfile;
                            }
                            else
                            {
                                throw new Exception("Khong co file nao");
                            }

                        }
                        else
                        {
                            throw new Exception("Mon hoc nay khong co file nao");
                        }
                    }
                    else
                    {
                        throw new Exception("Mon hoc nay khong co file nao");
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

        public async Task<KqJson> XetduyetFile(Xetduyet_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.ID_Canduyet > 0 && model.ID_Nguoiduyet > 0)
                {
                    if (model.Status == 1 || model.Status == 2)
                    {
                        var checkUser = await (from nd in _context.user_Dbs
                                               join role in _context.role_Dbs
                                               on nd.Role equals role.RoleId
                                               where nd.UserID == model.ID_Nguoiduyet
                                               select nd).SingleOrDefaultAsync();
                        if (checkUser != null)
                        {
                            var result = await (from file in _context.file_Tailen_Dbs
                                                where file.File_Tailen_Id == model.ID_Canduyet
                                                select file).SingleOrDefaultAsync();
                            if (result != null)
                            {
                                result.Status = model.Status;
                                result.Ngayduyet = DateTime.Now;
                                result.Nguoiduyet_Id = model.ID_Nguoiduyet;
                                result.Ghichu = model.Ghichu != "" ? result.Ghichu = model.Ghichu : result.Ghichu = null;
                                int row = await _context.SaveChangesAsync();
                                if (row > 0)
                                {
                                    kq.Status = true;
                                    kq.Message = "Duyet thanh cong";
                                    return kq;
                                }
                                else
                                {
                                    throw new Exception("Duyet that bai");
                                }
                            }
                            else
                            {
                                throw new Exception("Not Found");
                            }
                        }
                        else
                        {
                            throw new Exception("Khong du quyen de duyet");
                        }
                    }
                    else
                    {
                        throw new Exception("Trang thai duyet khong phu hop");
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
