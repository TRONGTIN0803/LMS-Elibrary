using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IHoidapService
    {
        Task<object> XemhoidapBaigiang(int baigiangId);

        Task<KqJson> DatcauhoiBaigang(CauhoiVandap_Model model);

        Task<KqJson> TrlCauhoi(Cautrl_Model model);  
    }
}
