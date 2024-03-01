using Carter;

namespace academia2024_api.Endpoints;

public class NombreEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        List<string> nombres = ["Juan", "Pedro"];

        app.MapGet("/nombres", () =>
        {
            return Results.Ok(nombres);
        }).WithTags("Nombres");

        app.MapPost("/nombres", (string nombre) =>
        {
            nombres.Add(nombre);
            return Results.Ok(nombres);
        }).WithTags("Nombres");

        app.MapDelete("/nombres", (string nombre) =>
        {
            nombres.Remove(nombre);
            return Results.Ok(nombres);
        }).WithTags("Nombres");
    }
}
