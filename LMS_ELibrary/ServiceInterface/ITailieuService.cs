using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ITailieuService
    {
        Task<object> GetAlltailieu();
        Task<IEnumerable<Tailieu_Baigiang_Model>> Tailieucuatoi(int id);
        Task<IEnumerable<Tailieu_Baigiang_Model>> searchBaigiang(int id, string key);

        Task<object> list_Tailieu_Monhoc(int monId,string? status,string? type);
        Task<object> list_Tailieu_Monhoc(int monId, string? type);
        Task<object> list_Tailieu_Monhoc_status(int monId, string? status);
        Task<object> list_Tailieu_Monhoc(int monId);

        Task<KqJson> TaomoiBaigiang(Taomoi_Baigiang_Request_DTO model);
        Task<KqJson> Taotainguyen_cho_Baigiang(Taotainguyen_Baigiang_Request_DTO model);
        Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu);

        Task<KqJson> tai_len_Tai_Nguyen(int user_id,List<IFormFile> files);
        Task<KqJson> tai_len_Bai_Giang(int user_id, List<IFormFile> files);

        Task<KqJson> delTailieu(Delete_Entity_Request_DTO model);

        Task<object> them_vao_Monhoc_va_Chude(Gui_pheduyet_tailieu_Request_DTO model);
        Task<object> Xem_File_theo_Mon(int monhoc_id);
        Task<object> XemTailieutheoTrangthai(int status);
        Task<KqJson> XetDuyetTaiLieu(Xetduyet_Request_DTO model);
        Task<KqJson> XetduyetFile(Xetduyet_Request_DTO model);
    }
}
