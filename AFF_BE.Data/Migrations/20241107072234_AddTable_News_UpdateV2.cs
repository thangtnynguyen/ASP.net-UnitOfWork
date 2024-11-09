using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFF_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_News_UpdateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "News",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "20235c3f-a2dc-403f-a699-1e6503f3139d", "AQAAAAIAAYagAAAAELb9eczXa95xTnYpAl9NdqVC/5azmOHISUO1iptaAw9iwCSLv1VdYtEZitfuFXK3nw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "News");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "19a05f8b-ce5c-4b73-9bcd-3543fe414597", "AQAAAAIAAYagAAAAEOMd5h8S96OuCAQVgYMfc2P82nFlS+qBKTtOm9AfBGYNpEH0tdIkDjFe4tQglGEcPw==" });
        }
    }
}
