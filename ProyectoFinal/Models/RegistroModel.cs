using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class RegistroModel
    {
        [Key]
        public int id {get; set;}
        public string? nombreCompleto {get; set;} = string.Empty;
        public string? apellidoCompleto {get; set;} = string.Empty;
        public string? fechaNacimiento {get; set;} = string.Empty;
        public string? numeroTelefono {get; set;} = string.Empty;
        public string? pais {get; set;} = string.Empty;
        public string? correoElectronico {get; set;} = string.Empty;
        public string? contrasena {get; set;} = string.Empty;
    }
}