using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string correoElectronico { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string contrasena { get; set; }
    }

}