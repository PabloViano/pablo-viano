using Carter;
using Microsoft.EntityFrameworkCore;
using DataBase;
using Mapster;
using SolucionInmobiliaria.Repository;
using SolucionInmobiliaria.Services;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

builder.Services.AddCarter();

builder.Services.AddCors(options =>
    options.AddPolicy("Academia2024", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetConnectionString("AppDb")));

builder.Services.AddTransient<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddTransient<IReservaRepository, ReservaRepository>();

builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(options => options.EnableTryItOutByDefault());

app.UseCors("Academia2024");

app.MapCarter();

app.Run();