using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SolucionInmobiliaria.Database.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEnReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Productos_ProductoReservadoCodigoAlfanumero",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_ProductoReservadoCodigoAlfanumero",
                table: "Reservas");

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "CodigoAlfanumero",
                keyValue: "A1");

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "CodigoAlfanumero",
                keyValue: "A2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("186283e4-9ee9-4e62-8efc-28ed8e17f7fe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("be0eb24e-3112-4f62-9532-05ee972421cf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d1b47292-125f-4715-869b-1f49a13f0ca0"));

            migrationBuilder.DropColumn(
                name: "ProductoReservadoCodigoAlfanumero",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "ProductoReservado",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoReservado",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "ProductoReservadoCodigoAlfanumero",
                table: "Reservas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "CodigoAlfanumero", "Barrio", "Descripcion", "Estado", "Price", "UrlImagen" },
                values: new object[,]
                {
                    { "A1", "Palermo", "Departamento de 2 ambientes", 0, 100000.0, "https://www.google.com" },
                    { "A2", "Recoleta", "Departamento de 3 ambientes", 0, 150000.0, "https://www.google.com" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("186283e4-9ee9-4e62-8efc-28ed8e17f7fe"), "administrador" },
                    { new Guid("be0eb24e-3112-4f62-9532-05ee972421cf"), "vendedor" },
                    { new Guid("d1b47292-125f-4715-869b-1f49a13f0ca0"), "cliente" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ProductoReservadoCodigoAlfanumero",
                table: "Reservas",
                column: "ProductoReservadoCodigoAlfanumero");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Productos_ProductoReservadoCodigoAlfanumero",
                table: "Reservas",
                column: "ProductoReservadoCodigoAlfanumero",
                principalTable: "Productos",
                principalColumn: "CodigoAlfanumero");
        }
    }
}
