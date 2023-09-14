using LMS_ELibrary.Model;

namespace LMS_ELibrary.ServiceInterface
{
    public interface ILopgiangService
    {
        Task<IEnumerable<Lopgiangday_Model>> getAllLopgiang();

        Task<Lopgiangday_Model> detailLopgiangday(int id);

        Task<KqJson> addLopgiang(Lopgiangday_Model lopgiangday_Model);

        Task<KqJson> editLopgiang(int lopgiang_id,Lopgiangday_Model lopgiang);
        
        Task<KqJson> deleteLopgiang(int lopgiang_id);
    }
}
