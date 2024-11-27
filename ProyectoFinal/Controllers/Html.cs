using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

public class HtmlController : Controller
{
    private readonly ApplicationDbContext _context;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult InicioSesion()
    {

        return View();
    }


    public IActionResult Registro()
    {
        return View();
    }

    public HtmlController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar el usuario en la base de datos por email
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == model.correoElectronico);

            // Si el usuario no existe
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
                return View(model); // Si no se encuentra el usuario, se muestra un mensaje de error.
            }

            // Verificar la contraseña (asumiendo que la contraseña está almacenada de forma segura, por ejemplo, hash)
            bool isPasswordValid = usuario.contrasena == model.contrasena; // Aquí se debe usar un hashing en un caso real

            if (!isPasswordValid)
            {
                ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
                return View(model); // Si la contraseña no es válida, se muestra un mensaje de error.
            }

            // Si las credenciales son correctas, redirigir al usuario a una página de inicio
            return RedirectToAction("Inicio", "Home"); // Redirigir a alguna página de inicio.
        }

        return View(model); // Si el modelo no es válido, se vuelve a mostrar el formulario de login.
    }

    [HttpPost]
    public async Task<IActionResult> Index(RegistroModel usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Html");
        }

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }

        return View(usuario);
    }

}
