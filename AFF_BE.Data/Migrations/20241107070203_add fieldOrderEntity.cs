using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class addfieldOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "928d1b50-7322-47d6-8f25-9b901521684e", "AQAAAAIAAYagAAAAEKmXU0nTAIgxbp2WS4ldowCGLANfGxpvuUiueVTbaBCvj1f88IYt78gh8mQfoNFZwA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1233556e-62e3-494d-9266-5772bf8414fd", "AQAAAAIAAYagAAAAEB1bU5c2XoqDqhbFycW3n/YYrZ3DisGZGJRa4dyBuh+ZX0yoQ5c6+GY1zmkStkOq9w==" });
        }
    }
}
