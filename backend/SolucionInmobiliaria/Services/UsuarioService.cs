using SolucionInmobiliaria.Endpoints.DTO;
using DataBase;
using SolucionInmobiliaria.Repository;
using Mapster;

namespace SolucionInmobiliaria.Services;

public interface IUsuarioService
{
    List<UsuarioResponseDto> GetUsuarios();

    UsuarioResponseDto GetUsuario(int id);

    void DeleteUsuario(int id);

    string UpdateUsuario(int id, UsuarioRequestDto usuario);

    void AddUsuario(UsuarioRequestDto usuario);
}

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    public void AddUsuario(UsuarioRequestDto usuario)
    {
        usuarioRepository.AddUsuario(usuario.Adapt<UsuarioDto>());
    }

    public void DeleteUsuario(int id)
    {
        usuarioRepository.DeleteUsuario(id);
    }

    public UsuarioResponseDto GetUsuario(int id)
    {
        return usuarioRepository.GetUsuario(id).Adapt<UsuarioResponseDto>();
    }

    public List<UsuarioResponseDto> GetUsuarios()
    {
        return usuarioRepository.GetUsuarios().Adapt<List<UsuarioResponseDto>>();
    }

    public string UpdateUsuario(int id, UsuarioRequestDto usuario)
    {
        usuarioRepository.UpdateUsuario(id, usuario.Adapt<UsuarioDto>());

        return $"Usuario con id {id} actualizado";
    }
}
