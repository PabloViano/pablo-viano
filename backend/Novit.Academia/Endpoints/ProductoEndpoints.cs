using Carter;

namespace Novit.Academia.Endpoints;

public class ProductoEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/api/Producto");

        app.MapGet("/", () =>
        {
            return Results.Ok();
        }).WithTags("Producto");

        app.MapGet("/{idProducto}", (int idProducto) =>
        {
            return Results.Ok();
        }).WithTags("Producto");

        app.MapPost("/", () =>
        {
            return Results.Created();
        }).WithTags("Producto");

        app.MapPut("/{idProducto}", (int idProducto) =>
        {
            return Results.Ok();
        }).WithTags("Producto");

        app.MapDelete("/{idProducto}", (int idProducto) =>
        {
            return Results.NoContent();
        }).WithTags("Producto");
    }
}
