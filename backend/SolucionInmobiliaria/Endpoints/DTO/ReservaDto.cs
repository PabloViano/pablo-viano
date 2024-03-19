using SolucionInmobiliaria.Domain;

namespace SolucionInmobiliaria.Endpoints.DTO;

    public class ReservaDto
    {
        public required int Id { get; set; }

        public EstadosReserva Estado { get; set; }

        public required UsuarioDto ClienteAsociado { get; set; }

        public required int IdVendedor { get; set; }

        public required ProductoDto ProductoReservado { get; set; }

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

    public required UsuarioDto ClienteAsociado { get; set; }

    public required ProductoDto ProductoReservado { get; set; }

    public DateTime FechaDesde { get; set; }

    public DateTime FechaHasta { get; set; }

}
