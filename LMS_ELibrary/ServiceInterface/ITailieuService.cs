using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ITailieuService
    {
        Task<object> GetAllBaigiang();
        Task<object> Tailieucuatoi(int id);
        Task<object> searchBaigiang(int id, string key);

        Task<KqJson> TaomoiBaigiang(Taomoi_Baigiang_Request_DTO model);
        Task<KqJson> Taotainguyen_cho_Baigiang(Taotainguyen_Baigiang_Request_DTO model);
        Task<KqJson> editTailieu(Edit_Baigiang_Tainguyen_Request_DTO model);

        Task<KqJson> tai_len_Tai_Nguyen(int user_id,List<IFormFile> files);
        Task<KqJson> tai_len_Bai_Giang(int user_id, List<IFormFile> files);

        Task<KqJson> delTailieu(Delete_Entity_Request_DTO model);

        Task<KqJson> Gui_Yeu_Cau_Huy_Yeu_Cau_Phe_Duyet(Gui_pheduyet_tailieu_Request_DTO model);
        Task<KqJson> Gui_Yeu_Cau_Huy_Yeu_Cau_Phe_Duyet_File(Gui_pheduyet_tailieu_Request_DTO model);
        Task<object> Xem_File_theo_Mon(int monhoc_id,int option,int status);
        Task<object> XemBaigiangtheoTrangthai(int status);
        Task<KqJson> XetDuyetBaigiang(Xetduyet_Request_DTO model);
        Task<KqJson> XetduyetFile(Xetduyet_Request_DTO model);

        Task<object> Top10_Filetailen_Gannhat(int user_id,int page);
    }
}
