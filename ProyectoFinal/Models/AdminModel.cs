using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class AdminModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Contraseña { get; set; } = string.Empty; // Se recomienda usar hash en lugar de texto plano

        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}