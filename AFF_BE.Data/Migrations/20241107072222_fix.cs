using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a15007d7-c07d-42e8-9123-ecbb046b5138", "AQAAAAIAAYagAAAAEM9VRYr4UePtGQiQptSi/P9Yhx99Q7D/Ggc6iBWhLdnUnG3OB2BiqVntonbgZ2KkyA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "928d1b50-7322-47d6-8f25-9b901521684e", "AQAAAAIAAYagAAAAEKmXU0nTAIgxbp2WS4ldowCGLANfGxpvuUiueVTbaBCvj1f88IYt78gh8mQfoNFZwA==" });
        }
    }
}
