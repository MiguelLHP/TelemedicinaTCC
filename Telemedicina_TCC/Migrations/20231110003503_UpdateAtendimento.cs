using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telemedicina_TCC.Migrations
{
    public partial class UpdateAtendimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionID",
                table: "Atendimentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StatusAtendimento",
                table: "Atendimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionID",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "StatusAtendimento",
                table: "Atendimentos");
        }
    }
}
