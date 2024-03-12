using SolucionInmobiliaria.Endpoints.DTO;
using DataBase;
using SolucionInmobiliaria.Repository;
using Mapster;

namespace SolucionInmobiliaria.Services;

public interface IReservaService
{
    List<ReservaResponseDto> GetReservas();

    ReservaResponseDto GetReserva(int id);

    void AddReserva(ReservaRequestDto reservaRequestDto);

    string UpdateReserva(int id, ReservaRequestDto reserva);

    void DeleteReserva(int id);
}

public class ReservaService(IReservaRepository reservaRepository) : IReservaService
{
    public void AddReserva(ReservaRequestDto reservaRequest)
    {
        reservaRepository.AddReserva(reservaRequest.Adapt<ReservaDto>());
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
}
