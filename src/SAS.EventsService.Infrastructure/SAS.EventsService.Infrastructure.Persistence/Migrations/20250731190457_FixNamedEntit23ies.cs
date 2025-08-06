using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.EventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixNamedEntit23ies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntity_Events_EventId",
                table: "NamedEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntity_NamedEntityType_TypeId",
                table: "NamedEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntityMention_Events_EventId",
                table: "NamedEntityMention");

            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntityMention_NamedEntity_NamedEntityId",
                table: "NamedEntityMention");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamedEntityType",
                table: "NamedEntityType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamedEntityMention",
                table: "NamedEntityMention");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamedEntity",
                table: "NamedEntity");

            migrationBuilder.RenameTable(
                name: "NamedEntityType",
                newName: "NamedEntityTypes");

            migrationBuilder.RenameTable(
                name: "NamedEntityMention",
                newName: "NamedEntityMentions");

            migrationBuilder.RenameTable(
                name: "NamedEntity",
                newName: "NamedEntities");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntityMention_NamedEntityId",
                table: "NamedEntityMentions",
                newName: "IX_NamedEntityMentions_NamedEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntityMention_EventId",
                table: "NamedEntityMentions",
                newName: "IX_NamedEntityMentions_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntity_TypeId",
                table: "NamedEntities",
                newName: "IX_NamedEntities_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntity_EventId",
                table: "NamedEntities",
                newName: "IX_NamedEntities_EventId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMentionedAt",
                table: "NamedEntities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamedEntityTypes",
                table: "NamedEntityTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamedEntityMentions",
                table: "NamedEntityMentions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamedEntities",
                table: "NamedEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntities_Events_EventId",
                table: "NamedEntities",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntities_NamedEntityTypes_TypeId",
                table: "NamedEntities",
                column: "TypeId",
                principalTable: "NamedEntityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntityMentions_Events_EventId",
                table: "NamedEntityMentions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntityMentions_NamedEntities_NamedEntityId",
                table: "NamedEntityMentions",
                column: "NamedEntityId",
                principalTable: "NamedEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntities_Events_EventId",
                table: "NamedEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntities_NamedEntityTypes_TypeId",
                table: "NamedEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntityMentions_Events_EventId",
                table: "NamedEntityMentions");

            migrationBuilder.DropForeignKey(
                name: "FK_NamedEntityMentions_NamedEntities_NamedEntityId",
                table: "NamedEntityMentions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamedEntityTypes",
                table: "NamedEntityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamedEntityMentions",
                table: "NamedEntityMentions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamedEntities",
                table: "NamedEntities");

            migrationBuilder.DropColumn(
                name: "LastMentionedAt",
                table: "NamedEntities");

            migrationBuilder.RenameTable(
                name: "NamedEntityTypes",
                newName: "NamedEntityType");

            migrationBuilder.RenameTable(
                name: "NamedEntityMentions",
                newName: "NamedEntityMention");

            migrationBuilder.RenameTable(
                name: "NamedEntities",
                newName: "NamedEntity");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntityMentions_NamedEntityId",
                table: "NamedEntityMention",
                newName: "IX_NamedEntityMention_NamedEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntityMentions_EventId",
                table: "NamedEntityMention",
                newName: "IX_NamedEntityMention_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntities_TypeId",
                table: "NamedEntity",
                newName: "IX_NamedEntity_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_NamedEntities_EventId",
                table: "NamedEntity",
                newName: "IX_NamedEntity_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamedEntityType",
                table: "NamedEntityType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamedEntityMention",
                table: "NamedEntityMention",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamedEntity",
                table: "NamedEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntity_Events_EventId",
                table: "NamedEntity",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntity_NamedEntityType_TypeId",
                table: "NamedEntity",
                column: "TypeId",
                principalTable: "NamedEntityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntityMention_Events_EventId",
                table: "NamedEntityMention",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NamedEntityMention_NamedEntity_NamedEntityId",
                table: "NamedEntityMention",
                column: "NamedEntityId",
                principalTable: "NamedEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
