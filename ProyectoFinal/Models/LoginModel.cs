using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class LoginModel
    {
        public int id { get; set; }

        public string? nombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

}