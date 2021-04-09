using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginRegister.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Zip",
                table: "RegisterModels",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgreeTerms",
                table: "RegisterModels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgreeTerms",
                table: "RegisterModels");

            migrationBuilder.AlterColumn<string>(
                name: "Zip",
                table: "RegisterModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
