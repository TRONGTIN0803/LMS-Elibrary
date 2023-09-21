using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IHoidapService
    {
        Task<object> XemhoidapBaigiang(int baigiangId);
        Task<object> XemcauhoiYeuthich(int user_id);
        Task<KqJson> DatcauhoiBaigang(CauhoiVandap_Model model);
        Task<KqJson> TrlCauhoi(Cautrl_Model model);
        Task<KqJson> ChinhsuaCautrl(Cautrl_Model model);
        Task<KqJson> ThemCauhoiYeuthich(CauhoiVandap_Model model);
        Task<KqJson> Xoacautrl(Cautrl_Model model);
    }
}
