namespace ProyectoFinal.Models
{
    public class UsViewModel 
    {
        public List<UsuariosModel> ListaUsu {get; set;} = new List<UsuariosModel>();
        public UsuariosModel NuevoUsuario {get; set;} = new UsuariosModel();
    }
}