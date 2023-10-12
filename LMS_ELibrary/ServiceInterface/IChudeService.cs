using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IChudeService
    {
        Task<object> getAllchude(int monhoc_id);
        Task<KqJson> editChude(Edit_Baigiang_Tainguyen_Request_DTO model);
        Task<KqJson> addChude(Chude_Model chude);
        Task<KqJson> deletetChude(Delete_Entity_Request_DTO model);
        Task<object> Xem_Chude_Monhoc(int monhoc_id);
    }
}
