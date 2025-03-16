using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class RecuContra
    {
        [Required(ErrorMessage = "Porfavor ingrese su Correo Electronico")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Porfavor ingrese su clave")]
        public int clave { get; set; } = 0;

        [Required(ErrorMessage = "Debe ingresar la nueva contraseña")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 12 palabras")]
        public string? contrasena { get; set; }

        [Required(ErrorMessage = "Porfavor confirma tu constraseña")]
        [Compare("contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string? contrasenaValidator { get; set; }
    }
}