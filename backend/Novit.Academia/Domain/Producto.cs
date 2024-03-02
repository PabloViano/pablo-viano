namespace Novit.Academia.Domain;

public class Producto
{
    public int IdProducto { get; set; }

    public required string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public required decimal Precio { get; set; }

    public string? UrlImagen { get; set; }

    public int Stock { get; set; } = 0;

    public List<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();
}
