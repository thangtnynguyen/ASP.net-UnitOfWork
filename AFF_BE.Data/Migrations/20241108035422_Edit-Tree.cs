using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditTree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9756506c-8231-4bb9-b510-a1fcbf55f5d5", "AQAAAAIAAYagAAAAEBs3Q5D4xrojvIf5o6m/HF3c2GS7LcVD3dwfqN7ZUwNhyRbJ+JY0UImKWjyDzsB5YQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_TreeNodes_UserId",
                table: "TreeNodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreeNodes_Users_UserId",
                table: "TreeNodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreeNodes_Users_UserId",
                table: "TreeNodes");

            migrationBuilder.DropIndex(
                name: "IX_TreeNodes_UserId",
                table: "TreeNodes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ad844a83-bc3b-42b9-a2be-69d4e99bc557", "AQAAAAIAAYagAAAAELowyHP7lLaF5y3hZJHgULEJxVcBQF4s9wItYum0N7KbTEeQdZQg1mRuR4deSn4ysg==" });
        }
    }
}
