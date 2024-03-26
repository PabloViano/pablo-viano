using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolucionInmobiliaria.Database.Migrations
{
    /// <inheritdoc />
    public partial class AgregamosTablaRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_ClienteAsociadoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_ClienteAsociadoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ClienteAsociadoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "IdVendedor",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "Username");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdClienteAsociado",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdVendedorAsociado",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolUsuario",
                columns: table => new
                {
                    RolesUsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuariosId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuario", x => new { x.RolesUsuarioId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_RolUsuario_Rol_RolesUsuarioId",
                        column: x => x.RolesUsuarioId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUsuario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuario_UsuariosId",
                table: "RolUsuario",
                column: "UsuariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropColumn(
                name: "IdClienteAsociado",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "IdVendedorAsociado",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rol",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClienteAsociadoId",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdVendedor",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteAsociadoId",
                table: "Reservas",
                column: "ClienteAsociadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_ClienteAsociadoId",
                table: "Reservas",
                column: "ClienteAsociadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
