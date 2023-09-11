using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IBaigiangService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getallbaigigang(int id);

        Task<IEnumerable<Tailieu_Baigiang_Model>> searchBaigiang(int id, string key);

        Task<IEnumerable<Tailieu_Baigiang_Model>> filterBaigiang(int id, int monId);

        Task<KqJson> addBaigiang(Tailieu_Baigiang_Db baigiang);

        Task<KqJson> changeMonhoc(int iddoc, int mon);

        Task<KqJson> XoaBaigiang(int user_id,int baigiang_id);
    }
}
