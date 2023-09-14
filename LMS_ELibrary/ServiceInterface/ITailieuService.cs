using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ITailieuService
    {
        Task<IEnumerable<Tailieu_Baigiang_Model>> getAlltailieu(int id);
        Task<KqJson> editTailieu(int id, Tailieu_Baigiang_Model tailieu);

        Task<KqJson> tai_len_Tai_Lieu(int user_id,List<IFormFile> files);

        Task<KqJson> delTailieu(int id);

        Task<KqJson> them_vao_Monhoc_va_Chude(int monhoc_id, int chude_id, List<int> tailieu_id);
    }
}
