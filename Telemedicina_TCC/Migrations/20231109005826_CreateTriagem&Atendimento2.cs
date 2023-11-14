using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telemedicina_TCC.Migrations
{
    public partial class CreateTriagemAtendimento2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Triagens",
                table: "Triagens",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atendimentos",
                table: "Atendimentos",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Triagens",
                table: "Triagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atendimentos",
                table: "Atendimentos");
        }
    }
}
