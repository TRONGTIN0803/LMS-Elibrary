using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ITailieuService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu(int id);
        Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu);

        Task<KqJson> addTailieu(Tailieu_Baigiang_Model tailieu);

        Task<KqJson> delTailieu(int id);
    }
}
