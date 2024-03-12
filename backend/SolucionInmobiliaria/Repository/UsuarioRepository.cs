using SolucionInmobiliaria.Endpoints.DTO;
using SolucionInmobiliaria.Domain;
using Microsoft.EntityFrameworkCore;
using DataBase;


namespace SolucionInmobiliaria.Repository;

public interface IUsuarioRepository
{
    List<Usuario> GetUsuarios();
    Usuario GetUsuario(int id);
    void CrearUsuario(UsuarioDto usuarioDto);
    string UpdateUsuario(int id, UsuarioDto usuarioDto);
    void DeleteUsuario(int id);
}

public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
{
    public void CrearUsuario(UsuarioDto usuarioDto)
    {
        //Ver si el usuario ya existe
        if (context.Usuarios.FirstOrDefault(u => u.Id == usuarioDto.Id) != null)
        {
            throw new Exception($"El usuario con id: {usuarioDto.Id} ya existe");
        }

        //Si no existe, crear un nuevo usuario y asignarle los valores del DTO
        var usuario = new Usuario
        {
            Id = context.Usuarios.Count(),
            Nombre = usuarioDto.Nombre,
            Apellido = usuarioDto.Apellido,
            Email = usuarioDto.Email,
            Password = usuarioDto.Password,
            Rol = usuarioDto.Rol,
            ReservasUsuario = new List<Reserva>()
        };

        //Agregar el usuario a la base de datos
        context.Add(usuario);

        //Guardar cambios en la base de datos
        context.SaveChanges();
    }

    public void DeleteUsuario(int id)
    {
        //Buscar el usuario por el id
        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario != null)
        {
            //Eliminar el usuario de la base de datos
            context.Usuarios.Remove(usuario);

            //Guardar cambios en la base de datos
            context.SaveChanges();
        }
        else
        {
            throw new Exception($"El usuario con id: {id} no existe");
        }
    }

    public List<Usuario> GetUsuarios()
    {
        return context.Usuarios.ToList();
    }

    public Usuario GetUsuario(int id)
    {
        if (context.Usuarios.FirstOrDefault(u => u.Id == id) == null)
        {
            throw new Exception($"El usuario con id: {id} no existe");
        }

        return context.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public string UpdateUsuario(int id, UsuarioDto usuarioDto)
    {
        //Buscar el usuario por el id
        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario != null)
        {
            //Actualizar los valores del usuario con los valores del DTO
            usuario.Nombre = usuarioDto.Nombre;
            usuario.Apellido = usuarioDto.Apellido;
            usuario.Email = usuarioDto.Email;
            usuario.Password = usuarioDto.Password;
            usuario.Rol = usuarioDto.Rol;

            //Guardar cambios en la base de datos
            context.SaveChanges();

            return $"El usuario con id: {id} ha sido actualizado";
        }
        else
        {
            throw new Exception($"El usuario con id: {id} no existe");
        }
    }
}       