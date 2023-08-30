using LMS_ELibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS_ELibrary.Model
{
    public class LMS_ELibraryContext : DbContext
    {
        public LMS_ELibraryContext(DbContextOptions<LMS_ELibraryContext> options):base(options)
        {

        }

        public DbSet<Chude_Db>chude_Dbs { get; set; }
        public DbSet<Dethi_Db>dethi_Dbs { get; set; }
        public DbSet<Ex_QA_Db>ex_QA_Dbs { get; set; }
        public DbSet<Help_Db>help_Dbs { get; set; }
        public DbSet<Lopgiangday_Db>lopgiangday_Dbs { get; set; }
        public DbSet<Monhoc_Db>monhoc_Dbs { get; set; }
        public DbSet<QA_Db>qA_Dbs { get; set; }
        public DbSet<Tailieu_Baigiang_Db>tailieu_Baigiang_Dbs { get; set; }
        public DbSet<Thongbao_Db>thongbao_Dbs { get; set; }
        public DbSet<Tobomon_Db>tobomon_Dbs { get; set; }
        public DbSet<User_Db>user_Dbs { get; set; }
    }
}
