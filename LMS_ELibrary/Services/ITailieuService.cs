using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Services
{
    public interface ITailieuService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu();
        Task<KqJson> editTailieu(int id,Tailieu_Baigiang_Model tailieu);

        Task<KqJson> addTailieu(Tailieu_Baigiang_Model tailieu);
    }
}
