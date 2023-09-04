using LMS_ELibrary.Model;

namespace LMS_ELibrary.Services
{
    public interface ILopgiangService
    {
        Task<IEnumerable<Lopgiangday_Model>> getAllLopgiang();

        Task<Lopgiangday_Model> detailLopgiangday(int id);
    }
}
