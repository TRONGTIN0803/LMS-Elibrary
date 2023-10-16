using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IThongbaoService
    {
        Task<object> getallThongbao(int id);

        Task<object> searchThongbao(int user_id, string keyword);

        Task<object> chitietThongbao(int idthongbao,int user_id);

        Task<KqJson> xoaThongbao(Delete_Entity_Request_DTO model);

        // 0 => chua doc ; 1 => da doc
        Task<object> locThongBao(int user_id, int status);

        Task<KqJson> danhDauThongBao(Danhdauthongbao_Request_DTO model);

        Task<KqJson> Taothongbao(Gui_Thongbao_Request_DTO model);
    }
}
