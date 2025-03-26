namespace ProyectoFinal.Models
{
    public class AdminModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; } // Se recomienda usar hash en lugar de texto plano
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
    }
}