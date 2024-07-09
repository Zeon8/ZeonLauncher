using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Launcher.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemovePatchExebutable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exebutable",
                table: "Updates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exebutable",
                table: "Updates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
