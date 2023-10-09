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
        /*
         * Trang thai duyet
         * 1 -> Luu nhap (Chua gui yeu cau duyet)
         * 2 -> Cho duyet (Da gui yeu cau duyet)
         * 3 -> Da duyet
         * 4 -> Tu choi duyet (Admin khong duyet)
         */
        public async Task<object> GetAllBaigiang()
        {
            try
            {
                var result = await _context.tailieu_Baigiang_Dbs.OrderByDescending(p => p.Sualancuoi).ToListAsync();
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        var col = _context.Entry(item);
                        await col.Reference(p => p.User).LoadAsync();
                        await col.Collection(p => p.list_Tainguyen).LoadAsync();
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
                        List<Tainguyen_Db> listtn = new List<Tainguyen_Db>();
                        foreach (var tn in item.list_Tainguyen)
                        {

                            Tainguyen_Db tainguyen = new Tainguyen_Db();
                            tainguyen.Baigiang_Id = tn.Baigiang_Id;
                            tainguyen.Ngaytao = tn.Ngaytao;
                            tainguyen.Status = tn.Status;
                            tainguyen.Nguoitao_Id = tn.Nguoitao_Id;
                            tainguyen.File_Tainguyen_Id = tn.File_Tainguyen_Id;
                            tainguyen.Nguoiduyet_Id = tn.Nguoiduyet_Id;
                            tainguyen.Ngayduyet = tn.Ngayduyet;
                            tainguyen.Ghichu = tn.Ghichu;
                            listtn.Add(tainguyen);
                        }
                        item.list_Tainguyen = listtn;
                    }
                    List<Tailieu_Baigiang_Model> listtialieu = new List<Tailieu_Baigiang_Model>();
                    listtialieu = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                    foreach (var item in listtialieu)
                    {
                        if (item.Status == "2")
                        {
                            item.Status = "Cho Duyet";
                        }
                        else if (item.Status == "3")
                        {
                            item.Status = "Da duyet";
                        }
                        else if (item.Status == "1")
                        {
                            item.Status = "Chua duyet"; //chua gui phe duyet
                        }
                        else if (item.Status == "4")
                        {
                            item.Status = "Tu choi"; //Admin khong duyet
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

        public async Task<object> Tailieucuatoi(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await (from file in _context.file_Tailen_Dbs
                                        where file.Nguoitailen_Id == id
                                        orderby file.Ngaytailen descending
                                        select file).ToListAsync();
                    if (result != null)
                    {

                        List<File_Tailen_Model> listtialieu = new List<File_Tailen_Model>();
                        listtialieu = _mapper.Map<List<File_Tailen_Model>>(result);
                        foreach (var item in listtialieu)
                        {
                            if (item.Type == "2")
                            {
                                item.Type = "Tai Nguyen";
                            }
                            else if (item.Type == "1")
                            {
                                item.Type = "Bai Giang";
                            }

                            if (item.Status == "2")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "3")
                            {
                                item.Status = "Da duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Chua duyet"; //chua gui phe duyet
                            }
                            else if (item.Status == "4")
                            {
                                item.Status = "Tu Choi duyet"; //Admin khong duyet
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
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }

        }

        public async Task<object> searchBaigiang(int id, string key)
        {
            try
            {
                if (id > 0)
                {
                    var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
                                        where baigiang.UserId == id && baigiang.TenDoc.Contains(key)
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

                            item.Monhoc = monhoc;

                            await col.Collection(p => p.list_Tainguyen).LoadAsync();
                            List<Tainguyen_Db> listtn = new List<Tainguyen_Db>();
                            foreach (var tn in item.list_Tainguyen)
                            {

                                Tainguyen_Db tainguyen = new Tainguyen_Db();
                                tainguyen.Baigiang_Id = tn.Baigiang_Id;
                                tainguyen.Ngaytao = tn.Ngaytao;
                                tainguyen.Status = tn.Status;
                                tainguyen.Nguoitao_Id = tn.Nguoitao_Id;
                                tainguyen.File_Tainguyen_Id = tn.File_Tainguyen_Id;
                                tainguyen.Nguoiduyet_Id = tn.Nguoiduyet_Id;
                                tainguyen.Ngayduyet = tn.Ngayduyet;
                                tainguyen.Ghichu = tn.Ghichu;
                                listtn.Add(tainguyen);
                            }
                            item.list_Tainguyen = listtn;

                        }
                        List<Tailieu_Baigiang_Model> listbaigiang = new List<Tailieu_Baigiang_Model>();
                        listbaigiang = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                        foreach (var item in listbaigiang)
                        {
                            item.Type = "Bai giang";
                            if (item.Status == "2")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "3")
                            {
                                item.Status = "Da duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Chua duyet"; //chua gui phe duyet
                            }
                            else if (item.Status == "4")
                            {
                                item.Status = "Tu choi"; //Admin khong duyet
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
                    _baigiang.Sualancuoi = DateTime.Now;
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
        public async Task<KqJson> editTailieu(Edit_Baigiang_Tainguyen_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Id_Edit > 0 && model.Name != "")
                {
                    var _tailieu = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == model.Id_Edit);
                    if (_tailieu != null)
                    {
                        if (_tailieu.Status == 3)
                        {
                            throw new Exception("Tai lieu nay da duyet! Khong the doi ten");
                        }
                        else
                        {
                            _tailieu.TenDoc = model.Name;
                            _tailieu.Sualancuoi = DateTime.Now;

                            int row_edit = await _context.SaveChangesAsync();
                            if (row_edit > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Sua tai lieu thanh cong";
                                return kq;
                            }
                            else
                            {
                                throw new Exception("Sua that bai");
                            }
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
                if (model.EntityId > 0 && model.User_Id>0)
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
                        
                        var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p => p.DocId == model.EntityId);
                        if (result != null)
                        {
                            // chi xoa duoc Baigiang co tinhtrang = 1 (nhap) or 4 (Admin khong duyet)
                            if (result.Status == 1 || result.Status == 4)
                            {
                                if (quyen.Phanquyen == 2)
                                {
                                    if (result.UserId != model.User_Id)
                                    {
                                        throw new Exception("Khong du quyen de xoa mon hoc nay");
                                    }
                                }
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
                                throw new Exception("Khong the xoa bai giang o tinh trang nay");
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

        public async Task<object> XemBaigiangtheoTrangthai(int status)
        {
            KqJson kq = new KqJson();
            try
            {
                if (status == 1 || status == 2 || status == 3 || status == 4)
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
                            item.Type = "Bai Giang";

                            if (item.Status == "2")
                            {
                                item.Status = "Cho Duyet";
                            }
                            else if (item.Status == "3")
                            {
                                item.Status = "Da duyet";
                            }
                            else if (item.Status == "1")
                            {
                                item.Status = "Chua duyet"; //chua gui phe duyet
                            }
                            else if (item.Status == "3")
                            {
                                item.Status = "Tu choi duyet"; //Admin khong duyet
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

        public async Task<KqJson> XetDuyetBaigiang(Xetduyet_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Status == -1 || model.Status == 1 && model.ID_Canduyet != 0 && model.ID_Nguoiduyet>0)
                {
                    var checkUser = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.ID_Nguoiduyet &&
                                           role.Phanquyen == 1
                                           select nd).SingleOrDefaultAsync();
                    if (checkUser != null)
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
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }


        public async Task<object> Xem_File_theo_Mon(int monhoc_id, int option, int status)
        {
            try
            {
                if (monhoc_id > 0)
                {
                    /*
                     * option = 0(khong chon) -> xem tat ca
                     * option = 1 -> xem file bai giang
                     * option = 2 -> xem file tai nguyen
                     */
                    /*
                     * Status = 0(khong chon) -> Tat ca
                     * Status = 1 -> Luu nhap
                     * Status = 2 -> Dang duyet (Gui yeu cau duyet)
                     * Status = 3 -> Da duyet
                     * Status = 4 -> Tu choi (Admin tu choi duyet)
                     */
                    List<File_Tailen_Db> listfile = new List<File_Tailen_Db>();

                    if (option == 0)
                    {
                        string query = "exec Xemfiletrongmonhoc @monhoc_id=" + monhoc_id;
                        var result = _context.file_Tailen_Dbs.FromSqlRaw(query).AsEnumerable().ToList();
                        if (status < 0)
                        {
                            throw new Exception("Trang thai khong phu hop");
                        }
                        else if (status == 0)
                        {
                            listfile = result;
                        }
                        else if (status > 0)
                        {
                            listfile = (from f in result where f.Status == status select f).ToList();
                        }


                    }
                    else if (option == 1)
                    {
                        var result = await (from c in _context.chude_Dbs
                                            join t in _context.tailieu_Baigiang_Dbs
                                            on c.ChudeID equals t.ChudeID
                                            join f in _context.file_Tailen_Dbs
                                            on t.File_Baigiang_Id equals f.File_Tailen_Id
                                            where c.Monhoc_Id == monhoc_id
                                            select f).ToListAsync();
                        if (status < 0)
                        {
                            throw new Exception("Trang thai khong phu hop");
                        }
                        else if (status == 0)
                        {
                            listfile = result;
                        }
                        else if (status > 0)
                        {
                            listfile = (from f in result where f.Status == status select f).ToList();
                        }
                    }
                    else if (option == 2)
                    {
                        var result = await (from c in _context.chude_Dbs
                                            join t in _context.tailieu_Baigiang_Dbs
                                            on c.ChudeID equals t.ChudeID
                                            join tn in _context.tainguyen_Dbs
                                            on t.DocId equals tn.Baigiang_Id
                                            join f in _context.file_Tailen_Dbs
                                            on tn.File_Tainguyen_Id equals f.File_Tailen_Id
                                            where c.Monhoc_Id == monhoc_id
                                            select f).ToListAsync();
                        if (status < 0)
                        {
                            throw new Exception("Trang thai khong phu hop");
                        }
                        else if (status == 0)
                        {
                            listfile = result;
                        }
                        else if (status > 0)
                        {
                            listfile = (from f in result where f.Status == status select f).ToList();
                        }
                    }
                    else
                    {
                        throw new Exception("Option khong phu hop");
                    }
                    if (listfile.Count > 0)
                    {
                        List<File_Tailen_Model> list_file_model = new List<File_Tailen_Model>();
                        list_file_model = _mapper.Map<List<File_Tailen_Model>>(listfile);
                        foreach (var file in list_file_model)
                        {
                            if (file.Type == "1")
                            {
                                file.Type = "Bai giang";
                            }
                            else if (file.Type == "2")
                            {
                                file.Type = "Tai nguyen";
                            }

                            if (file.Status == "-1")
                            {
                                file.Status = "Luu nhap";
                            }
                            else if (file.Status == "0")
                            {
                                file.Status = "Cho duyet";
                            }
                            else if (file.Status == "1")
                            {
                                file.Status = "Da duyet";
                            }
                            else if (file.Status == "2")
                            {
                                file.Status = "Da huy";
                            }
                        }
                        return list_file_model;
                    }
                    else
                    {
                        throw new Exception("Khong co file nao");
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
                                               where nd.UserID == model.ID_Nguoiduyet &&
                                               role.Phanquyen == 1
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
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<object> Top10_Filetailen_Gannhat(int user_id, int page)
        {
            try
            {
                if (user_id > 0 && page>=0)
                {
                    int skip = (page - 1) * 10;
                    var result =await (from file in _context.file_Tailen_Dbs
                                  where file.Nguoitailen_Id == user_id
                                  orderby file.Ngaytailen descending
                                  select file).Skip(skip).Take(10).ToListAsync();
                    if (result != null)
                    {

                        List<File_Tailen_Model> listtialieu = new List<File_Tailen_Model>();
                        listtialieu = _mapper.Map<List<File_Tailen_Model>>(result);
                        foreach (var item in listtialieu)
                        {
                            if (item.Type == "2")
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
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }
    }
}
