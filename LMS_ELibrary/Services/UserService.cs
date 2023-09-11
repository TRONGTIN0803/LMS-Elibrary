using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace LMS_ELibrary.Services
{
    public interface IUserService
    {
        Task<User_Model> Login(User_Model user);

        Task<object> checkInfor(int user_id);

        Task<object> UpLoadAvt(int user_id, IFormFile file);

        Task<KqJson> changePassword(int user_id, ChangePass pass);
    }
    public class UserService : IUserService
    {
        public readonly LMS_ELibraryContext _context;
        public readonly IMapper _mapper;

        public UserService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KqJson> changePassword(int user_id, ChangePass pass)
        {
            try
            {
                KqJson kq = new KqJson();
                string newpass = pass.newPass;
                string renewpass=pass.reNewass;
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
                            if (row>0)
                            {
                                kq.Status = true;
                                kq.Message = "Change Password Successful";
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
                return kq;
            }
            catch(Exception e)
            {
                KqJson kq = new KqJson();
                kq.Status = false;
                kq.Message = e.Message;
                return kq;
                
            }
        }

        public async Task<object> checkInfor(int user_id)
        {
            try
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

                    if (result.Role == 0)
                    {
                        user.Role = "Quan ly";
                    }
                    else if (result.Role == 1)
                    {
                        user.Role = "Giao vien";
                    }
                    else
                    {
                        user.Role = "Hoc sinh";
                    }
                    return user;
                }
                else
                {
                    KqJson kq = new KqJson();
                    kq.Status = false;
                    kq.Message = "Not Found";
                    return kq;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User_Model> Login(User_Model _user)
        {
            try
            {
                string username = _user.UserName;
                string pass = _user.Password;
                var result = await _context.user_Dbs.SingleOrDefaultAsync(u => u.UserName == username && u.Password == pass);
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

                    if (result.Role == 0)
                    {
                        user.Role = "Quan ly";
                    }
                    else if (result.Role == 1)
                    {
                        user.Role = "Giao vien";
                    }
                    else
                    {
                        user.Role = "Hoc sinh";
                    }

                    return user;
                }
                else
                {
                    throw new Exception("Not Found");
                }


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }


        }

        public async Task<object> UpLoadAvt(int user_id, IFormFile file)
        {

            try
            {
                KqJson kq = new KqJson();
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
                                result.AvtId= idavt;
                                int row = await _context.SaveChangesAsync();
                                if (row > 0)
                                {
                                    kq.Status = true;
                                    kq.Message = "Upadte Successful";
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
                return kq;
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
