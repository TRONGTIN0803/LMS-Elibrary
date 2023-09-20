using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IUserService
    {
        Task<object> Login(User_Model user);

        Task<object> checkInfor(int user_id);

        Task<object> UpLoadAvt(int user_id, IFormFile file);

        Task<KqJson> changePassword(int user_id, ChangePass pass);

        Task<IEnumerable<Avt_Model>> Avt_da_tai_len(int user_id);
    }
}
