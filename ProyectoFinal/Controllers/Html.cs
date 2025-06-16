using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Mozilla;
using ProyectoFinal.Models;
using ProyectoFinal.Pages;

namespace ProyectoFinal.Controllers;



public class HtmlController : Controller
{
    private readonly ApplicationDbContext _context;

    public HtmlController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Vistas

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult InicioSesion()
    {
        return View();
    }

    public IActionResult iniciarAdmin()
    {
        return View();
    }

    public IActionResult RecuContraseña()
    {
        return View();
    }

    public IActionResult Registro()
    {
        return View();
    }

    // Logica de Inicio y Registro de usuarios

    [HttpPost]
    public async Task<IActionResult> InicioSesion(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar el usuario en la base de datos por email
            var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == model.Email);
            var UserPass = await _context.Usuarios.FirstOrDefaultAsync(u => u.contrasena == model.Password);
            var UserName = await _context.Usuarios.FirstOrDefaultAsync(u => u.nombreCompleto == model.nombreCompleto);

            if (User == null)
            {
                ModelState.AddModelError(string.Empty, "Correo Incorrecto, Intente de nuevo");
                return View("InicioSesion", model);
            }
            bool isPasswordValid = User.contrasena == model.Password;

            if (!isPasswordValid)
            {
                ModelState.AddModelError(string.Empty, "Contraseña Incorrecta, Intente de nuevo");
                return View("InicioSesion", model);
            }

            HttpContext.Session.SetString("NombreUser", User.nombreCompleto);
            HttpContext.Session.SetString("CorreoUsuario", User.correoElectronico);

            return RedirectToAction("Principal", "UserPerfil");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(RegistroModel model)
    {
        // Metodo Para capturar los datos del primer registro
        if (!ModelState.IsValid)
        {
            // Agregar mensaje de error para mostrar en la vista
            ViewData["ErrorMessage"] = "Error en el registro. Rellena los campos.";
            // Enviar de vuelta a la vista de registro con el mensaje de error
            return View("Registro", model);
        }

        var emailSpliteado = model.correoElectronico.Split("@");
        if (emailSpliteado.Length > 2)
        {
            ViewData["ErrorMessage"] = "El correo es invalido, no puede tener mas de dos '@'";
            return View("Registro", model);
        }
        else
        {
            if (emailSpliteado[0].Contains('.'))
            {
                ViewData["ErrorMessage"] = "El correo electronico es invalido, no puede tener un punto antes del '@'";
                return View("Registro", model);
            }
            else
            {
                var correoExiste = await _context.Usuarios.AnyAsync(u => u.correoElectronico == model.correoElectronico);
                if (correoExiste)
                {
                    ViewData["ErrorMessage"] = "Ya hay una cuenta asociada a este correo electronico";
                    return View("Registro", model);
                }

                if (model.clave > 100000 && model.clave < 999999)
                {
                    Console.WriteLine("Si eran validos");
                    var usuario = new UsuariosModel
                    {
                        nombreCompleto = model.nombreCompleto,
                        apellidoCompleto = model.apellidoCompleto,
                        correoElectronico = model.correoElectronico,
                        contrasena = model.contrasena,
                        clave = model.clave
                    };

                    await _context.Usuarios.AddAsync(usuario);
                    await _context.SaveChangesAsync();
                    TempData["RegistroExitoso"] = "Se ha registrado exitosamente";
                    return RedirectToAction("Index", "Html");
                }
                ViewData["ErrorMessage"] = "La clave debe de ser de 6 digitos";
                return View("Registro", model);

            }
        }
    }

    [HttpPost]
    public async Task<IActionResult> RecuContrasena(RecuContra model)
    {
        Console.WriteLine("Si paso por aqui");
        if (ModelState.IsValid)
        {

            Console.WriteLine("Ey estas en validacion de correo");
            var UserSearch = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == model.Email);

            if (UserSearch != null)
            {
                Console.WriteLine("Ya estas en la validacion de la clave");
                bool ConfirmClave = model.clave == UserSearch.clave;

                if (!ConfirmClave)
                {
                    Console.WriteLine("Ey pasaste por el error en la clave");
                    ModelState.AddModelError("clave", "Error, la clave es incorrecta");
                    return View("RecuContraseña", model);
                }
                else
                {
                    Console.WriteLine("Ey clave confirmada, se debio actualizar correctamente");

                    if (model.contrasena != null) UserSearch.contrasena = model.contrasena;
                    _context.Usuarios.Update(UserSearch);
                    await _context.SaveChangesAsync();
                    ModelState.AddModelError("contrasena", "La contraseña ha sido cambiada exitosamente");
                    return RedirectToAction("Index", "Html");
                }
            }
            Console.WriteLine("Ey en el error del correo");
            ModelState.AddModelError("Email", "Error no se encontro al usuario con ese correo electronico");
            return View("RecuContraseña", model);
        }

        Console.WriteLine("Paso por el error.");
        ModelState.AddModelError(string.Empty, "Por favor rellene los campo");
        return View("RecuContraseña", model);
    }

    // Inicio Sesion Admins

    [HttpPost]
    public IActionResult iniciarAdmin(LoginAdModel model)
    {
        Console.WriteLine(model.Email);
        if (ModelState.IsValid)
        {
            var BuscarAd = _context.AdminUsers.FirstOrDefault(a => a.Email == model.Email);

            if (BuscarAd != null)
            {
                bool contraEncrip = BCrypt.Net.BCrypt.Verify(model.Contraseña, BuscarAd.Contraseña);
                Console.WriteLine(contraEncrip);
                if (contraEncrip)
                {
                    HttpContext.Session.SetString("CorreoAdmin", model.Email);
                    TempData["Valido"] = "Bienvenido Admin:" + model.Email;
                    return RedirectToAction("principalAdmin", "Admin");
                }
                TempData["Error"] = "Contraseña incorrecta, intente de nuevo";
                return RedirectToAction("iniciarAdmin");
            }
            TempData["Error"] = "Admin " + model.Email + " no encontrado";
            return RedirectToAction("iniciarAdmin");
        }
        TempData["Error"] = "Porfavor ingresar datos validos";
        return RedirectToAction("iniciarAdmin");
    }
}
