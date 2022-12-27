using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inSync.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ItemLists",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLockedByAdmin",
                table: "ItemLists",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LockReason",
                table: "ItemLists",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ItemLists",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MinecraftItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ResourceKey = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinecraftItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinecraftItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ItemLists");

            migrationBuilder.DropColumn(
                name: "IsLockedByAdmin",
                table: "ItemLists");

            migrationBuilder.DropColumn(
                name: "LockReason",
                table: "ItemLists");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ItemLists");
        }
    }
}
