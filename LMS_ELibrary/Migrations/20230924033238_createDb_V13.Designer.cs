﻿// <auto-generated />
using System;
using LMS_ELibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS_ELibrary.Migrations
{
    [DbContext(typeof(LMS_ELibraryContext))]
    [Migration("20230924033238_createDb_V13")]
    partial class createDb_V13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LMS_ELibrary.Data.Avt_Db", b =>
                {
                    b.Property<int>("AvtID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AvtID"), 1L, 1);

                    b.Property<DateTime>("Ngay_tai_len")
                        .HasColumnType("datetime2");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AvtID");

                    b.HasIndex("UserId");

                    b.ToTable("Avt");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.CauhoiVandap_Db", b =>
                {
                    b.Property<int>("CauhoiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CauhoiId"), 1L, 1);

                    b.Property<int?>("ChudeId")
                        .HasColumnType("int");

                    b.Property<int?>("LopgiangId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Ngaytao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Noidung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TailieuId")
                        .HasColumnType("int");

                    b.Property<string>("Tieude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CauhoiId");

                    b.HasIndex("TailieuId");

                    b.HasIndex("UserId");

                    b.ToTable("CauhoiVandap");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.CauhoiYeuthich_Db", b =>
                {
                    b.Property<int>("CauhoiYeuthichID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CauhoiYeuthichID"), 1L, 1);

                    b.Property<int?>("CauhoiId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CauhoiYeuthichID");

                    b.HasIndex("CauhoiId");

                    b.HasIndex("UserId");

                    b.ToTable("CauhoiYeuthich");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Cautrl_Db", b =>
                {
                    b.Property<int>("CautrlID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CautrlID"), 1L, 1);

                    b.Property<int?>("CauhoiId")
                        .HasColumnType("int");

                    b.Property<string>("Cautrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Ngaytao")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CautrlID");

                    b.HasIndex("CauhoiId");

                    b.HasIndex("UserId");

                    b.ToTable("Cautrl");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Chude_Db", b =>
                {
                    b.Property<int>("ChudeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChudeID"), 1L, 1);

                    b.Property<int?>("Monhoc_Id")
                        .HasColumnType("int");

                    b.Property<string>("Tenchude")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ChudeID");

                    b.HasIndex("Monhoc_Id");

                    b.ToTable("Chude");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Dethi_Db", b =>
                {
                    b.Property<int>("DethiID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DethiID"), 1L, 1);

                    b.Property<int?>("FileId")
                        .HasColumnType("int");

                    b.Property<string>("Madethi")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("MonhocID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Ngayduyet")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ngaytao")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Nguoiduyet")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("DethiID");

                    b.HasIndex("FileId");

                    b.HasIndex("MonhocID");

                    b.HasIndex("UserID");

                    b.ToTable("Dethi");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Ex_QA_Db", b =>
                {
                    b.Property<int>("EXQAID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EXQAID"), 1L, 1);

                    b.Property<int?>("DethiID")
                        .HasColumnType("int");

                    b.Property<int?>("QAID")
                        .HasColumnType("int");

                    b.HasKey("EXQAID");

                    b.HasIndex("DethiID");

                    b.HasIndex("QAID");

                    b.ToTable("Ex_QA");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.File_Dethi_Db", b =>
                {
                    b.Property<int?>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("FileId"), 1L, 1);

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Size")
                        .HasColumnType("float");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("User_Id");

                    b.ToTable("FileDethi");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Help_Db", b =>
                {
                    b.Property<int>("HelpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HelpID"), 1L, 1);

                    b.Property<DateTime>("NgayGui")
                        .HasColumnType("datetime2");

                    b.Property<string>("Noidung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tieude")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("HelpID");

                    b.HasIndex("UserID");

                    b.ToTable("Help");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Hocvien_Lop_Db", b =>
                {
                    b.Property<int>("HvLopID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HvLopID"), 1L, 1);

                    b.Property<int?>("Lopgiang_Id")
                        .HasColumnType("int");

                    b.Property<int?>("LopgiangdayID")
                        .HasColumnType("int");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("HvLopID");

                    b.HasIndex("LopgiangdayID");

                    b.HasIndex("User_Id");

                    b.ToTable("Hocvien_Lop");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Lopgiangday_Db", b =>
                {
                    b.Property<int>("LopgiangdayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LopgiangdayID"), 1L, 1);

                    b.Property<string>("Malop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MonhocID")
                        .HasColumnType("int");

                    b.Property<string>("TenLop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Thoigian")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Truycapgannhat")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("LopgiangdayID");

                    b.HasIndex("MonhocID");

                    b.HasIndex("UserID");

                    b.ToTable("LopGiangday");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Monhoc_Db", b =>
                {
                    b.Property<int>("MonhocID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonhocID"), 1L, 1);

                    b.Property<string>("MaMonhoc")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenMonhoc")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Tinhtrang")
                        .HasColumnType("int");

                    b.Property<int?>("TobomonId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Truycapgannhat")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MonhocID");

                    b.HasIndex("TobomonId");

                    b.HasIndex("UserId");

                    b.ToTable("Monhoc");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.MonhocYeuthich_Db", b =>
                {
                    b.Property<int>("MonhocYeuthichID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonhocYeuthichID"), 1L, 1);

                    b.Property<int?>("MonhocId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<bool?>("Yeuthich")
                        .HasColumnType("bit");

                    b.HasKey("MonhocYeuthichID");

                    b.HasIndex("MonhocId");

                    b.HasIndex("UserId");

                    b.ToTable("MonhocYeuthich");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.QA_Db", b =>
                {
                    b.Property<int>("QAID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QAID"), 1L, 1);

                    b.Property<string>("Cauhoi")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Cautrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Lancuoisua")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MonhocID")
                        .HasColumnType("int");

                    b.HasKey("QAID");

                    b.HasIndex("MonhocID");

                    b.ToTable("QA");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Role_Db", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("Mota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Phanquyen")
                        .HasColumnType("int");

                    b.Property<string>("Tenvaitro")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tailieu_Baigiang_Db", b =>
                {
                    b.Property<int>("DocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocId"), 1L, 1);

                    b.Property<int?>("ChudeID")
                        .HasColumnType("int");

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Kichthuoc")
                        .HasColumnType("float");

                    b.Property<int?>("MonhocID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayDuyet")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Nguoiduyet")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Sualancuoi")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenDoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DocId");

                    b.HasIndex("ChudeID");

                    b.HasIndex("MonhocID");

                    b.HasIndex("UserId");

                    b.ToTable("Tailieu_Baigiang");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Thongbao_Db", b =>
                {
                    b.Property<int>("ThongbaoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThongbaoID"), 1L, 1);

                    b.Property<string>("Noidung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Thoigian")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tieude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ThongbaoID");

                    b.HasIndex("UserID");

                    b.ToTable("Thongbao");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.ThongbaoLop_Db", b =>
                {
                    b.Property<int>("ThongbaoLopID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThongbaoLopID"), 1L, 1);

                    b.Property<int?>("Lopgiang_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Thongbao_Id")
                        .HasColumnType("int");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("ThongbaoLopID");

                    b.HasIndex("Lopgiang_Id");

                    b.HasIndex("Thongbao_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("ThongbaoLop");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tobomon_Db", b =>
                {
                    b.Property<int>("TobomonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TobomonId"), 1L, 1);

                    b.Property<string>("TobomonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TobomonId");

                    b.ToTable("Tobomon");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tongquan_Db", b =>
                {
                    b.Property<int>("TongquanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TongquanID"), 1L, 1);

                    b.Property<int?>("Monhoc_Id")
                        .HasColumnType("int");

                    b.Property<string>("Noidung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tieude")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TongquanID");

                    b.HasIndex("Monhoc_Id");

                    b.ToTable("Tongquan");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.User_Db", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Avt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AvtId")
                        .HasColumnType("int");

                    b.Property<string>("Diachi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Gioitinh")
                        .HasColumnType("bit");

                    b.Property<string>("MaUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nganh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Ngaysuadoi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("Role");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Avt_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("ListAvt")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Avt_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.CauhoiVandap_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Tailieu_Baigiang_Db", "Tailieu")
                        .WithMany("list_Cauhoivandap")
                        .HasForeignKey("TailieuId");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_CauhoiVandap")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Cauhoicandap_user");

                    b.Navigation("Tailieu");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.CauhoiYeuthich_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.CauhoiVandap_Db", "Cauhoi")
                        .WithMany("list_Cauhoiyeuthich")
                        .HasForeignKey("CauhoiId");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_Cauhoiyeuthich")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_cauhoiyeuthcih_user");

                    b.Navigation("Cauhoi");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Cautrl_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.CauhoiVandap_Db", "Cauhoi")
                        .WithMany("list_Cautrl")
                        .HasForeignKey("CauhoiId");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_Cautrl")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Cautrl_user");

                    b.Navigation("Cauhoi");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Chude_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("list_Chude")
                        .HasForeignKey("Monhoc_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_chude_Monhoc");

                    b.Navigation("Monhoc");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Dethi_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.File_Dethi_Db", "File_Dethi")
                        .WithMany("listDethi")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Dethi_File");

                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("ListDethi")
                        .HasForeignKey("MonhocID");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("ListDethi")
                        .HasForeignKey("UserID");

                    b.Navigation("File_Dethi");

                    b.Navigation("Monhoc");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Ex_QA_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Dethi_Db", "Dethi")
                        .WithMany("ListExQA")
                        .HasForeignKey("DethiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Ex_Qa_Dethi");

                    b.HasOne("LMS_ELibrary.Data.QA_Db", "QA")
                        .WithMany("ListExQA")
                        .HasForeignKey("QAID");

                    b.Navigation("Dethi");

                    b.Navigation("QA");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.File_Dethi_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_File_Dethi")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_File_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Help_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("ListHelp")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Hocvien_Lop_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Lopgiangday_Db", "Lopgiang")
                        .WithMany("List_HocvienLop")
                        .HasForeignKey("LopgiangdayID");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_HocvienLop")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_hocvienlop_user");

                    b.Navigation("Lopgiang");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Lopgiangday_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("ListLopgiangday")
                        .HasForeignKey("MonhocID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_lop_Monhoc");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("ListLopgiangday")
                        .HasForeignKey("UserID");

                    b.Navigation("Monhoc");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Monhoc_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Tobomon_Db", "Tobomon")
                        .WithMany("ListMonhoc")
                        .HasForeignKey("TobomonId");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "GiangVien")
                        .WithMany("list_Mongiangday")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Monhoc_user");

                    b.Navigation("GiangVien");

                    b.Navigation("Tobomon");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.MonhocYeuthich_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("List_Monhocyeuthich")
                        .HasForeignKey("MonhocId");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_Monhocyeuthich")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_MonYeuthich_User");

                    b.Navigation("Monhoc");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.QA_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("ListCauhoi")
                        .HasForeignKey("MonhocID");

                    b.Navigation("Monhoc");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tailieu_Baigiang_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Chude_Db", "Chude")
                        .WithMany("ListTailieu_Baigiang")
                        .HasForeignKey("ChudeID");

                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("ListTailieu_Baigiang")
                        .HasForeignKey("MonhocID");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("ListTailieu_Baigiang")
                        .HasForeignKey("UserId");

                    b.Navigation("Chude");

                    b.Navigation("Monhoc");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Thongbao_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("ListThongbao")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.ThongbaoLop_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Lopgiangday_Db", "Lopgiang")
                        .WithMany("list_ThongbaoLop")
                        .HasForeignKey("Lopgiang_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_thongbaolop_lop");

                    b.HasOne("LMS_ELibrary.Data.Thongbao_Db", "Thongbao")
                        .WithMany("List_ThongbaoLop")
                        .HasForeignKey("Thongbao_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_thongbaolop_thongbao");

                    b.HasOne("LMS_ELibrary.Data.User_Db", "User")
                        .WithMany("list_Thongbao")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_thongbaolop_user");

                    b.Navigation("Lopgiang");

                    b.Navigation("Thongbao");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tongquan_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Monhoc_Db", "Monhoc")
                        .WithMany("list_Tongquan")
                        .HasForeignKey("Monhoc_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_tongquan_monhoc");

                    b.Navigation("Monhoc");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.User_Db", b =>
                {
                    b.HasOne("LMS_ELibrary.Data.Role_Db", "RoleDb")
                        .WithMany("listUser")
                        .HasForeignKey("Role")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_user_role");

                    b.Navigation("RoleDb");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.CauhoiVandap_Db", b =>
                {
                    b.Navigation("list_Cauhoiyeuthich");

                    b.Navigation("list_Cautrl");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Chude_Db", b =>
                {
                    b.Navigation("ListTailieu_Baigiang");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Dethi_Db", b =>
                {
                    b.Navigation("ListExQA");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.File_Dethi_Db", b =>
                {
                    b.Navigation("listDethi");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Lopgiangday_Db", b =>
                {
                    b.Navigation("List_HocvienLop");

                    b.Navigation("list_ThongbaoLop");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Monhoc_Db", b =>
                {
                    b.Navigation("ListCauhoi");

                    b.Navigation("ListDethi");

                    b.Navigation("ListLopgiangday");

                    b.Navigation("ListTailieu_Baigiang");

                    b.Navigation("List_Monhocyeuthich");

                    b.Navigation("list_Chude");

                    b.Navigation("list_Tongquan");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.QA_Db", b =>
                {
                    b.Navigation("ListExQA");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Role_Db", b =>
                {
                    b.Navigation("listUser");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tailieu_Baigiang_Db", b =>
                {
                    b.Navigation("list_Cauhoivandap");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Thongbao_Db", b =>
                {
                    b.Navigation("List_ThongbaoLop");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.Tobomon_Db", b =>
                {
                    b.Navigation("ListMonhoc");
                });

            modelBuilder.Entity("LMS_ELibrary.Data.User_Db", b =>
                {
                    b.Navigation("ListAvt");

                    b.Navigation("ListDethi");

                    b.Navigation("ListHelp");

                    b.Navigation("ListLopgiangday");

                    b.Navigation("ListTailieu_Baigiang");

                    b.Navigation("ListThongbao");

                    b.Navigation("list_CauhoiVandap");

                    b.Navigation("list_Cauhoiyeuthich");

                    b.Navigation("list_Cautrl");

                    b.Navigation("list_File_Dethi");

                    b.Navigation("list_HocvienLop");

                    b.Navigation("list_Mongiangday");

                    b.Navigation("list_Monhocyeuthich");

                    b.Navigation("list_Thongbao");
                });
#pragma warning restore 612, 618
        }
    }
}
