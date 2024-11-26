namespace ProyectoFinal.Models
{
    public class Usuario
    {
        public string nombreCompleto {get; set;} = string.Empty;
        public string apellidoCompleto {get; set;} = string.Empty;
        public int fechaNacimiento {get; set;} = 0;
        public string numeroTelefono {get; set;} = string.Empty;
        public string pais {get; set;} = string.Empty;
        public string correoElectronico {get; set;} = string.Empty;
        public string contrasena {get; set;} = string.Empty;

    }
}