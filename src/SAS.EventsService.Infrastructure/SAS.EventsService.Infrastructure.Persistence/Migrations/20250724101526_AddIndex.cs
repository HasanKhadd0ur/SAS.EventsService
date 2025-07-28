using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Events_TopicId",
                table: "Events",
                newName: "IX_Event_TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_Name",
                table: "Topics",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Topic_Name",
                table: "Topics");

            migrationBuilder.RenameIndex(
                name: "IX_Event_TopicId",
                table: "Events",
                newName: "IX_Events_TopicId");
        }
    }
}
