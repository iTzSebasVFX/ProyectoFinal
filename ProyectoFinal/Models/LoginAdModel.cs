using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class LoginAdModel 
    {
        [Required]
        public string Email {get; set;} = string.Empty;
        [Required]
        public string Contraseña {get; set;} = string.Empty;
    }
}