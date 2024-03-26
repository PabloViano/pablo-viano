using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolucionInmobiliaria.Domain
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public required Guid Id { get; set; }

        public string? Nombre { get; set; }

        public string Username { get; set; }

        public required byte[] PasswordHash { get; set; }

        public required byte[] PasswordSalt { get; set; }

        public List<Rol>? RolesUsuario { get; set; }
    }

}



