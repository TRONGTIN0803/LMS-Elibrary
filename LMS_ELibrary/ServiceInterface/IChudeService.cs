using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IChudeService
    {
        Task<IEnumerable<Chude_Model>> getAllchude();
        Task<Chude_Model> editChude(int id, Chude_Model chude);
        Task<KqJson> addChude(Chude_Model chude);
        Task<KqJson> deletetChude(int id);
    }
}
