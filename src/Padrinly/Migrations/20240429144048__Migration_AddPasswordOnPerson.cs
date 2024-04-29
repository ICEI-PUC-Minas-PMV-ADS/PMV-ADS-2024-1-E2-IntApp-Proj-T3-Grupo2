using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Padrinly.Migrations
{
    /// <inheritdoc />
    public partial class _Migration_AddPasswordOnPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StudentResponsibleViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentName = table.Column<string>(type: "text", nullable: false),
                    StudentFirstDocument = table.Column<int>(type: "integer", nullable: false),
                    StudentSecondtDocument = table.Column<int>(type: "integer", nullable: true),
                    StudentBirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsNewResponsible = table.Column<bool>(type: "boolean", nullable: false),
                    IdResponsible = table.Column<int>(type: "integer", nullable: true),
                    ResponsibleName = table.Column<string>(type: "text", nullable: false),
                    ResponsibleFirstDocument = table.Column<int>(type: "integer", nullable: false),
                    ResponsibleSecondtDocument = table.Column<int>(type: "integer", nullable: true),
                    ResponsibleBirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ResponsibleEmail = table.Column<string>(type: "text", nullable: false),
                    ResponsiblePhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Neighborhood = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Complement = table.Column<string>(type: "text", nullable: false),
                    SelectedPersonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentResponsibleViewModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentResponsibleViewModel");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Persons");
        }
    }
}
