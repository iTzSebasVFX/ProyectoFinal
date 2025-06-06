using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class UsRoomModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe haber un usuario")]
        public int? IdUsuario { get; set; }
        [Required(ErrorMessage = "Debe haber un Room")]
        public int? IdRoom { get; set; }

        // Propiedades de navegación
        public RoomModel? Room { get; set; }
        public UsuariosModel? Usuario { get; set; }

    }
}