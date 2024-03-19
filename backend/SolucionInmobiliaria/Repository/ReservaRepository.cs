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
    void CreateReserva(ReservaDto reservaDto, int idCliente, string codigoProducto);
    string UpdateReserva(int id, ReservaDto reservaDto);
    void DeleteReserva(int id);
    void CancelarReserva(int id);
    void AprobarReserva(int id);
    void RechazarReserva(int id);
}

public class ReservaRepository(AppDbContext context) : IReservaRepository
{
    public void CreateReserva(ReservaDto reservaDto, int idCliente, string codigoProducto)
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

            ClienteAsociado = context.Usuarios.FirstOrDefault(p => p.Id == idCliente),


            ProductoReservado = context.Productos.FirstOrDefault(p => p.CodigoAlfanumero == codigoProducto),

        };

        reserva.ProductoReservado.Estado = EstadosProducto.Reservado;

        //Verificar si el producto es de Barrio X y su precio es menor a 100000, si cumple se aprueba la reserva
        if (reserva.ProductoReservado.Barrio == "X" && reserva.ProductoReservado.Price < 100000)
        {
            reserva.ProductoReservado.Estado = EstadosProducto.Vendido;
        }

        //Verificar si el producto es de Barrio Y y es el ultimo en venta, si cumple se aprueba la reserva
        if (context.Productos.GroupBy(p => p.Barrio == reserva.ProductoReservado.Barrio).Count() == 1)
        {
            reserva.ProductoReservado.Estado = EstadosProducto.Vendido;
        }

        //Verificar si el vendedor no tiene mas de 3 reservas ingresadas
        if (context.Reservas.GroupBy(r => r.IdVendedor == reserva.IdVendedor && r.Estado == EstadosReserva.Ingresada).Count() > 3)
        {
            throw new Exception($"El vendedor con id: {reserva.IdVendedor} ya tiene 3 reservas ingresadas");
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

        if (Reserva != null)
        {
            Reserva.Id = reservaDto.Id;
            Reserva.FechaDesde = reservaDto.FechaDesde;
            Reserva.FechaHasta = reservaDto.FechaHasta;
            Reserva.Estado = reservaDto.Estado;
            Reserva.ClienteAsociado = new Usuario
            {
                Id = reservaDto.ClienteAsociado.Id,
                Nombre = reservaDto.ClienteAsociado.Nombre,
                Apellido = reservaDto.ClienteAsociado.Apellido,
                Email = reservaDto.ClienteAsociado.Email,
                PasswordHash = reservaDto.ClienteAsociado.PasswordHash,
                PasswordSalt = reservaDto.ClienteAsociado.PasswordSalt,
                Rol = Roles.Comercial
            };
            Reserva.ProductoReservado = new Producto
            {
                CodigoAlfanumero = reservaDto.ProductoReservado.CodigoAlfanumero,
                Barrio = reservaDto.ProductoReservado.Barrio,
                Price = reservaDto.ProductoReservado.Price,
                UrlImagen = reservaDto.ProductoReservado.UrlImagen,
                Estado = reservaDto.ProductoReservado.Estado,
                Descripcion = reservaDto.ProductoReservado.Descripcion
            };

            context.SaveChanges();

            return $"La Reserva N° {id}, fue modificada correctamente";
        }
        else
        {
            throw new Exception($"La reserva con id: {id} no existe");
        }
    }

    //Cambiar el estado de la reserva a Cancelada
    public void CancelarReserva(int id)
    {
        var reserva = context.Reservas.FirstOrDefault(r => r.Id == id);

        if (reserva != null && reserva.Estado != EstadosReserva.Cancelada)
        {
            reserva.Estado = EstadosReserva.Cancelada;
            reserva.ProductoReservado.Estado = EstadosProducto.Disponible;
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
            reserva.ProductoReservado.Estado = EstadosProducto.Vendido;
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
