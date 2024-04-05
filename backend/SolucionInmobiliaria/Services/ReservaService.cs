using SolucionInmobiliaria.Endpoints.DTO;
using DataBase;
using SolucionInmobiliaria.Repository;
using Mapster;

namespace SolucionInmobiliaria.Services;

public interface IReservaService
{
    List<ReservaResponseDto> GetReservas();

    ReservaResponseDto GetReserva(int id);

    void CreateReserva(ReservaRequestDto reservaRequestDto, Guid idCliente, string producto);

    string UpdateReserva(int id, ReservaRequestDto reserva);

    void DeleteReserva(int id);

    void CancelarReserva(int idReserva);

    void AprobarReserva(int idReserva);

    void RechazarReserva(int idReserva);

}

public class ReservaService(IReservaRepository reservaRepository) : IReservaService
{
    public void CreateReserva(ReservaRequestDto reservaRequest, Guid idCliente, string producto)
    {
        reservaRepository.CreateReserva(reservaRequest.Adapt<ReservaDto>(), idCliente, producto);
    }

    public void CancelarReserva(int idReserva)
    {
        reservaRepository.CancelarReserva(idReserva);

    }

    public void AprobarReserva(int idReserva)
    {
        reservaRepository.AprobarReserva(idReserva);
    }

    public void DeleteReserva(int id)
    {
        reservaRepository.DeleteReserva(id);
    }

    public ReservaResponseDto GetReserva(int id)
    {
        var reserva = reservaRepository.GetReserva(id);

        return reserva.Adapt<ReservaResponseDto>();
    }

    public List<ReservaResponseDto> GetReservas()
    {
        return reservaRepository.GetReservas().Adapt<List<ReservaResponseDto>>();
    }

    public string UpdateReserva(int id, ReservaRequestDto reserva)
    {
        reservaRepository.UpdateReserva(id, reserva.Adapt<ReservaDto>());

        return "Reserva actualizada";
    }

    public void RechazarReserva(int idReserva)
    {
        reservaRepository.RechazarReserva(idReserva);
    }

}
