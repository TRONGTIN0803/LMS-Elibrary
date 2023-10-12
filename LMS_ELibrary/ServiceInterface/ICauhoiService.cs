using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ICauhoiService
    {
        Task<object> getAllCauhoi();

        Task<object> xemCauhoitheoMon(int idmon);

        Task<object> xemCauHoitheoToMon(int idtomon);

        Task<object> chitietCauhoi(int idcauhoi);

        Task<KqJson> editCauhoi(Edit_Cauhoi_Request_DTO model);

        Task<KqJson> xoaCauhoi(Delete_Entity_Request_DTO model);

        Task<KqJson> addCauhoi(Taocauhoi_Request_DTO model);
    }
}
