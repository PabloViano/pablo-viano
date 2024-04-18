using Carter;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Services;
using Microsoft.AspNetCore.Authorization;

namespace SolucionInmobiliaria.Endpoints;

public class EndpointsProductos : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/api/Productos");

        //Mostrar la lista completa de productos
        app.MapGet("/", (IProductoService productoService) =>
        {
            var productos = productoService.GetProductos();

            return Results.Ok(productos);

        }).WithTags("Productos");

        app.MapGet("/{codigoAlfanumerico}", (IProductoService productoService, string codigoAlfanumerico) =>
        {
            var producto = productoService.GetProducto(codigoAlfanumerico);

            return producto != null ? Results.Ok(producto) : Results.NotFound();

        }).WithTags("Productos");

        //Crear un producto, su codigo y precio son requeridos, los demas atributos son opcionales
        app.MapPost("/", ([FromServices] IProductoService productoService, [FromBody] ProductoRequestDto productoDto) =>
        {
            productoService.CreateProducto(productoDto);

            return Results.Created();

        }).WithTags("Productos")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });

        //Eliminar un producto con su codigo AlfaNumerico
        app.MapDelete("/{codigoAlfaNumerico}", (IProductoService productoService, string codigoAlfaNumerico) =>
        {
            productoService.DeleteProducto(codigoAlfaNumerico);

            return Results.NoContent();

        }).WithTags("Productos")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });

        //Modificar un producto con su codigo AlfaNumerico
        app.MapPut("/{codigoAlfanumero}", ([FromServices] IProductoService productoService, string codigoAlfanumero, [FromBody] ProductoModificadoDto productoDto) =>
        {
            productoService.UpdateProducto(codigoAlfanumero, productoDto);

            return Results.Ok();

        }).WithTags("Productos")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });


    }
}
