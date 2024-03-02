namespace Novit.Academia.Domain;

public class ItemCarrito
{
    public int IdItemCarrito { get; set; }
    
    public int Cantidad { get; set; } = 1;
    
    public required Producto Producto { get; set; } 
    
    public required Carrito Carrito { get; set; }

    public decimal Subtotal() => Producto.Precio * Cantidad;
}
