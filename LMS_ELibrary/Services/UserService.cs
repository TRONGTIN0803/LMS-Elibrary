using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace LMS_ELibrary.Services
{
    
    public class UserService:IUserService
    {
        public readonly LMS_ELibraryContext _context;
        public readonly IMapper _mapper;

        public UserService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> add_Account_Hocvien(Register_User_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if(model.UserName!=""&&model.Password!=""&&model.UserFullname!=""&&model.Email!=""&model.Sdt!=""&&model.Nganh!="")
                {
                    User_Db account = new User_Db();
                    account.UserName = model.UserName;
                    account.Password = model.Password;
                    account.UserFullname = model.UserFullname;
                    account.Gioitinh = model.Gioitinh != null ? model.Gioitinh : null ;
                    account.Role = 3;
                    account.Email = model.Email;
                    account.Sdt = model.Sdt;
                    account.Diachi = model.Diachi != "" ? model.Diachi : null ;
                    account.Nganh = model.Nganh;
                    account.Ngaysuadoi = DateTime.Now;

                    await _context.user_Dbs.AddAsync(account);
                    await _context.SaveChangesAsync();
                    if (account.UserID < 10)
                    {
                        account.MaUser = "HV0"+account.UserID;
                    }
                    else
                    {
                        account.MaUser = "HV" + account.UserID;
                    }
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
                kq.Message=e.Message;

                return kq;
            }
        }

        public async Task<KqJson> add_Account_Giangvien(Register_User_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.UserName != "" && model.Password != "" && model.UserFullname != "" && model.Email != "" & model.Sdt != "" && model.Nganh != "")
                {
                    User_Db account = new User_Db();
                    account.UserName = model.UserName;
                    account.Password = model.Password;
                    account.UserFullname = model.UserFullname;
                    account.Gioitinh = model.Gioitinh != null?model.Gioitinh:null ;
                    account.Role = 2;
                    account.Email = model.Email;
                    account.Sdt = model.Sdt;
                    account.Diachi = model.Diachi != "" ? model.Diachi : null;
                    account.Nganh = model.Nganh;
                    account.Ngaysuadoi = DateTime.Now;

                    await _context.user_Dbs.AddAsync(account);
                    if (account.UserID < 10)
                    {
                        account.MaUser = "GV0" + account.UserID;
                    }
                    else
                    {
                        account.MaUser = "GV" + account.UserID;
                    }
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
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;

                return kq;
            }
        }

        public async Task<object> Avt_da_tai_len(int user_id)
        {
            try
            {
                if (user_id > 0)
                {
                    List<Avt_Model> list_avt = new List<Avt_Model>();
                    var result = await (from avt in _context.avt_Db where avt.UserId == user_id select avt).ToListAsync();

                    if (result .Count>0)
                    {
                        list_avt = _mapper.Map<List<Avt_Model>>(result);
                        return list_avt;
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
                    
                }
                else
                {
                    throw new Exception("Bad Request!");
                }

            }catch(Exception e)
            {

                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> changePassword(int user_id, ChangePass pass)
        {
            KqJson kq = new KqJson();
            try
            {
                if(user_id > 0 && pass.newPass != "" && pass.reNewass != "")
                {
                    string newpass = pass.newPass;
                    string renewpass = pass.reNewass;
                    if (newpass != renewpass)
                    {
                        throw new Exception("Mat khau khong trung khop!");
                    }
                    else
                    {
                        if (newpass.Length < 8)
                        {
                            throw new Exception("Mat khau it nhat 8 ky tu");
                        }
                        else if (newpass.Any(Char.IsDigit) == false)
                        {
                            throw new Exception("Mat khau phai chua so");
                        }
                        else if (newpass.Any(Char.IsLetter) == false)
                        {
                            throw new Exception("Mat khau phai chua chu cai");
                        }
                        else if (newpass.Any(Char.IsLower) == false)
                        {
                            throw new Exception("Mat khau phai chua chu thuong");
                        }
                        else if (newpass.Any(Char.IsUpper) == false)
                        {
                            throw new Exception("Mat khau phai chua chu hoa");
                        }
                        else if (newpass.Any(p => !char.IsLetterOrDigit(p)) == false)
                        {
                            throw new Exception("Mat khau phai chua ky tu dac biet");
                        }
                        else
                        {
                            var result = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == user_id && p.Password == pass.Password);
                            if (result != null)
                            {
                                result.Password = newpass;
                                int row = await _context.SaveChangesAsync();
                                if (row > 0)
                                {
                                    kq.Status = true;
                                    kq.Message = "Change Password Successful";
                                    return kq;
                                }
                                else
                                {
                                    throw new Exception("Change Password Failed");
                                }

                            }
                            else
                            {
                                throw new Exception("Not Found");
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Bad Request");
                }
                
            }
            catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
                
            }
        }

        public async Task<object> checkInfor(int user_id)
        {
            try
            {
                if (user_id > 0)
                {
                    var result = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == user_id);

                    if (result != null)
                    {
                        User_Model user = new User_Model();
                        user = _mapper.Map<User_Model>(result);
                        user.Password = "***";
                        if (result.Gioitinh == true)
                        {
                            user.Gioitinh = "Nam";
                        }
                        else
                        {
                            user.Gioitinh = "Nu";
                        }

                        if (user.Avt == null)
                        {
                            user.Avt = "Khong co Anh dai dien";
                        }

                        
                        return user;
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

        public async Task<object> Login(User_Model _user)
        {
            KqJson kq = new KqJson();
            try
            {
                
                if (_user .UserName!=""&&_user.Password!=""&&_user.Role!="")
                {
                    int role =int.Parse(_user.Role);
                    string username = _user.UserName;
                    string pass = _user.Password;
                    var result = await _context.user_Dbs.SingleOrDefaultAsync(u => u.UserName == username && u.Password == pass);
                    if (result != null)
                    {
                        if (result.Role == role)
                        {
                            User_Model user = new User_Model();
                            user = _mapper.Map<User_Model>(result);
                            user.Password = "***";
                            
                            if (result.Gioitinh == true)
                            {
                                user.Gioitinh = "Nam";
                            }
                            else
                            {
                                user.Gioitinh = "Nu";
                            }

                            if (user.Avt == null)
                            {
                                user.Avt = "Khong co Anh dai dien";
                            }

                            if (result.Role == 1)
                            {
                                user.Phanquyen = "Quan Ly";
                                if (user.UserID < 10)
                                {
                                    user.MaUser="AD0"+user.UserID;
                                }
                                else
                                {
                                    user.MaUser = "AD" + user.UserID;
                                }
                            }
                            else if (result.Role == 1)
                            {
                                user.Phanquyen = "Giang Vien";
                            }
                            else
                            {
                                user.Phanquyen = "Hoc Vien";
                            }

                            return user;
                        }
                        else
                        {
                            throw new Exception("Not Found");
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Not Found");
                    }
                }
                else
                {
                    throw new Exception("Bad request");
                }
            }
            catch (Exception e)
            {
                kq.Status = false;
                kq.Message=e.Message;

                return kq;

            }


        }

        public async Task<object> UpLoadAvt(int user_id, IFormFile file)
        {
            KqJson kq = new KqJson();
            try
            {
                if (user_id > 0)
                {
                    //long size = file.Sum(f => f.Length);
                    string path = "";
                    double size = file.Length;
                    var result = await _context.user_Dbs.SingleOrDefaultAsync(p => p.UserID == user_id);
                    if (result != null)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\", fileName);

                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await file.CopyToAsync(stream);
                                path = filePath;
                            }

                            if (path != null)
                            {
                                Avt_Db avt = new Avt_Db();
                                avt.Path = path;
                                avt.Size = size;
                                avt.Ngay_tai_len = DateTime.Now;
                                avt.UserId = user_id;

                                await _context.avt_Db.AddAsync(avt);
                                int row_count = await _context.SaveChangesAsync();
                                if (row_count > 0)
                                {
                                    int idavt = avt.AvtID;
                                    result.Avt = path;
                                    result.AvtId = idavt;
                                    int row = await _context.SaveChangesAsync();
                                    if (row > 0)
                                    {
                                        kq.Status = true;
                                        kq.Message = "Upadte Successful";
                                        return kq;
                                    }
                                    else
                                    {
                                        throw new Exception("Update Failed");
                                    }

                                }
                                else
                                {
                                    throw new Exception("Loi luu avt!");
                                }
                            }
                            else
                            {
                                throw new Exception("Loi tai file");
                            }

                        }
                        else
                        {
                            throw new Exception("Khong co file de tai");
                        }

                    }
                    else
                    {
                        throw new Exception("Not Fuond");
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

        public async Task<KqJson> xoaAccount(User_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.UserID >0)
                {
                    var result = await _context.user_Dbs.SingleOrDefaultAsync(p=>p.UserID==model.UserID);
                    if (result != null)
                    {
                        var quyen = await (from nd in _context.user_Dbs
                                           join role in _context.role_Dbs
                                           on nd.Role equals role.RoleId
                                           where nd.UserID == model.UserID
                                           select role).FirstOrDefaultAsync();
                        if (quyen.Phanquyen == 1)
                        {
                            throw new Exception("Khong the xoa tai khoan ADMIN");
                        }
                        else
                        {
                            _context.user_Dbs.Remove(result);
                            int row = await _context.SaveChangesAsync();
                            if (row > 0)
                            {
                                kq.Status = true;
                                kq.Message = "Thanh cong";

                                return kq;
                            }
                            else
                            {
                                throw new Exception("Xoa that bai");
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
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message= e.Message;

                return kq;
            }
        }

        public async Task<KqJson> ThemRole(Role_Model model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.Tenvaitro != "" && model.Mota !=""&&model.Phanquyen>0)
                {
                    Role_Db role = new Role_Db();
                    role.Tenvaitro=model.Tenvaitro;
                    role.Mota=model.Mota;
                    role.Phanquyen = model.Phanquyen;

                    await _context.role_Dbs.AddAsync(role);
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
            }catch(Exception e)
            {
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
            }
        }

        public async Task<KqJson> SuaThongtinUser(SuathongtinUser_Request_DTO model)
        {
            KqJson kq = new KqJson();
            try
            {
                if (model.User_Id > 0)
                {
                    var result = await _context.user_Dbs.SingleOrDefaultAsync(p=>p.UserID==model.User_Id);
                    if(result!= null)
                    {
                        result.UserFullname = model.UserFullname != "" ? model.UserFullname : null;
                        result.Diachi = model.Diachi != "" ? model.Diachi : null;
                        result.Sdt = model.Sdt != "" ? model.Sdt : null;
                        result.Email = model.Email != "" ? model.Email : null;
                        int row = await _context.SaveChangesAsync();
                        if (row > 0)
                        {
                            kq.Status = true;
                            kq.Message = "Sua thanh cong";
                            return kq;
                        }
                        else
                        {
                            throw new Exception("Sua that bai");
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
