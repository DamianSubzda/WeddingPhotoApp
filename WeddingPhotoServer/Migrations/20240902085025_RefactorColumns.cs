using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPhotoServer.Migrations
{
    /// <inheritdoc />
    public partial class RefactorColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendBy",
                table: "Photos",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Photos",
                newName: "SendBy");
        }
    }
}
