using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Invitaciones
    {
        [Key]
        public int Id { get; set; }

        public int RoomId { get; set; }

        [Required(ErrorMessage = "Para enviar la invitacion debe de haber un remitente")]
        public string Remitente { get; set; } = string.Empty;
        [Required(ErrorMessage = "Para enviar la invitacion debe de haber un destinatario")]
        public string Destinatario { get; set; } = string.Empty;

        public bool Aceptada { get; set; }
        public DateOnly Fecha { get; set; }

    }
}