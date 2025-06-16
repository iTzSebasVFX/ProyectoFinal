using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Mysqlx;

namespace ProyectoFinal.Models
{
    public class RoomModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El RoomName no puede ir vacio")]
        public string RoomName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del creador no puede ir vacio")]
        public string? CreadorName { get; set; } = string.Empty;

        public DateTime fechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<UsRoomModel>? Participantes { get; set; }
    }
}