using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IDethiService
    {
        Task<IEnumerable<Dethi_Model>> getalldethi();

        Task<IEnumerable<Dethi_Model>> filterDethitheoMon(int id);

        Task<IEnumerable<Dethi_Model>> filterDethitheoTohomon(int id);

        Task<KqJson> tao_dethi_nganhangcauhoi(Dethi_Model dethi, List<int> idQues);

        Task<KqJson> tao_dethi_tructiep(Tao_cauhoi_tructiep_Request_DTO model);

        Task<IEnumerable<Dethi_Model>> searchDethi(string madethi);

        Task<Dethi_Model> chitietDethi(int id);

        Task<KqJson> doiMadethi(int iddethi, Dethi_Model dethi);

        Task<KqJson> guiPheduyet(int iddethi);

        Task<KqJson> deleteDethi(int id);

        Task<KqJson> tai_len_Dethi(int user_id,List<IFormFile>files);

        Task<KqJson> them_File_vao_Dethi(int dethi_id,File_Dethi_Db file);
    }
}
