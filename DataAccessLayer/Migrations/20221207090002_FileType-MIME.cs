using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class FileTypeMIME : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileTypes",
                newName: "MIMEType");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "FileTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "FileInformation",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "FileTypes");

            migrationBuilder.RenameColumn(
                name: "MIMEType",
                table: "FileTypes",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "FileInformation",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
