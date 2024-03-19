using Carter;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Services;

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


        //Crear reserva
        app.MapPost("/{idCliente}/{idProducto}", ([FromServices] IReservaService reservaService, [FromBody] ReservaRequestDto reservaDto, [FromQuery] int idCliente, [FromQuery] string codigoProducto) =>
        {
            reservaService.CreateReserva(reservaDto, idCliente, codigoProducto);

            return Results.Created();

        }).WithTags("Reservas");


        //Cancelar reserva
        app.MapPut("/cancelar/{id}", ([FromServices] IReservaService reservaService, [FromQuery] int id) =>
        {
            reservaService.CancelarReserva(id);

            return Results.Ok();

        }).WithTags("Reservas");

        //Aprobar reserva
        app.MapPut("/aprobar/{id}", ([FromServices] IReservaService reservaService, [FromQuery] int id) =>
        {
            reservaService.AprobarReserva(id);

            return Results.Ok();

        }).WithTags("Reservas");

        //Rechazar reserva
        app.MapPut("/rechazar/{id}", ([FromServices] IReservaService reservaService, [FromQuery] int id) =>
        {
            reservaService.RechazarReserva(id);

            return Results.Ok();

        }).WithTags("Reservas");
    }
}
