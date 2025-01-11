using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEL.Migrations
{
    /// <inheritdoc />
    public partial class Primeiramigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudantes_CPF",
                table: "Estudantes",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudantes_Nome",
                table: "Estudantes",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudantes");
        }
    }
}
