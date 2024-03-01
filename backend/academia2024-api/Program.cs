using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregar configuracion de CORS
builder.Services.AddCors(options => options.AddPolicy("Academia2024", 
    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option => option.EnableTryItOutByDefault());
}

app.UseHttpsRedirection();

//Se hace uso de CORS definido previamente
app.UseCors("Academia2024");

app.MapCarter();

app.Run();
