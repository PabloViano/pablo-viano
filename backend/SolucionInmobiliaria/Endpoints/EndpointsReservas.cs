using Carter;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Services;
using Microsoft.AspNetCore.Authorization;

namespace SolucionInmobiliaria.Endpoints;

public class EndpointsReservas : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/api/Reservas");

        //Lista de reservas
        app.MapGet("/", (IReservaService reservaService) =>
        {
            var reservas = reservaService.GetReservas();

            return Results.Ok(reservas);

        }).WithTags("Reservas");

        app.MapGet("/{idReserva}", (IReservaService reservaService, string idReserva) =>
        {
            var reserva = reservaService.GetReserva(int.Parse(idReserva));

            return Results.Ok(reserva);
        }).WithTags("Reservas");


        //Crear reserva
        app.MapPost("/{idCliente}/{idProducto}", ([FromServices] IReservaService reservaService, [FromBody] ReservaRequestDto reservaDto, [FromQuery] Guid idCliente, [FromQuery] string codigoProducto) =>
        {
            reservaService.CreateReserva(reservaDto, idCliente, codigoProducto);

            return Results.Created();

        }).WithTags("Reservas")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });


        //Cancelar reserva
        app.MapPut("/cancelar/{id}", ([FromServices] IReservaService reservaService, [FromQuery] int id) =>
        {
            reservaService.CancelarReserva(id);

            return Results.Ok();

        }).WithTags("Reservas")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });

        //Aprobar reserva
        app.MapPut("/aprobar/{id}", ([FromServices] IReservaService reservaService, [FromQuery] int id) =>
        {
            reservaService.AprobarReserva(id);

            return Results.Ok();

        }).WithTags("Reservas")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });

        //Rechazar reserva
        app.MapPut("/rechazar/{id}", ([FromServices] IReservaService reservaService, [FromQuery] int id) =>
        {
            reservaService.RechazarReserva(id);

            return Results.Ok();

        }).WithTags("Reservas")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });
    }
}
