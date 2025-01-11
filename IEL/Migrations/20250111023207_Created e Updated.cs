using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IEL.Migrations
{
    /// <inheritdoc />
    public partial class CreatedeUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Estudantes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Estudantes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Estudantes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Estudantes");
        }
    }
}
