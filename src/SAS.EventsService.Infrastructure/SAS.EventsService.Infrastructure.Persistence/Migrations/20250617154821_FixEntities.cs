using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Named",
                table: "NamedEntity",
                newName: "EntityName");

            migrationBuilder.AlterColumn<string>(
                name: "NormalisedName",
                table: "NamedEntityType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityName",
                table: "NamedEntity",
                newName: "Named");

            migrationBuilder.AlterColumn<int>(
                name: "NormalisedName",
                table: "NamedEntityType",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
