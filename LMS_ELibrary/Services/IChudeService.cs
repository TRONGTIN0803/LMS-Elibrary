using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Services
{
    public interface IChudeService
    {
        Task<Chude_Model> editChude(int id,Chude_Model chude);
    }
}
