using SolucionInmobiliaria.Domain;

namespace SolucionInmobiliaria.Endpoints.DTO;

public class ProductoDto
{
    public required string  CodigoAlfanumero { get; set; }

    public string? Barrio { get; set; }

    public required double Price { get; set; }

    public string? UrlImagen { get; set; }

    public string? Estado { get; set; }

    public string? Descripcion { get; set; }

    public int? IDReserva { get; set; }
}

public class ProductoRequestDto
{

    public string? Barrio { get; set; }

    public required double Price { get; set; }

    public string? UrlImagen { get; set; }

    public string? Estado { get; set; }

    public string? Descripcion { get; set; }

    public int? IDReserva { get; set; }
}

public class ProductoResponseDto
{
    public required string CodigoAlfanumero { get; set; }

    public string? Barrio { get; set; }

    public required double Price { get; set; }

    public string? UrlImagen { get; set; }

    public string? Estado { get; set; }

    public string? Descripcion { get; set; }

    public int? IDReserva { get; set; }
}

public class ProductoModificadoDto
{
    public string Barrio { get; set; }

    public string Descripcion { get; set; }

    public double Price { get; set; }

    public string UrlImagen { get; set; }
}
