using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Padrinly.Migrations
{
    /// <inheritdoc />
    public partial class _Migration_PasswordNotMapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Persons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
