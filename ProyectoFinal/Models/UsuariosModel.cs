using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class UsuariosModel
    {
        [Key]
        public int id { get; set; }

        public string nombreCompleto { get; set; } = string.Empty;

        public string? apellidoCompleto { get; set; } = string.Empty;

        [Range(18, 120, ErrorMessage = "Edad debe estar entre 18 y 120")]
        public int Edad { get; set; }

        public string? numeroTelefono { get; set; } = string.Empty;

        public string? pais { get; set; } = string.Empty;

        public string correoElectronico { get; set; } = string.Empty;

        public string contrasena { get; set; } = string.Empty;

        public int clave { get; set; } = 0;
        
        [Required]
        public string? Genero { get; set; } = string.Empty;

        public string? FotoRuta { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? FotoPerfil { get; set; }

    }
}   