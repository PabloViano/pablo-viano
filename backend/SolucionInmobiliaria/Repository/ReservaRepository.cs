using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Domain;
using Microsoft.EntityFrameworkCore;
using DataBase;
using Mapster;

namespace SolucionInmobiliaria.Repository;

public interface IReservaRepository
{
    List<Reserva> GetReservas();
    Reserva GetReserva(int id);
    void CreateReserva(ReservaDto reservaDto, Guid idCliente, string codigoProducto);
    string UpdateReserva(int id, ReservaDto reservaDto);
    void DeleteReserva(int id);
    void CancelarReserva(int id);
    void AprobarReserva(int id);
    void RechazarReserva(int id);
}

public class ReservaRepository(AppDbContext context) : IReservaRepository
{
    public void CreateReserva(ReservaDto reservaDto, Guid idCliente, string codigoProducto)
    {
        if (context.Usuarios.FirstOrDefault(p => p.Id == idCliente) == null)
        {
            throw new Exception($"El cliente con id: {idCliente} no existe");
        }
        if (context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigoProducto) == null)
        {
            throw new Exception($"El producto con codigo: {codigoProducto} no existe");
        }
        var reserva = new Reserva
        {
            Id = context.Reservas.Count() + 1,

            FechaDesde = reservaDto.FechaDesde,

            FechaHasta = reservaDto.FechaHasta,

            Estado = EstadosReserva.Ingresada,

            IdClienteAsociado = idCliente,


            ProductoReservado = codigoProducto,

        };

        var productoReservado = context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigoProducto);
        productoReservado.Estado = EstadosProducto.Reservado;

        //Verificar si el producto es de Barrio X y su precio es menor a 100000, si cumple se aprueba la reserva
        if (productoReservado.Barrio == "X" && productoReservado.Price < 100000)
        {
            productoReservado.Estado = EstadosProducto.Vendido;
        }

        //Verificar si el producto es de Barrio Y y es el ultimo en venta, si cumple se aprueba la reserva
        if (context.Productos.GroupBy(p => p.Barrio == productoReservado.Barrio).Count() == 1)
        {
            productoReservado.Estado = EstadosProducto.Vendido;
        }

        //Verificar si el vendedor no tiene mas de 3 reservas ingresadas
        if (context.Reservas.GroupBy(r => r.IdVendedorAsociado == reserva.IdVendedorAsociado && r.Estado == EstadosReserva.Ingresada).Count() > 3)
        {
            throw new Exception($"El vendedor con id: {reserva.IdVendedorAsociado} ya tiene 3 reservas ingresadas");
        }

        context.Reservas.Add(reserva);
        context.SaveChanges();
    }

    public void DeleteReserva(int id)
    {
        var Reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

        if (Reserva != null)
        {
            context.Reservas.Remove(Reserva);
            context.SaveChanges();
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe");
        }
    }

    public List<Reserva> GetReservas()
    {
        return context.Reservas.ToList();
    }

    public Reserva GetReserva(int id)
    {
        if (context.Reservas.FirstOrDefault(r => r.Id == id) != null)
        {
            return context.Reservas.FirstOrDefault(r => r.Id == id);
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe");
        }
    }


    //Verificar si este metodo funciona bien
    public string UpdateReserva(int id, ReservaDto reservaDto)
    {
        var Reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

        if (Reserva != null && context.Usuarios.FirstOrDefault(u => u.Id == reservaDto.IdClienteAsociado) != null && context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == reservaDto.ProductoReservado) != null)
        {
            Reserva.Id = reservaDto.Id;
            Reserva.FechaDesde = reservaDto.FechaDesde;
            Reserva.FechaHasta = reservaDto.FechaHasta;
            Reserva.Estado = reservaDto.Estado;
            Reserva.IdClienteAsociado = context.Usuarios.FirstOrDefault(u => u.Id == reservaDto.IdClienteAsociado).Id;
            Reserva.ProductoReservado = reservaDto.ProductoReservado;
            context.SaveChanges();

            return $"La Reserva N° {id}, fue modificada correctamente";
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe o el id del cliente es inexistente");
        }
    }

    //Cambiar el estado de la reserva a Cancelada
    public void CancelarReserva(int id)
    {
        var reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

        if (reserva != null && reserva.Estado != EstadosReserva.Cancelada)
        {
            reserva.Estado = EstadosReserva.Cancelada;
            context.Productos.FirstOrDefault(x => x.CodigoAlfanumero == reserva.ProductoReservado).Estado = EstadosProducto.Disponible;
            context.SaveChanges();
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe o ya esta cancelada");
        }
    }

    //Cambiar el estado de la reserva a Aprobada
    public void AprobarReserva(int id)
    {
        var reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

        if (reserva != null && reserva.Estado != EstadosReserva.Aprobada)
        {
            reserva.Estado = EstadosReserva.Aprobada;
            context.Productos.FirstOrDefault(x => x.CodigoAlfanumero == reserva.ProductoReservado).Estado = EstadosProducto.Vendido;
            context.SaveChanges();
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe o ya esta aprobada");
        }
    }

    public void RechazarReserva(int id)
    {
        var reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

        if (reserva != null)
        {
            reserva.Estado = EstadosReserva.Rechazada;
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe");
        }
    }
}
