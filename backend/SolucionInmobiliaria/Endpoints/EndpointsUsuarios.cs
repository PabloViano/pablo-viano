using Carter;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Services;

namespace SolucionInmobiliaria.Endpoints
{
    public class EndpointsUsuarios : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder routes)
        {
            var app = routes.MapGroup("/api/Usuarios");

            //Mostrar la lista completa de usuarios
            app.MapGet("/", (AppDbContext context) =>
            {
                return context.Usuarios.ToListAsync();
            }).WithTags("Usuarios");

        }
    }
}
