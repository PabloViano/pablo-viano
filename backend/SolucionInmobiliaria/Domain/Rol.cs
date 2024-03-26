using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolucionInmobiliaria.Domain;

    [Table ("Roles")]
    public class Rol
    {
        [Key]
        public required Guid Id { get; set; }

        public required string Nombre { get; set; }

        public List<Usuario>? Usuarios { get; set; }
    }


