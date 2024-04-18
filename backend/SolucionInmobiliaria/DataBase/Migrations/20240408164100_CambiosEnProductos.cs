using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolucionInmobiliaria.Database.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEnProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDReserva",
                table: "Productos",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDReserva",
                table: "Productos");
        }
    }
}
