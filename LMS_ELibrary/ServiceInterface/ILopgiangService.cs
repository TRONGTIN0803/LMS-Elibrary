using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ILopgiangService
    {
        Task<object> getAllLopgiang();

        Task<object> detailLopgiangday(int id);

        Task<KqJson> addLopgiang(Lopgiangday_Model mode);

        Task<KqJson> editLopgiang(int lopgiang_id,Lopgiangday_Model model);
        
        Task<KqJson> deleteLopgiang(Delete_Entity_Request_DTO model);

        Task<KqJson> xepLopChoHocVien(Hocvien_Lop_Model model);

        Task<object> lopDangTheoHoc(int user_id);
        

    }
}
