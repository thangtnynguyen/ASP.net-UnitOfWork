using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class ComissionEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "IndirectCommissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b147ad07-7e6b-477e-a83d-9892cd0d3534", "AQAAAAIAAYagAAAAENcuBzP1yJyADuqR9QKhGQlXLMCdEX/WZxIBYicVyeUmsPmih1/qQSuCQWETCL+iQA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "IndirectCommissions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a63ccdc6-2283-41ec-ad05-00d678275b73", "AQAAAAIAAYagAAAAEMonDprxb7PhqsU/dF2FYnrReG+mBEVkgH/Puhowcr8gIwBky3gE8zCdJaMtrKAXeA==" });
        }
    }
}
