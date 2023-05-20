using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVolunteer_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLimitsForProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteersLimit",
                table: "ProjectDates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteersLimit",
                table: "ProjectDates");
        }
    }
}
