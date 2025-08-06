using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntities_Events_EventId",
                table: "NamedEntities");

            migrationBuilder.DropIndex(
                name: "IX_NamedEntities_EventId",
                table: "NamedEntities");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "NamedEntities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "NamedEntities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NamedEntities_EventId",
                table: "NamedEntities",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntities_Events_EventId",
                table: "NamedEntities",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
