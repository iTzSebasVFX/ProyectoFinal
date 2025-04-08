using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Juego
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public string ImagenFondoUrl { get; set; } = string.Empty;
        [Required]
        public string UrlJuego { get; set; } = string.Empty;
    }
}
