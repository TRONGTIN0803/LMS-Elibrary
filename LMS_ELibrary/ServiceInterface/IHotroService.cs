using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IHotroService
    {
        Task<object> getAlllisthotro();

        Task<KqJson> PostHelp(Help_Model help);

        Task<KqJson> addHelp(Help_Model help);
    }
}
