using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCE.Teste.Livraria.Infra.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Isbn = table.Column<string>(type: "varchar(150)", nullable: false),
                    Autor = table.Column<string>(type: "varchar(150)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataPublicacao = table.Column<DateTime>(nullable: false),
                    ImgCapa = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
