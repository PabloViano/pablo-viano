using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Domain;
using Microsoft.EntityFrameworkCore;
using DataBase;

namespace SolucionInmobiliaria.Repository;

public interface IReservaRepository
{
    List<Reserva> GetReservas();
    Reserva GetReserva(int id);
    void AddReserva(ReservaDto reservaDto);
    string UpdateReserva(int id, ReservaDto reservaDto);
    void DeleteReserva(int id);
}

public class ReservaRepository(AppDbContext context) : IReservaRepository
{
    public void AddReserva(ReservaDto reservaDto)
    {
        //Ver si la reserva ya existe
        if (context.Reservas.FirstOrDefault(r => r.Id == reservaDto.Id) != null)
        {
            throw new Exception($"La reserva con id: {reservaDto.Id} ya existe");
        }

        //Si no existe, crear una nueva reserva y asignarle los valores del DTO
        var reserva = new Reserva
        {
            Id = reservaDto.Id,

            FechaDesde = reservaDto.FechaDesde,

            FechaHasta = reservaDto.FechaHasta,

            Estado = reservaDto.Estado,

            ClienteAsociado = new Usuario
            {
                Id = reservaDto.ClienteAsociado.Id,
                Nombre = reservaDto.ClienteAsociado.Nombre,
                Apellido = reservaDto.ClienteAsociado.Apellido,
                Email = reservaDto.ClienteAsociado.Email,
                Password = reservaDto.ClienteAsociado.Password,
                ReservasUsuario = reservaDto.ClienteAsociado.ReservasUsuario,
                Rol = Roles.Comercial
            },

            ProductoReservado = new Producto
            {
                CodigoAlfanumero = reservaDto.ProductoReservado.CodigoAlfanumero,
                Barrio = reservaDto.ProductoReservado.Barrio,
                Price = reservaDto.ProductoReservado.Price,
                UrlImagen = reservaDto.ProductoReservado.UrlImagen,
                Estado = reservaDto.ProductoReservado.Estado,
                Descripcion = reservaDto.ProductoReservado.Descripcion
            }
        };
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
                Password = reservaDto.ClienteAsociado.Password,
                ReservasUsuario = reservaDto.ClienteAsociado.ReservasUsuario,
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
}
