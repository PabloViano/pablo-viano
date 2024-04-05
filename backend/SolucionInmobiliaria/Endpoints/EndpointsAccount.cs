using Carter;
using DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Novit.Academia.Endpoints;

public class AccountEndpoints(IConfiguration configuration) : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder routes)
    {
        var app = routes.MapGroup("/api/Account");

        app.MapPost("/Register", (AppDbContext context, UsuarioRegisterDto request) =>
        {

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new Usuario()
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Nombre = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RolesUsuario = new List<Rol>() { context.Roles.FirstOrDefault(x => x.Nombre == request.Role)}
            };


            if (context.Roles.FirstOrDefault(x => x.Nombre == request.Role) is null)
            {
                return Results.BadRequest("Rol inexistente.");
            }

            //context.Roles.FirstOrDefault(x => x.Nombre == request.Role.ToString()).Usuarios.Add(user);

            context.Usuarios.Add(user);

            context.SaveChanges();

            var userCreated = context.Usuarios.FirstOrDefault(x => x.Id == user.Id);

            return Results.Ok(new { Id = userCreated.Id, Username = userCreated.Username, Role = userCreated.RolesUsuario.First().Nombre });
        })
            .WithTags("Account")
            .AllowAnonymous();


        app.MapPost("/Login", (AppDbContext context, UsuarioDto request) =>
        {

            var user = context.Usuarios.Where(x => x.Username == request.Username).Include(x => x.RolesUsuario).First();

            if (user is null)
            {
                return Results.BadRequest("Usuario inexistente.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Results.BadRequest("Contraseña incorrecta.");
            }

            var token = new { accessToken = CreateToken(user) };

            return Results.Ok(token);
        })
            .WithTags("Account")
            .AllowAnonymous();

        app.MapPost("/Role", (AppDbContext context, RolDto request) =>
        {
            var role = new Rol()
            {
                Id = Guid.NewGuid(),
                Nombre = request.Nombre
            };

            context.Roles.Add(role);

            context.SaveChanges();

            return Results.Ok(role);
        })
            .WithTags("Account")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });

        app.MapPost("/User/{userId:Guid}/AddRole/{roleId:Guid}", (AppDbContext context, Guid userId, Guid roleId) =>
        {

            var user = context.Usuarios.FirstOrDefault(x => x.Id == userId);

            if (user is null)
            {
                return Results.BadRequest("Usuario inexistente.");
            }

            var role = context.Roles.FirstOrDefault(x => x.Id == roleId);

            if (role is null)
            {
                return Results.BadRequest("Rol inexistente.");
            }

            user.RolesUsuario.Add(role);

            context.SaveChanges();

            return Results.Ok("Rol asociado");
        })
            .WithTags("Account")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });


        app.MapGet("/User", (AppDbContext context) =>
        {
            var users = context.Usuarios.Include(x => x.RolesUsuario).ToList().Select(x =>
                new { x.Id, x.Username, x.PasswordHash, roles = x.RolesUsuario.Select(y => new { y.Id, y.Nombre }) });
            return Results.Ok(users);
        })
            .WithTags("Account")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });

        app.MapGet("/Role", (AppDbContext context) =>
        {
            var roles = context.Roles.Include(x => x.Usuarios).ToList().Select(x =>
                new { x.Id, x.Nombre, users = x.Usuarios.Select(y => new { y.Id, y.Username }) });
            return Results.Ok(roles);
        })
            .WithTags("Account")
            .RequireAuthorization(new AuthorizeAttribute { Roles = "administrador" });
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(Usuario user)
    {
        var claims = new List<Claim>
        {
            new Claim("name", user.Username),
        };

        string roles = string.Join(",", user.RolesUsuario.Select(x => x.Nombre));

        claims.Add(new Claim("role", roles));

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}