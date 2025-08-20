using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTier.ManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Employees");
        }
    }
}
