using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMS_ELibrary.Services
{
    public class UserService : IUserService
    {
        public readonly LMS_ELibraryContext _context;
        public readonly IMapper _mapper;

        public UserService(LMS_ELibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}
