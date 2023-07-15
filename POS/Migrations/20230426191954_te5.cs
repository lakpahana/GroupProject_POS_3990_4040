using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_POS.Migrations
{
    /// <inheritdoc />
    public partial class te5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TM");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TM");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TM",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "kke",
                table: "TM",
                newName: "ds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "TM",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ds",
                table: "TM",
                newName: "kke");

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "TM",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TM",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
