using SolucionInmobiliaria.Domain;

namespace SolucionInmobiliaria.Endpoints.DTO;
    public class UsuarioDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

public class  UsuarioRegisterDto : UsuarioDto 
{
    public string Role { get; set; } = string.Empty;
}
