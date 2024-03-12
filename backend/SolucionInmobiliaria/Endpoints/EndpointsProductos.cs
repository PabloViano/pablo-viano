using Carter;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Services;

namespace SolucionInmobiliaria.Endpoints;

public class EndpointsProductos : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/api/Productos");

        //Mostrar la lista completa de productos
        app.MapGet("/", (IProductoService productoService ) =>
        {
            var productos = productoService.GetProductos();

            return Results.Ok(productos);

        }).WithTags("Productos");

        //Crear un producto, su codigo y precio son requeridos, los demas atributos son opcionales
        app.MapPost("/", ([FromServices] IProductoService productoService, [FromBody] ProductoRequestDto productoDto) =>
        {
            productoService.CreateProducto(productoDto);

            return Results.Created();

        }).WithTags("Productos");

        //Eliminar un producto con su codigo AlfaNumerico
        app.MapDelete("/{codigoAlfaNumerico}", (IProductoService productoService, string codigoAlfaNumerico) =>
        {
            productoService.DeleteProducto(codigoAlfaNumerico);

            return Results.NoContent();

        }).WithTags("Productos");

        //Modificar un producto con su codigo AlfaNumerico
        app.MapPut("/{codigoAlfanumerico}/Producto", ([FromServices] IProductoService productoService, string codigoProducto, [FromBody] ProductoRequestDto productoDto) =>
        {
            productoService.UpdateProducto(codigoProducto, productoDto);

            return Results.Ok();

        }).WithTags("Productos");


    }
}
