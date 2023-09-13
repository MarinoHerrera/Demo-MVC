using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoABC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(6)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PrecioDetal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrecioMayor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Estiba = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ModDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
