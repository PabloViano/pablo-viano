using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;

builder.Services.AddCarter();

builder.Services.AddCors(options =>
    options.AddPolicy("Academia2024", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(options => options.EnableTryItOutByDefault());

app.UseCors("Academia2024");

app.MapCarter();

app.Run();
