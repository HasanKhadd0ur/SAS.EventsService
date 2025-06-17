using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNamedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NamedEntityType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalisedName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedEntityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NamedEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Named = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamedEntity_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NamedEntity_NamedEntityType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NamedEntityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NamedEntityMention",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamedEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedEntityMention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NamedEntityMention_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NamedEntityMention_NamedEntity_NamedEntityId",
                        column: x => x.NamedEntityId,
                        principalTable: "NamedEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NamedEntity_EventId",
                table: "NamedEntity",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedEntity_TypeId",
                table: "NamedEntity",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedEntityMention_EventId",
                table: "NamedEntityMention",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedEntityMention_NamedEntityId",
                table: "NamedEntityMention",
                column: "NamedEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NamedEntityMention");

            migrationBuilder.DropTable(
                name: "NamedEntity");

            migrationBuilder.DropTable(
                name: "NamedEntityType");
        }
    }
}
