using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Employee_Name",
                schema: "employee",
                table: "Employee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "employee",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee_Name",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "employee",
                table: "Employee");
        }
    }
}
