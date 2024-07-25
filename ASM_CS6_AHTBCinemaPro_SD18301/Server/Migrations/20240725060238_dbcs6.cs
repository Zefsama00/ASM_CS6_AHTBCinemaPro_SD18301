using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Migrations
{
    public partial class dbcs6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhuyenMais",
                columns: table => new
                {
                    IdKM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhuyenMaiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phantram = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMais", x => x.IdKM);
                });

            migrationBuilder.CreateTable(
                name: "LoaiGhes",
                columns: table => new
                {
                    IdLoaiGhe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLoaiGhe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGhes", x => x.IdLoaiGhe);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhims",
                columns: table => new
                {
                    IdLP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhims", x => x.IdLP);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    IdPhong = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuongGhe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.IdPhong);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Phims",
                columns: table => new
                {
                    IdPhim = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenPhim = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DienVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DangPhim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TheLoai = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ThoiLuong = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phims", x => x.IdPhim);
                    table.ForeignKey(
                        name: "FK_Phims_LoaiPhims_TheLoai",
                        column: x => x.TheLoai,
                        principalTable: "LoaiPhims",
                        principalColumn: "IdLP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ghes",
                columns: table => new
                {
                    IdGhe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenGhe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phong = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiGhe = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghes", x => x.IdGhe);
                    table.ForeignKey(
                        name: "FK_Ghes_LoaiGhes_LoaiGhe",
                        column: x => x.LoaiGhe,
                        principalTable: "LoaiGhes",
                        principalColumn: "IdLoaiGhe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ghes_Phongs_Phong",
                        column: x => x.Phong,
                        principalTable: "Phongs",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    IdKH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NamSinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.IdKH);
                    table.ForeignKey(
                        name: "FK_KhachHangs_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    IdNV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenNV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NamSinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDUser = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.IdNV);
                    table.ForeignKey(
                        name: "FK_NhanViens_Users_IDUser",
                        column: x => x.IDUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaChieus",
                columns: table => new
                {
                    IdCaChieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phong = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Phim = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NgayChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaChieus", x => x.IdCaChieu);
                    table.ForeignKey(
                        name: "FK_CaChieus_Phims_Phim",
                        column: x => x.Phim,
                        principalTable: "Phims",
                        principalColumn: "IdPhim",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaChieus_Phongs_Phong",
                        column: x => x.Phong,
                        principalTable: "Phongs",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GioChieus",
                columns: table => new
                {
                    IdGioChieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GioBatDau = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioKetThuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    Cachieu = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioChieus", x => x.IdGioChieu);
                    table.ForeignKey(
                        name: "FK_GioChieus_CaChieus_Cachieu",
                        column: x => x.Cachieu,
                        principalTable: "CaChieus",
                        principalColumn: "IdCaChieu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ves",
                columns: table => new
                {
                    IdVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaVe = table.Column<float>(type: "real", nullable: false),
                    SuatChieu = table.Column<int>(type: "int", nullable: false),
                    Ghe = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ves", x => x.IdVe);
                    table.ForeignKey(
                        name: "FK_Ves_Ghes_Ghe",
                        column: x => x.Ghe,
                        principalTable: "Ghes",
                        principalColumn: "IdGhe",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ves_GioChieus_SuatChieu",
                        column: x => x.SuatChieu,
                        principalTable: "GioChieus",
                        principalColumn: "IdGioChieu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    IdHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVe = table.Column<int>(type: "int", nullable: false),
                    NhanVien = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    KhachHang = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    KhuyenMai = table.Column<int>(type: "int", nullable: true),
                    TongTien = table.Column<float>(type: "real", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.IdHD);
                    table.ForeignKey(
                        name: "FK_HoaDons_KhachHangs_KhachHang",
                        column: x => x.KhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "IdKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDons_KhuyenMais_KhuyenMai",
                        column: x => x.KhuyenMai,
                        principalTable: "KhuyenMais",
                        principalColumn: "IdKM",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDons_NhanViens_NhanVien",
                        column: x => x.NhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "IdNV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDons_Ves_IdVe",
                        column: x => x.IdVe,
                        principalTable: "Ves",
                        principalColumn: "IdVe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaChieus_Phim",
                table: "CaChieus",
                column: "Phim");

            migrationBuilder.CreateIndex(
                name: "IX_CaChieus_Phong",
                table: "CaChieus",
                column: "Phong");

            migrationBuilder.CreateIndex(
                name: "IX_Ghes_LoaiGhe",
                table: "Ghes",
                column: "LoaiGhe");

            migrationBuilder.CreateIndex(
                name: "IX_Ghes_Phong",
                table: "Ghes",
                column: "Phong");

            migrationBuilder.CreateIndex(
                name: "IX_GioChieus_Cachieu",
                table: "GioChieus",
                column: "Cachieu");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_IdVe",
                table: "HoaDons",
                column: "IdVe");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_KhachHang",
                table: "HoaDons",
                column: "KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_KhuyenMai",
                table: "HoaDons",
                column: "KhuyenMai");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_NhanVien",
                table: "HoaDons",
                column: "NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_IDUser",
                table: "KhachHangs",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_IDUser",
                table: "NhanViens",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Phims_TheLoai",
                table: "Phims",
                column: "TheLoai");

            migrationBuilder.CreateIndex(
                name: "IX_Ves_Ghe",
                table: "Ves",
                column: "Ghe",
                unique: true,
                filter: "[Ghe] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ves_SuatChieu",
                table: "Ves",
                column: "SuatChieu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "KhuyenMais");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "Ves");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ghes");

            migrationBuilder.DropTable(
                name: "GioChieus");

            migrationBuilder.DropTable(
                name: "LoaiGhes");

            migrationBuilder.DropTable(
                name: "CaChieus");

            migrationBuilder.DropTable(
                name: "Phims");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "LoaiPhims");
        }
    }
}
