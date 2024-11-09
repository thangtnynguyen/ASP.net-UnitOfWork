using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProductEntityk1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "IndirectCommissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "IndirectCommissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedName",
                table: "IndirectCommissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IndirectCommissions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "IndirectCommissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "IndirectCommissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedName",
                table: "IndirectCommissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DirectCommissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DirectCommissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedName",
                table: "DirectCommissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DirectCommissions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "DirectCommissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "DirectCommissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedName",
                table: "DirectCommissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "46c9022f-7d03-4c64-a1de-9fce97443ecb", "AQAAAAIAAYagAAAAEFjvzYZxFB+1uK2loQHSO0QImHPCDZJYZZUgg5Emcrs0VcxQaDLHc98p2PRNMYebEw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "CreatedName",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "UpdatedName",
                table: "IndirectCommissions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DirectCommissions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DirectCommissions");

            migrationBuilder.DropColumn(
                name: "CreatedName",
                table: "DirectCommissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DirectCommissions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DirectCommissions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DirectCommissions");

            migrationBuilder.DropColumn(
                name: "UpdatedName",
                table: "DirectCommissions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7493fcdc-e7dd-448d-ab83-23f41e89cb30", "AQAAAAIAAYagAAAAEJDPoQ/2ZhvroiefLMvg/f68u2SPvXOP4BmlrPKfkadc1+kpekNhIdSLt9KdF5iyrg==" });
        }
    }
}
