using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVolunteer_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddApproves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ProjectDates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ProjectDates");
        }
    }
}
