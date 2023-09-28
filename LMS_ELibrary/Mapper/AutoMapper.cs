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
            CreateMap<Dethi_Db, Dethi_Model>();
            CreateMap<Ex_QA_Db, Ex_QA_Model>();
            CreateMap<QA_Db, QA_Model>();
            CreateMap<Thongbao_Db, Thongbao_Model>();
            CreateMap<Help_Db, Help_Model>();
            CreateMap<Avt_Db, Avt_Model>();
            CreateMap<Tobomon_Db, Tobomon_Model>();
            CreateMap<CauhoiVandap_Db, CauhoiVandap_Model>();
            CreateMap<Cautrl_Db, Cautrl_Model>();
            CreateMap<CauhoiYeuthich_Db, CauhoiYeuthich_Model>();
            CreateMap<Tongquan_Db, Tongquan_Model>();
            CreateMap<Role_Db, Role_Model>();
            CreateMap<ThongbaoLop_Db, ThongbaoLop_Model>();
        }
    }
}
