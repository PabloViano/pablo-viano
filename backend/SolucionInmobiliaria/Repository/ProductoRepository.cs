using DataBase;
using SolucionInmobiliaria.Domain;
using SolucionInmobiliaria.Endpoints.DTO;

namespace SolucionInmobiliaria.Repository;

public interface IProductoRepository
{
    List<Producto> GetProductos();
    string UpdateProducto(string codigo, ProductoModificadoDto productoDto);
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
            CodigoAlfanumero = $"A{ context.Productos.Count() + 2}",
            Barrio = producto.Barrio,
            Price = producto.Price,
            UrlImagen = producto.UrlImagen,
            Descripcion = producto.Descripcion
        };

        if (producto.Estado == "Disponible")
        {
            producto1.Estado = EstadosProducto.Disponible;
        }
        else if (producto.Estado == "Reservado")
        {
            producto1.Estado = EstadosProducto.Reservado;
        }
        else if (producto.Estado == "Vendido")
        {
            producto1.Estado = EstadosProducto.Vendido;
        }
        else
        {
            throw new Exception("El estado del producto no es valido");
        }

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

    public string UpdateProducto(string codigoAlfanumero, ProductoModificadoDto productoModificado)
    {
        var producto = context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigoAlfanumero);

        if (producto != null)
        {
            if (productoModificado.Barrio != null && productoModificado.Barrio != producto.Barrio)
            {
                producto.Barrio = productoModificado.Barrio;
            }
            if (productoModificado.Price != 0 && producto.Price != productoModificado.Price)
            {
                producto.Price = productoModificado.Price;
            }
            if (productoModificado.UrlImagen != null && producto.UrlImagen != productoModificado.UrlImagen)
            {
                producto.UrlImagen = productoModificado.UrlImagen;
            }
            if (productoModificado.Descripcion != null && producto.Descripcion != productoModificado.Descripcion)
            {
                producto.Descripcion = productoModificado.Descripcion;
            }

            context.SaveChanges();

            return producto.CodigoAlfanumero;
        }
        else
        {
            throw new Exception($"El producto con id: {codigoAlfanumero} no existe");
        }
    }
}

