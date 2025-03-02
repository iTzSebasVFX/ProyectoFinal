using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class RegistroModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Porfavor Ingrese un nombre")]
        public string? nombreCompleto {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un apellido")]
        public string? apellidoCompleto {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un nombre")]
        public string? fechaNacimiento {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un nombre")]
        public string? numeroTelefono {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un nombre")]
        public string? pais {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un Email")]
        public string? correoElectronico {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un Contraseņa")]
        public string? contrasena {get; set;} = string.Empty;
    }
}