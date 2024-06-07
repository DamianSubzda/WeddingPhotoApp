using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPhotoServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangePhotoModelv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "Photos",
                newName: "AddToGallery");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Photos",
                newName: "FileDirectory");

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Photos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendBy",
                table: "Photos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "SendBy",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "FileDirectory",
                table: "Photos",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "AddToGallery",
                table: "Photos",
                newName: "IsPublic");
        }
    }
}
