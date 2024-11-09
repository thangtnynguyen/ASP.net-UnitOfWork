using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class TreeNoteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchPath",
                table: "TreeNodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "66e2a9da-fddf-4427-b420-7ef064692e26", "AQAAAAIAAYagAAAAENb6rPRkJUTF/XNdZoRZHOREG5IHS7wLUxWnYAdCdT26UUT2jCwrEiXmgatLkQMGCg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchPath",
                table: "TreeNodes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "46c9022f-7d03-4c64-a1de-9fce97443ecb", "AQAAAAIAAYagAAAAEFjvzYZxFB+1uK2loQHSO0QImHPCDZJYZZUgg5Emcrs0VcxQaDLHc98p2PRNMYebEw==" });
        }
    }
}
