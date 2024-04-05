using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolucionInmobiliaria.Domain
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        public required int Id { get; set; }

        public EstadosReserva Estado { get; set; }

        public Guid IdClienteAsociado { get; set; }

        public Guid IdVendedorAsociado { get; set; }

        public string ProductoReservado { get; set; }

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

    }

    public enum EstadosReserva
    {
        Ingresada,
        Cancelada,
        Aprobada,
        Rechazada
    }   
}
