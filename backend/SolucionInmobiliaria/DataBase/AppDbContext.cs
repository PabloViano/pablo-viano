using Microsoft.EntityFrameworkCore;
using SolucionInmobiliaria.Domain;

namespace DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData(
                new Producto
                {
                    CodigoAlfanumero = "A1",
                    Barrio = "Palermo",
                    Price = 100000,
                    UrlImagen = "https://www.google.com",
                    Estado = EstadosProducto.Disponible,
                    Descripcion = "Departamento de 2 ambientes"
                },
                new Producto
                {
                    CodigoAlfanumero = "A2",
                    Barrio = "Recoleta",
                    Price = 150000,
                    UrlImagen = "https://www.google.com",
                    Estado = EstadosProducto.Disponible,
                    Descripcion = "Departamento de 3 ambientes"
                }
                );
        }
    }
}

