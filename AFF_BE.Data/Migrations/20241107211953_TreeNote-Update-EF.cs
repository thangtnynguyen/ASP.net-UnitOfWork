using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class TreeNoteUpdateEF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TreeNodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "TreeNodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedName",
                table: "TreeNodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TreeNodes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TreeNodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "TreeNodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedName",
                table: "TreeNodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ad844a83-bc3b-42b9-a2be-69d4e99bc557", "AQAAAAIAAYagAAAAELowyHP7lLaF5y3hZJHgULEJxVcBQF4s9wItYum0N7KbTEeQdZQg1mRuR4deSn4ysg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "CreatedName",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TreeNodes");

            migrationBuilder.DropColumn(
                name: "UpdatedName",
                table: "TreeNodes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66e2a9da-fddf-4427-b420-7ef064692e26", "AQAAAAIAAYagAAAAENb6rPRkJUTF/XNdZoRZHOREG5IHS7wLUxWnYAdCdT26UUT2jCwrEiXmgatLkQMGCg==" });
        }
    }
}
