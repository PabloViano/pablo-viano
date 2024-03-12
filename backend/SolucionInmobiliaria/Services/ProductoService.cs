using SolucionInmobiliaria.Endpoints.DTO;
using DataBase;
using SolucionInmobiliaria.Repository;
using Mapster;

namespace SolucionInmobiliaria.Services;

public interface IProductoService
{
    List<ProductoResponseDto> GetProductos();

    ProductoResponseDto GetProducto(string codigoAlfanumero);

    void CreateProducto(ProductoRequestDto producto);

    string UpdateProducto(string codigoAlfanumero, ProductoRequestDto producto);

    void DeleteProducto(string codigoAlfanumero);
}

public class ProductoService(IProductoRepository productoRepository) : IProductoService
{
    public void CreateProducto(ProductoRequestDto producto)
    {
        productoRepository.AddProducto(producto.Adapt<ProductoDto>());
    }

    public void DeleteProducto(string codigoAlfanumero)
    {
        productoRepository.DeleteProducto(codigoAlfanumero);
    }

    public ProductoResponseDto GetProducto(string codigoAlfanumero)
    {
        var producto = productoRepository.GetProducto(codigoAlfanumero);

        return producto.Adapt<ProductoResponseDto>();
    }

    public List<ProductoResponseDto> GetProductos()
    {
        return productoRepository.GetProductos().Adapt<List<ProductoResponseDto>>();
    }

    public string UpdateProducto(string codigoAlfanumero, ProductoRequestDto producto)
    {
        productoRepository.UpdateProducto(codigoAlfanumero, producto.Adapt<ProductoDto>());

        return $"Producto con codigo {codigoAlfanumero} actualizado";
    }
}

