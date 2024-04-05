using DataBase;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;

namespace SolucionInmobiliaria.Repository;

public interface IProductoRepository
{
    List<Producto> GetProductos();
    string UpdateProducto(string codigo, ProductoDto productoDto);
    void DeleteProducto(string codigo);
    Producto GetProducto(string codigo);
    void AddProducto(ProductoDto producto);
}

public class ProductoRepository(AppDbContext context) : IProductoRepository
{
    public void AddProducto(ProductoDto producto)
    {
        Producto producto1 = new Producto
        {
            CodigoAlfanumero = $"A{ context.Productos.Count() + 1}",
            Barrio = producto.Barrio,
            Price = producto.Price,
            UrlImagen = producto.UrlImagen,
            Estado = producto.Estado,
            Descripcion = producto.Descripcion
        };

        context.Productos.Add(producto1);

        context.SaveChanges();
    }



    public void DeleteProducto(string codigoAlfanumerico)
    {
        //Buscar el producto por el codigo alfanumerico
        var producto = context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigoAlfanumerico);

        if (producto != null)
        {
            foreach (var reserva in context.Reservas)
            {
                if (reserva.ProductoReservado == codigoAlfanumerico)
                {
                    throw new Exception($"El producto con id: {codigoAlfanumerico} tiene reservas asociadas");
                }
            }
            //Eliminar el producto de la base de datos
            context.Productos.Remove(producto);

            //Guardar cambios en la base de datos
            context.SaveChanges();
        }
        else
        {
            throw new Exception($"El producto con id: {codigoAlfanumerico} no existe");
        }
    }

    public Producto GetProducto(string codigo)
    {
        var producto = context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigo);

        if (producto != null)
        {
            return producto;
        }
        else
        {
            throw new Exception($"El producto con id: {codigo} no existe");
        }
    }

    public List<Producto> GetProductos()
    {
        return context.Productos.ToList();
    }

    public string UpdateProducto(string codigo, ProductoDto productoDto)
    {
        var producto = context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigo);

        if (producto != null)
        {
            producto.Barrio = productoDto.Barrio;
            producto.Price = productoDto.Price;
            producto.UrlImagen = productoDto.UrlImagen;
            producto.Estado = productoDto.Estado;
            producto.Descripcion = productoDto.Descripcion;

            context.SaveChanges();

            return producto.CodigoAlfanumero;
        }
        else
        {
            throw new Exception($"El producto con id: {codigo} no existe");
        }
    }
}

