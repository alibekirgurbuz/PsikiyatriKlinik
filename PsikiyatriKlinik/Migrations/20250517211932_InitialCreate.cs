using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsikiyatriKlinik.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ilaclar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlacAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IlacTuru = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StokDurumu = table.Column<int>(type: "int", nullable: false),
                    Doz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullanimSikligi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilaclar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullaniciTipi = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    YetkiSeviyesi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notlar = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UzmanlikAlani = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Doktor_Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SicilNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    KlinikAd = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KanGrubu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AcilDurumKisi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AcilDurumTelefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastaId = table.Column<int>(type: "int", nullable: false),
                    DoktorId = table.Column<int>(type: "int", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RandevuSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    RandevuDurumu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanicilar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanicilar_HastaId",
                        column: x => x.HastaId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Terapiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerapiTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerapiSuresi = table.Column<TimeSpan>(type: "time", nullable: false),
                    TerapistNotu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DoktorId = table.Column<int>(type: "int", nullable: false),
                    HastaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terapiler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terapiler_Kullanicilar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Terapiler_Kullanicilar_HastaId",
                        column: x => x.HastaId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_DoktorId",
                table: "Randevular",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HastaId",
                table: "Randevular",
                column: "HastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Terapiler_DoktorId",
                table: "Terapiler",
                column: "DoktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Terapiler_HastaId",
                table: "Terapiler",
                column: "HastaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ilaclar");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Terapiler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
