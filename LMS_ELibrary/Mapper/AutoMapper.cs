using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Mapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<User_Db, User_Model>();
            CreateMap<Tailieu_Baigiang_Db, Tailieu_Baigiang_Model>();
            CreateMap<Lopgiangday_Db, Lopgiangday_Model>();
            CreateMap<Monhoc_Db, Monhoc_Model>();
            CreateMap<Chude_Db, Chude_Model>();
            CreateMap<Lopgiangday_Db, Lopgiangday_Model>();

        }
    }
}
