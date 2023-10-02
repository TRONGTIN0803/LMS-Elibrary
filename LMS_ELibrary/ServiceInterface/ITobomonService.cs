using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ITobomonService
    {
        Task<object> Getall();
        Task<KqJson> AddTobomon(Tobomon_Request_DTO model);
        Task<KqJson> editTobomon(Tobomon_Request_DTO model);
        Task<KqJson> deleteTobomon(Tobomon_Request_DTO model);
        Task<object> Xem_list_Tobomon_Giangvien(int giangvien_Id);
    }
}
