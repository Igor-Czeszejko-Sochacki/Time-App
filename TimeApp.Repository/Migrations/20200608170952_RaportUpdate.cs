using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeApp.Repository.Migrations
{
    public partial class RaportUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Raports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Raports");
        }
    }
}
