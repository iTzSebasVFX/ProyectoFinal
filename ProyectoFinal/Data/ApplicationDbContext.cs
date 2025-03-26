using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using ProyectoFinal.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext
    (DbContextOptions<ApplicationDbContext> options) : base
    (options)
    {
    }

    public DbSet<UsuariosModel> Usuarios { get; set; }
    public DbSet<AdminModel> AdminUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Agregar datos iniciales (seeding)
        modelBuilder.Entity<AdminModel>().HasData(new AdminModel
        {
            Id = 1,
            Nombre = "Administrador",
            Email = "admin@example.com",
            Contraseña = BCrypt.Net.BCrypt.HashPassword("admin123"), // Hashear la contraseña
            FechaRegistro = DateTime.UtcNow,
            Activo = true
        });

    }
}
