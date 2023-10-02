using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IHoidapService
    {
        Task<object> XemhoidapBaigiang(int user_id, int baigiangId,int lop_Id,int typeCauhoi, int filt);
        Task<object> XemcauhoiYeuthich(int user_id);
        Task<KqJson> DatcauhoiBaigang(Datcauhoitronglop_Request_DTO model);
        Task<KqJson> TrlCauhoi(Cautrl_Model model);
        Task<KqJson> ChinhsuaCautrl(Cautrl_Model model);
        Task<KqJson> YeuthichCauhoi(Yeuthich_Request_DTO model);
        Task<KqJson> Xoacautrl(Cautrl_Model model);
    }
}
