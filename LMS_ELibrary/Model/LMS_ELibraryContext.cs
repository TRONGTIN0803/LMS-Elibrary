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
        public DbSet<Avt_Db>avt_Db { get; set; }
        public DbSet<File_Dethi_Db>file_Dethi_Db { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ex_QA_Db>(entity => {
                // Thiết lập cho bảng Dethi
                entity.HasOne(e => e.Dethi)                       // Chỉ ra Entity là phía một (bảng Dethi)
                        .WithMany(dethi=>dethi.ListExQA)         // Chỉ ra Collection tập Ex_QA lưu ở phía một
                        .HasForeignKey("DethiID")                 // Chỉ ra tên FK nếu muốn
                        .OnDelete(DeleteBehavior.Cascade)            // Ứng xử khi User bị xóa (Hoặc chọn DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Ex_Qa_Dethi"); // Tự đặt tên Constrain (dàng buốc)

            });
            modelBuilder.Entity<Avt_Db>(entity => {
                // Thiết lập cho bảng AVt
                entity.HasOne(e => e.User)                       
                        .WithMany(dethi => dethi.ListAvt)         
                        .HasForeignKey("UserId")                 
                        .OnDelete(DeleteBehavior.Cascade)            
                        .HasConstraintName("FK_Avt_User"); 

            });
            modelBuilder.Entity<Dethi_Db>(entity => {
                // Thiết lập cho bảng Dethi
                entity.HasOne(e => e.File_Dethi)                       
                        .WithMany(file => file.listDethi)         
                        .HasForeignKey("FileId")                
                        .OnDelete(DeleteBehavior.SetNull)            
                        .HasConstraintName("FK_Dethi_File"); 

            });

            modelBuilder.Entity<File_Dethi_Db>(entity => {
                // Thiết lập cho bảng File_Dethi
                entity.HasOne(e => e.User)                       
                        .WithMany(user => user.list_File_Dethi)         
                        .HasForeignKey("User_Id")                 
                        .OnDelete(DeleteBehavior.Cascade)            
                        .HasConstraintName("FK_File_User"); 

            });


        }
    }
}
