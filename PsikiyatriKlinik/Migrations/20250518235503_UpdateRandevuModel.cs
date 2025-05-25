using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsikiyatriKlinik.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRandevuModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoktorAdi",
                table: "Randevular",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DoktorId1",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HastaId1",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_DoktorId1",
                table: "Randevular",
                column: "DoktorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HastaId1",
                table: "Randevular",
                column: "HastaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_DoktorId1",
                table: "Randevular",
                column: "DoktorId1",
                principalTable: "Kullanicilar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanicilar_HastaId1",
                table: "Randevular",
                column: "HastaId1",
                principalTable: "Kullanicilar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_DoktorId1",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanicilar_HastaId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_DoktorId1",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_HastaId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "DoktorAdi",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "DoktorId1",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "HastaId1",
                table: "Randevular");
        }
    }
}
