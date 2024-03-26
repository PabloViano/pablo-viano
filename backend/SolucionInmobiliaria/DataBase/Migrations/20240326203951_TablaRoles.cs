using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SolucionInmobiliaria.Database.Migrations
{
    /// <inheritdoc />
    public partial class TablaRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Rol_RolesUsuarioId",
                table: "RolUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("186283e4-9ee9-4e62-8efc-28ed8e17f7fe"), "administrador" },
                    { new Guid("be0eb24e-3112-4f62-9532-05ee972421cf"), "vendedor" },
                    { new Guid("d1b47292-125f-4715-869b-1f49a13f0ca0"), "cliente" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Roles_RolesUsuarioId",
                table: "RolUsuario",
                column: "RolesUsuarioId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolUsuario_Roles_RolesUsuarioId",
                table: "RolUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

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

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolUsuario_Rol_RolesUsuarioId",
                table: "RolUsuario",
                column: "RolesUsuarioId",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
