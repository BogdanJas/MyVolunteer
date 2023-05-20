using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVolunteer_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewVolunteerVariableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Volunteers");
        }
    }
}
