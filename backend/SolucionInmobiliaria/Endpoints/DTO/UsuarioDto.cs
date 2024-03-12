using SolucionInmobiliaria.Domain;

namespace SolucionInmobiliaria.Endpoints.DTO;
    public class UsuarioDto
    {
        public required int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Roles Rol { get; set; }
        public List<Reserva>? ReservasUsuario { get; set; }

    }

public class UsuarioRequestDto
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public Roles Rol { get; set; }
    //Lista con los codigos de las Reservas que tiene el usuario
    public List<string>? ReservasUsuario { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

}

public class UsuarioResponseDto
{
    public required int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public Roles Rol { get; set; }
    public List<Reserva>? ReservasUsuario { get; set; }

}
