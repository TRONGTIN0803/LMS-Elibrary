using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.ServiceInterface
{
    public interface IMonhocService
    {
        Task<object> getAllMonhoc(int user_id,int page); 
        Task<object> searchMonhoc(string key);

        Task<object> chitietMonhoc(int id,int user_id);

        Task<object> locMonhoc(int option,int user_id);
        Task<object> locMonhoc_theo_Tinhtrang(int status,int giangvien_id); //chi co giangvien va admin duoc xem

        Task<KqJson> editMonhoc(Edit_Monhoc_Request_DTO model);
        Task<KqJson> setTrangthai(Guiyeucau_Huyyeucau_Monhoc_Request_DTO model);

        Task<KqJson> addMonhoc(Monhoc_Model monhoc);
        Task<KqJson> deleteMonhoc(Delete_Entity_Request_DTO model);

        Task<KqJson> YeuthichMonhoc(Yeuthich_Request_DTO model);
        Task<object> Xem_List_monhoc_Yeuthich(int hocvien_id, int option);

        Task<object> xemMonhocDanghoc(int hocvien_id);  //hoc vien xem list mon hoc cua ban than

        Task<KqJson> themTongquanMonhoc(Them_Tongquan_Monhoc_Request_DTO model);

        Task<KqJson> xetduyetMonhoc(Xetduyet_Request_DTO model);

        Task<object> Mondangday(int giangvien_Id);  //giang vien xem monhoc dang day
        

    }
}
