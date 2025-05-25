using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PsikiyatriKlinik.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDogumTarihi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TcKimlikNo",
                table: "Kullanicilar",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TcKimlikNo",
                table: "Kullanicilar");
        }
    }
}
