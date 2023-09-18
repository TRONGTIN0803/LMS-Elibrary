using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IMonhocService
    {
        Task<IEnumerable<Monhoc_Model>> getAllMonhoc(int user_id);
        Task<Monhoc_Model> searchMonhoc(string key);

        Task<Monhoc_Model> chitietMonhoc(int id,int user_id);

        Task<IEnumerable<Monhoc_Model>> locMonhoc(int option);
        Task<object> locMonhoc_theo_Tinhtrang(int status);

        Task<KqJson> editMonhoc(int monhoc_id,Monhoc_Model monhoc);
        Task<KqJson> setTrangthai(List<int> monhoc_id,int status);

        Task<KqJson> addMonhoc(Monhoc_Model monhoc);
        Task<KqJson> deleteMonhoc(int monhoc_id);

        Task<KqJson> ThemYeuthichMonhoc(MonhocYeuthich_Model model);
        Task<KqJson> HuyYeuthichMonhoc(MonhocYeuthich_Model model);

    }
}
