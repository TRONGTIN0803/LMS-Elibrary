using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Services
{
    public interface IDethiService
    {
        Task<IEnumerable<Dethi_Model>> getalldethi();

        Task<IEnumerable<Dethi_Model>> filterDethitheoMon(int id);

        Task<IEnumerable<Dethi_Model>> filterDethitheoTohomon(int id);
    }
}
