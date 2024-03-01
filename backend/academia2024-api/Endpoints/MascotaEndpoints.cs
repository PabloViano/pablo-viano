using Carter;

namespace academia2024_api.Endpoints;

public class MascotaEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        List<string> nombres = ["Juan", "Pedro"];

        app.MapGet("/mascotas", () => {
            return Results.Ok(nombres);
        }).WithTags("Mascotas");
    }
}
