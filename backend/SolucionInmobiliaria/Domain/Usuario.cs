using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolucionInmobiliaria.Domain
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public required int Id { get; set; }

        public required Roles Rol { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public required string Email { get; set; }

        public required byte[] PasswordHash { get; set; }

        public required byte[] PasswordSalt { get; set; }


    }

    public enum Roles
    {
        Vendedor,
        Comercial
    }
}



