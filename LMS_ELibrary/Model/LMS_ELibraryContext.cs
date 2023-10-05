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
        public DbSet<MonhocYeuthich_Db>monhocYeuthich_Dbs { get; set; }
        public DbSet<CauhoiVandap_Db>cauhoiVandap_Dbs { get; set; }
        public DbSet<Cautrl_Db>cautrl_Dbs { get; set; }
        public DbSet<CauhoiYeuthich_Db>cauhoiYeuthich_Dbs { get; set; }
        public DbSet<Hocvien_Lop_Db>hocvien_Lop_Dbs { get; set; }
        public DbSet<ThongbaoLop_Db>thongbaoLop_Dbs { get; set; }
        public DbSet<Tongquan_Db>tongquan_Dbs { get; set; }
        public DbSet<Role_Db> role_Dbs { get; set; }
        public DbSet<File_Tailen_Db> file_Tailen_Dbs { get; set; }
        public DbSet<Tainguyen_Db> tainguyen_Dbs { get; set; }


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
            modelBuilder.Entity<MonhocYeuthich_Db>(entity => {
                // Thiết lập cho bảng File_Dethi
                entity.HasOne(e => e.User)
                        .WithMany(user => user.list_Monhocyeuthich)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_MonYeuthich_User");

            });
            //modelBuilder.Entity<MonhocYeuthich_Db>(entity => {
            //    // Thiết lập cho bảng MonhocYeuthich
            //    entity.HasOne(e => e.Monhoc)
            //            .WithMany(user => user.List_Monhocyeuthich)
            //            .HasForeignKey("MonhocId")
            //            .OnDelete(DeleteBehavior.SetNull)
            //            .HasConstraintName("FK_MonYeuthich_monhoc");

            //});
            modelBuilder.Entity<CauhoiVandap_Db>(entity => {
                // Thiết lập cho bảng CauhoiVandap
                entity.HasOne(e => e.User)
                        .WithMany(user => user.list_CauhoiVandap)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Cauhoicandap_user");

            });
            //modelBuilder.Entity<CauhoiVandap_Db>(entity => {
            //    // Thiết lập cho bảng CauhoiVandap
            //    entity.HasOne(e => e.Tailieu)
            //            .WithMany(user => user.list_Cauhoivandap)
            //            .HasForeignKey("TailieuId")
            //            .OnDelete(DeleteBehavior.SetNull)
            //            .HasConstraintName("FK_Cauhoicandap_tailieu");

            //});
            modelBuilder.Entity<Cautrl_Db>(entity =>
            {
                // Thiết lập cho bảng CauhoiVandap
                entity.HasOne(e => e.User)
                        .WithMany(user => user.list_Cautrl)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Cautrl_user");

            });
            //modelBuilder.Entity<Cautrl_Db>(entity => {
            //    // Thiết lập cho bảng CauhoiVandap
            //    entity.HasOne(e => e.Cauhoi)
            //            .WithMany(user => user.list_Cautrl)
            //            .HasForeignKey("CauhoiId")
            //            .OnDelete(DeleteBehavior.Cascade)
            //            .HasConstraintName("FK_Cautrl_cauhoi");

            //});
            modelBuilder.Entity<CauhoiYeuthich_Db>(entity =>
            {
                // Thiết lập cho bảng CauhoiVandap
                entity.HasOne(e => e.User)
                        .WithMany(user => user.list_Cauhoiyeuthich)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_cauhoiyeuthcih_user");

            });
            modelBuilder.Entity<User_Db>(entity =>
            {
                // Thiết lập cho bảng CauhoiVandap
                entity.HasOne(e => e.RoleDb)
                        .WithMany(user => user.listUser)
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_user_role");

            });

            modelBuilder.Entity<Monhoc_Db>(entity =>
            {
                // Thiết lập cho bảng MOnhoc
                entity.HasOne(e => e.GiangVien)
                        .WithMany(GiangVien => GiangVien.list_Mongiangday)
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Monhoc_user");

            });

            modelBuilder.Entity<Chude_Db>(entity =>
            {
                // Thiết lập cho bảng Chude
                entity.HasOne(e => e.Monhoc)
                        .WithMany(Monhoc => Monhoc.list_Chude)
                        .HasForeignKey("Monhoc_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_chude_Monhoc");

            });

            modelBuilder.Entity<Lopgiangday_Db>(entity =>
            {
                // Thiết lập cho bảng Lop
                entity.HasOne(e => e.Monhoc)
                        .WithMany(Monhoc => Monhoc.ListLopgiangday)
                        .HasForeignKey("MonhocID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_lop_Monhoc");

            });

            modelBuilder.Entity<ThongbaoLop_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Thongbao)
                        .WithMany(Thongbao => Thongbao.List_ThongbaoLop)
                        .HasForeignKey("Thongbao_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_thongbaolop_thongbao");

            });

            modelBuilder.Entity<ThongbaoLop_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.User)
                        .WithMany(User => User.list_Thongbao)
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_thongbaolop_user");

            });
            modelBuilder.Entity<ThongbaoLop_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Lopgiang)
                        .WithMany(Lopgiang => Lopgiang.list_ThongbaoLop)
                        .HasForeignKey("Lopgiang_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_thongbaolop_lop");

            });

            modelBuilder.Entity<Hocvien_Lop_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.User)
                        .WithMany(User => User.list_HocvienLop)
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_hocvienlop_user");

            });
            modelBuilder.Entity<Tongquan_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Monhoc)
                        .WithMany(Monhoc => Monhoc.list_Tongquan)
                        .HasForeignKey("Monhoc_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_tongquan_monhoc");

            });
            modelBuilder.Entity<Tainguyen_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Baigiang)
                        .WithMany(Baigiang => Baigiang.list_Tainguyen)
                        .HasForeignKey("Baigiang_Id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_tainguyen_baigiang");

            });
            modelBuilder.Entity<Tainguyen_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Nguoitailen)
                        .WithMany(user=>user.list_Tainguyen)
                        .HasForeignKey("Nguoitao_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_tainguyen_user");

            });
            modelBuilder.Entity<Tainguyen_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.File_Tainguyen)
                        .WithMany(file => file.list_Tainguyen)
                        .HasForeignKey("File_Tainguyen_Id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_tainguyen_filetainguyen");

            });
            modelBuilder.Entity<Tailieu_Baigiang_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.File_Baigiang)
                        .WithMany(file=>file.list_Baigiang)
                        .HasForeignKey("File_Baigiang_Id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_baigiang_file");

            });
            modelBuilder.Entity<File_Tailen_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Nguoitailen)
                        .WithMany(user=>user.list_File_Tailen)
                        .HasForeignKey("Nguoitailen_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_filebaigiang_user");

            });
            modelBuilder.Entity<QA_Db>(entity =>
            {
                // Thiết lập cho bảng thongbao
                entity.HasOne(e => e.Nguoitao)
                        .WithMany(user => user.list_Cauhoi_Datao)
                        .HasForeignKey("Nguoitao_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_cauhoi_user");

            });

        }
    }
}
