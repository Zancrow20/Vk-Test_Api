using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VkWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserGroups",
                keyColumn: "id",
                keyValue: 1,
                column: "code",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "UserGroups",
                keyColumn: "id",
                keyValue: 2,
                column: "code",
                value: "User");

            migrationBuilder.UpdateData(
                table: "UserStates",
                keyColumn: "id",
                keyValue: 1,
                column: "code",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "UserStates",
                keyColumn: "id",
                keyValue: 2,
                column: "code",
                value: "Blocked");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserGroups",
                keyColumn: "id",
                keyValue: 1,
                column: "code",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserGroups",
                keyColumn: "id",
                keyValue: 2,
                column: "code",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserStates",
                keyColumn: "id",
                keyValue: 1,
                column: "code",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserStates",
                keyColumn: "id",
                keyValue: 2,
                column: "code",
                value: null);
        }
    }
}
