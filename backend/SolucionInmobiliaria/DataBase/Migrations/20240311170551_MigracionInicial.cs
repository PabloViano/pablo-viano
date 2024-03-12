using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SolucionInmobiliaria.Database.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    CodigoAlfanumero = table.Column<string>(type: "TEXT", nullable: false),
                    Barrio = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    UrlImagen = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.CodigoAlfanumero);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rol = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    ClienteAsociadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoReservadoCodigoAlfanumero = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Productos_ProductoReservadoCodigoAlfanumero",
                        column: x => x.ProductoReservadoCodigoAlfanumero,
                        principalTable: "Productos",
                        principalColumn: "CodigoAlfanumero");
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_ClienteAsociadoId",
                        column: x => x.ClienteAsociadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "CodigoAlfanumero", "Barrio", "Descripcion", "Estado", "Price", "UrlImagen" },
                values: new object[,]
                {
                    { "A1", "Palermo", "Departamento de 2 ambientes", 0, 100000.0, "https://www.google.com" },
                    { "A2", "Recoleta", "Departamento de 3 ambientes", 0, 150000.0, "https://www.google.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteAsociadoId",
                table: "Reservas",
                column: "ClienteAsociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ProductoReservadoCodigoAlfanumero",
                table: "Reservas",
                column: "ProductoReservadoCodigoAlfanumero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
