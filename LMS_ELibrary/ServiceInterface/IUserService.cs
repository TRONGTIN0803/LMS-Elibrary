using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IUserService
    {
        Task<object> Login(User_Model user);

        Task<object> checkInfor(int user_id);

        Task<object> UpLoadAvt(int user_id, IFormFile file);

        Task<KqJson> changePassword(int user_id, ChangePass pass);

        Task<object> Avt_da_tai_len(int user_id);

        Task<KqJson> add_Account_Hocvien(Register_User_Request_DTO model);
        Task<KqJson> add_Account_Giangvien(Register_User_Request_DTO model);
        Task<KqJson> xoaAccount(User_Model model);

        Task<KqJson> ThemRole(Role_Model model);
        Task<KqJson>SuaThongtinUser(SuathongtinUser_Request_DTO model);
    }
}
