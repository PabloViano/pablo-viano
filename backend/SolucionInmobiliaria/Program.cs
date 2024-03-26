using Carter;
using DataBase;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolucionInmobiliaria.Repository;
using SolucionInmobiliaria.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var config = builder.Configuration;

builder.Services.AddCarter();

builder.Services.AddCors(options =>
    options.AddPolicy("Academia2024", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetConnectionString("AppDb")));

builder.Services.AddTransient<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddTransient<IReservaService, ReservaService>();

builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(options => options.EnableTryItOutByDefault());

app.UseCors("Academia2024");

app.MapCarter();

app.Run();