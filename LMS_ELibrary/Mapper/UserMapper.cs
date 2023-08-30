using AutoMapper;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Mapper
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<User_Db, User_Model>();
            CreateMap<Tailieu_Baigiang_Db, Tailieu_Baigiang_Model>();
        }
    }
}
