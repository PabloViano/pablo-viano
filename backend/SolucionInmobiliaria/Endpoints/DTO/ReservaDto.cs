using SolucionInmobiliaria.Domain;

namespace SolucionInmobiliaria.Endpoints.DTO;

    public class ReservaDto
    {
        public required int Id { get; set; }

        public EstadosReserva Estado { get; set; }

        public required Guid IdClienteAsociado { get; set; }

        public required Guid IdVendedor { get; set; }

        public required string ProductoReservado { get; set; }

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

    }

public class ReservaRequestDto
{


    public DateTime FechaDesde { get; set; }

    public DateTime FechaHasta { get; set; }

}

public class ReservaResponseDto
{
    public required int Id { get; set; }

    public EstadosReserva Estado { get; set; }

    public required Guid IdClienteAsociado { get; set; }

    public required string ProductoReservado { get; set; }

    public DateTime FechaDesde { get; set; }

    public DateTime FechaHasta { get; set; }

}
