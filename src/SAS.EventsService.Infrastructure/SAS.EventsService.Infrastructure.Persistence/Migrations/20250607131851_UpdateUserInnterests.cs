using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserInnterests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Regions_RegionId",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Regions_RegionId1",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_UserInterests_RegionId1",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "RegionId1",
                table: "UserInterests");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "UserInterests",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_RegionId",
                table: "UserInterests",
                newName: "IX_UserInterests_LocationId");

            migrationBuilder.AddColumn<string>(
                name: "InterestName",
                table: "UserInterests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RadiusInKm",
                table: "UserInterests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Locations_LocationId",
                table: "UserInterests",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Locations_LocationId",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "InterestName",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "RadiusInKm",
                table: "UserInterests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Topics");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "UserInterests",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_LocationId",
                table: "UserInterests",
                newName: "IX_UserInterests_RegionId");

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId1",
                table: "UserInterests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_RegionId1",
                table: "UserInterests",
                column: "RegionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Regions_RegionId",
                table: "UserInterests",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Regions_RegionId1",
                table: "UserInterests",
                column: "RegionId1",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
