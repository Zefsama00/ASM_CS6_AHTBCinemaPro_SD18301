﻿// <auto-generated />
using System;
using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Migrations
{
    [DbContext(typeof(DBCinemaContext))]
    [Migration("20240725060238_dbcs6")]
    partial class dbcs6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.CaChieu", b =>
                {
                    b.Property<int>("IdCaChieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("NgayChieu")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phim")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phong")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCaChieu");

                    b.HasIndex("Phim");

                    b.HasIndex("Phong");

                    b.ToTable("CaChieus");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ghe", b =>
                {
                    b.Property<string>("IdGhe")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoaiGhe")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phong")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenGhe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGhe");

                    b.HasIndex("LoaiGhe");

                    b.HasIndex("Phong");

                    b.ToTable("Ghes");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.GioChieu", b =>
                {
                    b.Property<int>("IdGioChieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cachieu")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("GioBatDau")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("GioKetThuc")
                        .HasColumnType("time");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGioChieu");

                    b.HasIndex("Cachieu");

                    b.ToTable("GioChieus");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.HoaDon", b =>
                {
                    b.Property<int>("IdHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdVe")
                        .HasColumnType("int");

                    b.Property<string>("KhachHang")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("KhuyenMai")
                        .HasColumnType("int");

                    b.Property<string>("NhanVien")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("TongTien")
                        .HasColumnType("real");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHD");

                    b.HasIndex("IdVe");

                    b.HasIndex("KhachHang");

                    b.HasIndex("KhuyenMai");

                    b.HasIndex("NhanVien");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhachHang", b =>
                {
                    b.Property<string>("IdKH")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NamSinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenKH")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKH");

                    b.HasIndex("IDUser");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhuyenMai", b =>
                {
                    b.Property<int>("IdKM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KhuyenMaiName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phantram")
                        .HasColumnType("int");

                    b.HasKey("IdKM");

                    b.ToTable("KhuyenMais");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.LoaiGhe", b =>
                {
                    b.Property<string>("IdLoaiGhe")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenLoaiGhe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLoaiGhe");

                    b.ToTable("LoaiGhes");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.LoaiPhim", b =>
                {
                    b.Property<string>("IdLP")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenLoai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLP");

                    b.ToTable("LoaiPhims");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.NhanVien", b =>
                {
                    b.Property<string>("IdNV")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IDUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NamSinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenNV")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdNV");

                    b.HasIndex("IDUser");

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phim", b =>
                {
                    b.Property<string>("IdPhim")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DangPhim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DienVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenPhim")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TheLoai")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ThoiLuong")
                        .HasColumnType("int");

                    b.HasKey("IdPhim");

                    b.HasIndex("TheLoai");

                    b.ToTable("Phims");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phong", b =>
                {
                    b.Property<string>("IdPhong")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SoLuongGhe")
                        .HasColumnType("int");

                    b.Property<int>("SoPhong")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPhong");

                    b.ToTable("Phongs");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.User", b =>
                {
                    b.Property<string>("IdUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ve", b =>
                {
                    b.Property<int>("IdVe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ghe")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("GiaVe")
                        .HasColumnType("real");

                    b.Property<int>("SuatChieu")
                        .HasColumnType("int");

                    b.Property<string>("TenVe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdVe");

                    b.HasIndex("Ghe")
                        .IsUnique()
                        .HasFilter("[Ghe] IS NOT NULL");

                    b.HasIndex("SuatChieu");

                    b.ToTable("Ves");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.CaChieu", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phim", "Phims")
                        .WithMany("CaChieus")
                        .HasForeignKey("Phim");

                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phong", "Phongs")
                        .WithMany("CaChieu")
                        .HasForeignKey("Phong");

                    b.Navigation("Phims");

                    b.Navigation("Phongs");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ghe", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.LoaiGhe", "LoaiGhes")
                        .WithMany("Ghes")
                        .HasForeignKey("LoaiGhe");

                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phong", "Phongs")
                        .WithMany("Ghes")
                        .HasForeignKey("Phong");

                    b.Navigation("LoaiGhes");

                    b.Navigation("Phongs");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.GioChieu", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.CaChieu", "CaChieus")
                        .WithMany("GioChieus")
                        .HasForeignKey("Cachieu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaChieus");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.HoaDon", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ve", "Ve")
                        .WithMany("HoaDons")
                        .HasForeignKey("IdVe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhachHang", "KhachHangs")
                        .WithMany("HoaDons")
                        .HasForeignKey("KhachHang");

                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhuyenMai", "KhuyenMais")
                        .WithMany("HoaDons")
                        .HasForeignKey("KhuyenMai");

                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.NhanVien", "NhanViens")
                        .WithMany("HoaDons")
                        .HasForeignKey("NhanVien");

                    b.Navigation("KhachHangs");

                    b.Navigation("KhuyenMais");

                    b.Navigation("NhanViens");

                    b.Navigation("Ve");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhachHang", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.User", "Users")
                        .WithMany("KhachHangs")
                        .HasForeignKey("IDUser");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.NhanVien", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.User", "Users")
                        .WithMany("NhanViens")
                        .HasForeignKey("IDUser");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phim", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.LoaiPhim", "LoaiPhim")
                        .WithMany("Phims")
                        .HasForeignKey("TheLoai");

                    b.Navigation("LoaiPhim");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ve", b =>
                {
                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ghe", "Ghes")
                        .WithOne("Ves")
                        .HasForeignKey("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ve", "Ghe");

                    b.HasOne("ASM_CS6_AHTBCinemaPro_SD18301.Models.GioChieu", "GioChieus")
                        .WithMany("Ves")
                        .HasForeignKey("SuatChieu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ghes");

                    b.Navigation("GioChieus");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.CaChieu", b =>
                {
                    b.Navigation("GioChieus");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ghe", b =>
                {
                    b.Navigation("Ves");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.GioChieu", b =>
                {
                    b.Navigation("Ves");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhachHang", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.KhuyenMai", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.LoaiGhe", b =>
                {
                    b.Navigation("Ghes");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.LoaiPhim", b =>
                {
                    b.Navigation("Phims");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.NhanVien", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phim", b =>
                {
                    b.Navigation("CaChieus");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Phong", b =>
                {
                    b.Navigation("CaChieu");

                    b.Navigation("Ghes");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.User", b =>
                {
                    b.Navigation("KhachHangs");

                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("ASM_CS6_AHTBCinemaPro_SD18301.Models.Ve", b =>
                {
                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
