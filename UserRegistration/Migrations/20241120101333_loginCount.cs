using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRegistration.Migrations
{
    /// <inheritdoc />
    public partial class loginCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loginCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loginCount",
                table: "AspNetUsers");
        }
    }
}
