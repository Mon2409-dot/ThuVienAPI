using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations.BookAuthDb
{
    /// <inheritdoc />
    public partial class AddGiamDocRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f3a1c20-1234-4a5b-9abc-def012345678", "8f3a1c20-1234-4a5b-9abc-def012345678", "GIÁM ĐỐC", "GIÁM ĐỐC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f3a1c20-1234-4a5b-9abc-def012345678");
        }
    }
}
