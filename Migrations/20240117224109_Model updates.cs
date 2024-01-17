using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mesi.Migrations
{
    /// <inheritdoc />
    public partial class Modelupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Departament",
                table: "Doctors",
                newName: "Department");

            migrationBuilder.AddColumn<int>(
                name: "WorkEnd",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkStart",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkEnd",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WorkStart",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Doctors",
                newName: "Departament");
        }
    }
}
