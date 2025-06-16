using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class UsRoomModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe haber un usuario")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Debe haber un Room")]
        public int IdRoom { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdRoom")]
        public RoomModel Room { get; set; } = null!;
        [ForeignKey("IdUsuario")]
        public UsuariosModel Usuario { get; set; } = null!;

    }
}