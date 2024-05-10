using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padrinly.Migrations
{
    /// <inheritdoc />
    public partial class _Migration_AddAvatarFileNameInModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "StudentResponsibleViewModel",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "StudentResponsibleViewModel");
        }
    }
}
