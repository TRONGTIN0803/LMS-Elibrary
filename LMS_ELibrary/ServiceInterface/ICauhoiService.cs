using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ICauhoiService
    {
        Task<IEnumerable<QA_Model>> getAllCauhoi();

        Task<IEnumerable<QA_Model>> xemCauhoitheoMon(int idmon);

        Task<IEnumerable<QA_Model>> xemCauHoitheoToMon(int idtomon);

        Task<QA_Model> chitietCauhoi(int idcauhoi);

        Task<KqJson> editCauhoi(int idcauhoi, QA_Model cauhoi);

        Task<KqJson> xoaCauhoi(int idcauhoi);

        Task<KqJson> addCauhoi(Taocauhoi_Request_DTO model);
    }
}
