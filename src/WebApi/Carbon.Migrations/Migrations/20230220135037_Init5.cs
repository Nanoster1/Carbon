using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carbon.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_is_valid",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "password_is_valid",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
