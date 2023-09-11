using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IThongbaoService
    {
        Task<IEnumerable<Thongbao_Model>> getallThongbao(int id);

        Task<IEnumerable<Thongbao_Model>> searchThongbao(int user_id, string keyword);

        Task<object> chitietThongbao(int idthongbao);

        Task<KqJson> xoaThongbao(List<int> listid);

        // 0 => chua doc ; 1 => da doc
        Task<object> locThongBao(int user_id, int status);

        Task<KqJson> danhDauThongBao(int thongbao_id, int status);
    }
}
