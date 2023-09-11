using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IHotroService
    {
        Task<IEnumerable<Help_Model>> getAlllisthotro();

        Task<KqJson> PostHelp(Help_Model help);
    }
}
