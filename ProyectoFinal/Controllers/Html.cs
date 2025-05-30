using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Mozilla;
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

    public IActionResult Clave(RegistroModel model)
    {

        return View(model);
    }

    public HtmlController(ApplicationDbContext context)
    {
        _context = context;
    }


    // [HttpPost]
    // public async Task<IActionResult> InicioSesion(LoginModel model)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         // Buscar el usuario en la base de datos por email
    //         var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == model.Email);
    //         Console.WriteLine("Hola aqui andamos en verificacion de correo");

    //         // Si el usuario no existe
    //         if (usuario == null)
    //         {
    //             var admin = await _context.AdminUsers.FirstOrDefaultAsync(a => a.Email == model.Email);

    //             if (admin == null)
    //             {
    //                 ModelState.AddModelError(string.Empty, "Usuario no encontrado.");
    //                 return View("InicioSesion", model); // Si no se encuentra el usuario, se muestra un mensaje de error.
    //             }
    //             Console.WriteLine("Bienvenido a la pagina principal " + model.Email);
    //             bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, admin.Contraseña);

    //             if (!isPasswordValid)
    //             {
    //                 ModelState.AddModelError(string.Empty, "Contraseña Incorrecta, Intente de nuevo");
    //                 return View("InicioSesion", model);
    //             }

    //             HttpContext.Session.SetString("NombreUser", admin.Nombre);
    //             HttpContext.Session.SetString("CorreoUsuario", admin.Email);
    //             HttpContext.Session.SetString("ContraAdmin", admin.Contraseña);

    //             return RedirectToAction("Principal", "UserPerfil");

    //         }
    //         else
    //         {
    //             // Verificar la contraseña (asumiendo que la contraseña está almacenada de forma segura, por ejemplo, hash)
    //             bool isPasswordValid = usuario.contrasena == model.Password; // Aquí se debe usar un hashing en un caso real
    //             Console.WriteLine("Hola aqui ya anda en la validacion de contraseña");

    //             if (!isPasswordValid)
    //             {
    //                 ModelState.AddModelError(string.Empty, "Contraseña Incorrecta, Intente de nuevo");
    //                 return View(model); // Si la contraseña no es válida, se muestra un mensaje de error.
    //             }
    //             else
    //             {
    //                 // Si las credenciales son correctas, agregar un mensaje en la terminal
    //                 Console.WriteLine("Bienvenido a la pagina principal " + model.Email);

    //                 if (usuario != null && !string.IsNullOrEmpty(usuario.nombreCompleto) && !string.IsNullOrEmpty(usuario.correoElectronico))
    //                 {
    //                     //Ahora crearemos un session que almacene el nombre del usuario
    //                     HttpContext.Session.SetString("NombreUser", usuario.nombreCompleto);
    //                     HttpContext.Session.SetString("CorreoUsuario", usuario.correoElectronico);
    //                 }

    //                 // Redirigir al usuario a otra acción (por ejemplo, a la página principal)
    //                 return RedirectToAction("Principal", "UserPerfil");
    //             }
    //         }
    //     }

    //     return View(model);
    // }

    [HttpPost]
    public async Task<IActionResult> InicioSesion(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Buscar el usuario en la base de datos por email
            var admin = await _context.AdminUsers.FirstOrDefaultAsync(a => a.Email == model.Email);
            Console.WriteLine("Hola aqui andamos en verificacion de correo");

            // Si el admin no existe
            if (admin == null)
            {

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == model.Email);

                if (usuario == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuario no encontrado.");
                    return View("InicioSesion", model); // Si no se encuentra el usuario, se muestra un mensaje de error.
                }

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

                    if (usuario != null && !string.IsNullOrEmpty(usuario.nombreCompleto) && !string.IsNullOrEmpty(usuario.correoElectronico))
                    {
                        //Ahora crearemos un session que almacene el nombre del usuario
                        HttpContext.Session.SetString("NombreUser", usuario.nombreCompleto);
                        HttpContext.Session.SetString("CorreoUsuario", usuario.correoElectronico);
                    }

                    // Redirigir al usuario a otra acción (por ejemplo, a la página principal)
                    return RedirectToAction("Principal", "UserPerfil");
                }
            }
            else
            {
                var ValExist = await _context.Usuarios.FirstOrDefaultAsync(u => u.contrasena == admin.Contraseña);
                if (ValExist == null)
                {
                    Console.WriteLine("Bienvenido a la pagina principal " + model.Email);
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, admin.Contraseña);

                    if (!isPasswordValid)
                    {
                        ModelState.AddModelError(string.Empty, "Contraseña Incorrecta, Intente de nuevo");
                        return View("InicioSesion", model);
                    }

                    HttpContext.Session.SetString("NombreUser", admin.Nombre);
                    HttpContext.Session.SetString("CorreoUsuario", admin.Email);
                    HttpContext.Session.SetString("ContraAdmin", admin.Contraseña);

                    return RedirectToAction("Principal", "UserPerfil");
                }

            }
        }

        return View(model);
    }

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
                    Console.WriteLine("Bienvenido a la pagina principal " + model.Email);
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

    [HttpPost]
    public IActionResult CapturarDatos(RegistroModel model)
    {


        Console.WriteLine("Si paso por aqui");

        if (ModelState.IsValid)
        {
            Console.WriteLine("Si eran validos");
            return View("Clave", model);
        }
        else
        {
            Console.WriteLine("Tambien por aqui");

            // Agregar mensaje de error para mostrar en la vista
            ViewData["ErrorMessage"] = "Error en el registro. Verifica los datos.";

            // Enviar de vuelta a la vista de registro con el mensaje de error
            return View("Registro", model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(RegistroModel nuevoRegistro)
    {

        if (ModelState.IsValid)
        {

            var usuario = new UsuariosModel
            {
                nombreCompleto = nuevoRegistro.nombreCompleto,
                apellidoCompleto = nuevoRegistro.apellidoCompleto,
                correoElectronico = nuevoRegistro.correoElectronico,
                contrasena = nuevoRegistro.contrasena,
                clave = nuevoRegistro.clave
            };

            if (usuario.clave < 100000 || usuario.clave > 999999)
            {
                ModelState.AddModelError("clave", "La clave debe tener exactamente 6 dígitos");
                return View("Clave", nuevoRegistro);
            }
            else
            {
                var UserSearch = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == usuario.correoElectronico);

                if (UserSearch == null)
                {
                    Console.WriteLine("Opa pasate por aqui");

                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    ModelState.AddModelError("correoElectronico", "Usuario registrado correctamente");
                    return RedirectToAction("Index", "Html");

                }
                ModelState.AddModelError("correoElectronico", "El correo electrónico ya está en uso.");
                return View("Clave", nuevoRegistro);
            }
        }

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }

        ModelState.AddModelError(string.Empty, "Ingrese datos porfavor");
        return View("Clave", nuevoRegistro);
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

                    UserSearch.contrasena = model.contrasena;

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

}
