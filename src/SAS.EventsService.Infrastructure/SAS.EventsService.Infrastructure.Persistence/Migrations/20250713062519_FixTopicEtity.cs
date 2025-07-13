using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTopicEtity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Lat_Lon",
                table: "Locations",
                columns: new[] { "Latitude", "Longitude" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Location_Lat_Lon",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Topics");
        }
    }
}
