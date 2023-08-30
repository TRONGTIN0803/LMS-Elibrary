using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Mapper
{
    public class MonhocMapper:Profile
    {
        public MonhocMapper()
        {
            CreateMap<Monhoc_Db, Monhoc_Model>();
        }
    }
}
