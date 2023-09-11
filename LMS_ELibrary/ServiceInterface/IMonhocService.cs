using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IMonhocService
    {
        Task<IEnumerable<Monhoc_Model>> getAllMonhoc();
        Task<Monhoc_Model> searchMonhoc(string key);

        Task<Monhoc_Model> chitietMonhoc(int id);

        Task<IEnumerable<Monhoc_Model>> locMonhoc(int option);
    }
}
