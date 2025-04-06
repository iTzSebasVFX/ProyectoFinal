namespace ProyectoFinal.Models
{
    public class AdminViewModel
    {
        public List<AdminModel> ListaAdmins { get; set; } = new List<AdminModel>();
        public AdminModel NuevoAdmin { get; set; } = new AdminModel();
    }

}
