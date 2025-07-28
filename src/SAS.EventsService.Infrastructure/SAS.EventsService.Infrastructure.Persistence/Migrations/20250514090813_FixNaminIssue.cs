using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    public partial class FixNaminIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdatedAT",
                table: "Events",
                newName: "LastUpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAT",
                table: "Events",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "EventInfo_SentimentLabel",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventInfo_SentimentLabel",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedAt",
                table: "Events",
                newName: "LastUpdatedAT");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Events",
                newName: "CreatedAT");
        }
    }
}
