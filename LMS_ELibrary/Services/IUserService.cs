using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Services
{
    public interface IUserService
    {
        Task<User_Model> Login(User_Model user);

        Task<object> checkInfor(int user_id);

        Task<object> UpLoadAvt(int user_id, IFormFile file);

        Task<KqJson> changePassword(int user_id, ChangePass pass);
    }
}
