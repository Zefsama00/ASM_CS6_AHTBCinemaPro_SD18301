using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server.Migrations
{
    public partial class themdanhmucdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "GioChieus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    IdDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.IdDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucPhims",
                columns: table => new
                {
                    IDDanhMucPhim = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPhim = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdDanhMuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucPhims", x => x.IDDanhMucPhim);
                    table.ForeignKey(
                        name: "FK_DanhMucPhims_DanhMucs_IdDanhMuc",
                        column: x => x.IdDanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "IdDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhMucPhims_Phims_IdPhim",
                        column: x => x.IdPhim,
                        principalTable: "Phims",
                        principalColumn: "IdPhim",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucPhims_IdDanhMuc",
                table: "DanhMucPhims",
                column: "IdDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucPhims_IdPhim",
                table: "DanhMucPhims",
                column: "IdPhim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMucPhims");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "GioChieus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
