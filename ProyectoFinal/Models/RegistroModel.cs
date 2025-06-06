using System.ComponentModel.DataAnnotations;
using Mysqlx;

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

        [Required(ErrorMessage = "Porfavor Ingrese un Email")]
        public string correoElectronico {get; set;} = string.Empty;

        [Required(ErrorMessage = "Porfavor Ingrese un Contrasena")] 
        public string? contrasena {get; set;} = string.Empty;

        public int clave {get; set;} = 0;
    }
}