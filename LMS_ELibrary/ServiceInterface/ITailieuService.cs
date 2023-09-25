using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ITailieuService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu(int id);
        Task<IEnumerable<Tailieu_Baigiang_Model>> searchBaigiang(int id, string key);

        Task<IEnumerable<Tailieu_Baigiang_Model>> filterBaigiang(int id, int monId);
        Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu);

        Task<KqJson> tai_len_Tai_Nguyen(int user_id,List<IFormFile> files);
        Task<KqJson> tai_len_Bai_Giang(int user_id, List<IFormFile> files);

        Task<KqJson> delTailieu(int id);

        Task<object> them_vao_Monhoc_va_Chude(Gui_pheduyet_tailieu_Request_DTO model);

        Task<object> XemTailieutheoTrangthai(int status);
        Task<KqJson> XetDuyetTaiLieu(Xetduyet_Request_DTO model);
    }
}
