namespace Novit.Academia.Domain;

public class Carrito
{
    public int IdCarrito { get; set; }

    public List<ItemCarrito> Items { get; set; } = [];

    public bool Estado { get; set; } = true;

    public decimal Total() => Items.Sum(item => item.Subtotal());
}
