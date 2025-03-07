using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
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

    public IActionResult Clave()
    {

        return View();
    }

    public IActionResult Juegos()
    {
        return View();
    }

    public HtmlController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> InicioSesion(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar el usuario en la base de datos por email
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == model.Email);
            Console.WriteLine("Hola aqui andamos en verificacion de correo");

            // Si el usuario no existe
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Usuario no encontrado.");
                return View(model); // Si no se encuentra el usuario, se muestra un mensaje de error.
            }
            else
            {
                // Verificar la contraseña (asumiendo que la contraseña está almacenada de forma segura, por ejemplo, hash)
                bool isPasswordValid = usuario.contrasena == model.Password; // Aquí se debe usar un hashing en un caso real
                Console.WriteLine("Hola aqui ya anda en la validacion de contraseña");

                if (!isPasswordValid)
                {
                    ModelState.AddModelError(string.Empty, "Contraseña Incorrecta, Intente de nuevo");
                    return View(model); // Si la contraseña no es válida, se muestra un mensaje de error.
                }
                else
                {
                    // Si las credenciales son correctas, agregar un mensaje en la terminal
                    Console.WriteLine("Bienvenido a la pagina principal " + model.Email);

                    //Ahora crearemos un session que almacene el nombre del usuario
                    HttpContext.Session.SetString("NombreUser", usuario.nombreCompleto);

                    // Redirigir al usuario a otra acción (por ejemplo, a la página principal)
                    return RedirectToAction("Principal", "UserPerfil");
                }
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(RegistroModel nuevoRegistro)
    {

        if (ModelState.IsValid)
        {

            var usuario = new UsuariosModel
            {
                nombreCompleto = nuevoRegistro.nombreCompleto,
                apellidoCompleto = nuevoRegistro.apellidoCompleto,
                correoElectronico = nuevoRegistro.correoElectronico,
                contrasena = nuevoRegistro.contrasena
            };

            var UserSearch = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == usuario.correoElectronico);

            if (UserSearch == null)
            {
                Console.WriteLine("Opa algo salio mal");

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                ModelState.AddModelError("correoElectronico", "Usuario registrado correctamente");
                return RedirectToAction("Index", "Html");

            }

            ModelState.AddModelError("correoElectronico", "El correo electrónico ya está en uso.");
            return View(usuario);
        }

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }

        ModelState.AddModelError(string.Empty, "Ingrese datos porfavor");
        return View(nuevoRegistro);
    }

}
