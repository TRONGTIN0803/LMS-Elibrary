using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
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

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu(int id)
        {
            try
            {
                var result = await (from tailieu in _context.tailieu_Baigiang_Dbs where tailieu.Type == 0 && tailieu.UserId==id select tailieu).ToListAsync();
                foreach (var item in result)
                {
                    var col = _context.Entry(item);
                    col.Reference(p => p.User).Load();
                    User_Db user = new User_Db();
                    user.UserFullname = item.User.UserFullname;
                    user.UserName=item.User.UserName;
                    user.Password = "***";
                    user.Email = item.User.Email;
                    user.Role = item.User.Role;
                    user.Avt = item.User.Avt;
                    user.Gioitinh = item.User.Gioitinh;
                    user.Sdt = item.User.Sdt;
                    user.Diachi = item.User.Diachi;

                    item.User= user;

                }
                List<Tailieu_Baigiang_Model> listtialieu = new List<Tailieu_Baigiang_Model>();
                listtialieu = _mapper.Map<List<Tailieu_Baigiang_Model>>(result);
                foreach (var item in listtialieu)
                {
                    item.Type = "Tai lieu";
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
                    if(item.User.Gioitinh == "True")
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
            catch(Exception e)
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

        public async Task<IEnumerable<Tailieu_Baigiang_Model>> filterBaigiang(int id, int monId)
        {
            try
            {
                var result = await (from baigiang in _context.tailieu_Baigiang_Dbs
                                    where baigiang.Type == 1 && baigiang.UserId == id && baigiang.MonhocID == monId
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

        public async Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu)
        {
            try
            {
                var _tailieu = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p=>p.DocId==id && p.Type==0);
                KqJson kq = new KqJson();
                if (_tailieu != null)
                {
                    _tailieu.TenDoc = tailieu.TenDoc!=null ? _tailieu.TenDoc = tailieu.TenDoc : _tailieu.TenDoc;
                    _tailieu.UserId = tailieu.UserID != null ? _tailieu.UserId = tailieu.UserID : _tailieu.UserId;
                    _tailieu.MonhocID = tailieu.MonhocID != null ? _tailieu.MonhocID = tailieu.MonhocID : _tailieu.MonhocID;
                    _tailieu.ChudeID = tailieu.ChudeID != null ? _tailieu.ChudeID = tailieu.ChudeID : _tailieu.ChudeID;
                    //_tailieu.Kichthuoc = tailieu.Kichthuoc != null ? _tailieu.Kichthuoc = tailieu.Kichthuoc : _tailieu.Kichthuoc;
                    //_tailieu.Path = tailieu.Path != null ? _tailieu.Path = tailieu.Path : _tailieu.Path;
                    //_tailieu.Status = tailieu.Status != null ? _tailieu.Status = int.Parse(tailieu.Status) : _tailieu.Status;
                    //_tailieu.Type = tailieu.Type != null ? _tailieu.Type = int.Parse(tailieu.Type) : _tailieu.Type;
                    _tailieu.Sualancuoi = DateTime.Now;

                    int row_edit =await _context.SaveChangesAsync();
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
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> tai_len_Tai_Lieu(int user_id,List<IFormFile> files)
        {
            try
            {
                if(user_id != null && files != null)
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
                            _tailieu.Status = 0; // status =0 -> dang duyet ; 1 -> da duyet
                            _tailieu.Type = 0;  // type = 0 -> tailieu ; 1-> baigiang
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
            }catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message=e.Message;

                return kq;
            }
        }

        public async Task<KqJson>delTailieu(int id)
        {
            try
            {
                KqJson kq = new KqJson();

                var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p=>p.DocId==id && p.Type==0);
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

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<KqJson> them_vao_Monhoc_va_Chude(int monhoc_id, int chude_id, List<int> tailieu_id)
        {
            try
            {
                if(monhoc_id!=null && chude_id!=null && tailieu_id != null)
                {
                    foreach(int docid in tailieu_id)
                    {
                        var result = await _context.tailieu_Baigiang_Dbs.SingleOrDefaultAsync(p=>p.DocId==docid);
                        if (result != null)
                        {
                            result.MonhocID = monhoc_id;
                            result.ChudeID = chude_id;
                            result.Sualancuoi = DateTime.Now;
                        }
                        else{
                            throw new Exception("Not Found");
                        }
                    }
                    int row = await _context.SaveChangesAsync();
                    if (row == tailieu_id.Count)
                    {
                        KqJson kq = new KqJson();
                        kq.Status = true;
                        kq.Message ="Thanh cong!";

                        return kq;
                    }
                    else
                    {
                        throw new Exception("Co phan tu khong phu hop");
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
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
