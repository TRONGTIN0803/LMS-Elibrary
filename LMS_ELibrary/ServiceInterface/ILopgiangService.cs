using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ILopgiangService
    {
        Task<IEnumerable<Lopgiangday_Model>> getAllLopgiang();

        Task<Lopgiangday_Model> detailLopgiangday(int id);
    }
}
