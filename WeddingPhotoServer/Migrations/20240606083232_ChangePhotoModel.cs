using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPhotoServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangePhotoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Photos",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Photos",
                newName: "ImageUrl");
        }
    }
}
