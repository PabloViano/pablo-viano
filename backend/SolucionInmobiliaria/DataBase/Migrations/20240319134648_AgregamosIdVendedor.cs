using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolucionInmobiliaria.Database.Migrations
{
    /// <inheritdoc />
    public partial class AgregamosIdVendedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVendedor",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdVendedor",
                table: "Reservas");
        }
    }
}
