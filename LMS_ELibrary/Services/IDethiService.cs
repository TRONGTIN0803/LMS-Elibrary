using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Services
{
    public interface IDethiService
    {
        Task<IEnumerable<Dethi_Model>> getalldethi();

        Task<IEnumerable<Dethi_Model>> filterDethitheoMon(int id);

        Task<IEnumerable<Dethi_Model>> filterDethitheoTohomon(int id);

        Task<KqJson> tao_dethi_nganhangcauhoi(Dethi_Model dethi,List<int>idQues);

        Task<IEnumerable<Dethi_Model>> searchDethi(string madethi);

        Task<Dethi_Model> chitietDethi(int id);

        Task<KqJson> doiMadethi(int iddethi, Dethi_Model dethi);
        
        Task<KqJson> guiPheduyet(int iddethi);

        Task<KqJson> deleteDethi(int id);



    }
}
