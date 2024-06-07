using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPhotoServer.Migrations
{
    /// <inheritdoc />
    public partial class PublicArgument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Photos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Photos");
        }
    }
}
