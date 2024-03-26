namespace SolucionInmobiliaria.Endpoints.DTO;

public class RolDto
{
    public string Nombre { get; set; }
}

public class RolUsuarioDto
{
    public Guid IdRol { get; set; }
    public Guid IdUser { get; set; }
}
