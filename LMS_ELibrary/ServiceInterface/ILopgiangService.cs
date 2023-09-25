using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ILopgiangService
    {
        Task<IEnumerable<Lopgiangday_Model>> getAllLopgiang();

        Task<Lopgiangday_Model> detailLopgiangday(int id);

        Task<KqJson> addLopgiang(Lopgiangday_Model lopgiangday_Model);

        Task<KqJson> editLopgiang(int lopgiang_id,Lopgiangday_Model lopgiang);
        
        Task<KqJson> deleteLopgiang(int lopgiang_id);

        Task<KqJson> xepLopChoHocVien(Hocvien_Lop_Model model);

        Task<object> lopDangTheoHoc(int user_id);
        Task<KqJson> themHocvienVaolop(them_Hocvien_vao_Lop_Request_DTO model);

    }
}
