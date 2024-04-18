using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolucionInmobiliaria.Domain
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        public string CodigoAlfanumero { get; set; }

        public string? Barrio { get; set; }

        public double Price { get; set; }

        public string? UrlImagen { get; set; }

        public EstadosProducto? Estado { get; set; }

        public string? Descripcion { get; set; }

        public int? IDReserva { get; set; }
    }

    public enum  EstadosProducto
    {
        Disponible,
        Reservado,
        Vendido
    }
}
