using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

[SessionValidator]

public class UserPerfilController : Controller
{

    private readonly ApplicationDbContext _context;

    private readonly IWebHostEnvironment _webHostEnvironment;
    public UserPerfilController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Principal()
    {
        return View();
    }

    public async Task<IActionResult> Perfil()
    {
        var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");

        var SearchUser = await _context.Usuarios.FirstOrDefaultAsync(model => model.correoElectronico == CorreoUsuario);

        if (SearchUser != null)
        {
            return View(SearchUser);
        }
        return View();
    }

    public async Task<IActionResult> Juegos()
    {
        var ListaJuego = await _context.Juegos.ToListAsync();
        return View("Juegos", ListaJuego);
    }

    public IActionResult EditarJuegos()
    {
        return View();
    }

    public IActionResult ZonaChat()
    {
        return RedirectToAction("ListaChats", "Chat");
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Html");
    }

    public async Task<IActionResult> ValidarAdmin()
    {
        Console.WriteLine("Si paso por aqui");
        var NombreAdmin = HttpContext.Session.GetString("NombreUser");
        var Email = HttpContext.Session.GetString("CorreoUsuario");
        var Contraseña = HttpContext.Session.GetString("ContraAdmin");
        Console.WriteLine(Contraseña);

        var BuscarAd = await _context.AdminUsers.FirstOrDefaultAsync(a => a.Email == Email);

        if (BuscarAd != null)
        {
            bool VeriNombre = BuscarAd.Nombre == NombreAdmin;
            bool VeriEmail = BuscarAd.Email == Email;
            bool VeriContraseña = BuscarAd.Contraseña == Contraseña;
            if (VeriNombre && VeriEmail && VeriContraseña == true)
            {
                int Verificador = 1;
                Console.WriteLine("Acceso correcto", Verificador);
                HttpContext.Session.SetString("Verificador", Verificador.ToString());
                return RedirectToAction("Juegos");
            }
            Console.WriteLine("Paso Por error 2");
            return RedirectToAction("Juegos");
        }
        Console.WriteLine("Paso Por error 1");
        return RedirectToAction("Juegos");
    }

    public async Task<IActionResult> PagFormEditar(int id)
    {
        Console.WriteLine(id);
        var BuscarDatos = await _context.Juegos.FirstOrDefaultAsync(j => j.Id == id);
        if (BuscarDatos != null)
        {
            return View("EditarJuegos", BuscarDatos);
        }
        return RedirectToAction("Juegos");
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarPerfil(UsuariosModel datos)
    {
        Console.WriteLine("Inicio");

        if (ModelState.IsValid)
        {

            Console.WriteLine("Aqui en la busqueda de usuario");

            var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");

            var UserSearch = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == CorreoUsuario);

            if (UserSearch == null)
            {
                Console.WriteLine("Paso por el error de busqueda");

                ModelState.AddModelError(string.Empty, "Error usuario no encontrado");
                return RedirectToAction("Principal", "UserPerfil");
            }

            // Realzamos la actualizacion en la bd
            UserSearch.nombreCompleto = datos.nombreCompleto;
            UserSearch.apellidoCompleto = datos.apellidoCompleto;
            UserSearch.Edad = datos.Edad;
            UserSearch.numeroTelefono = datos.numeroTelefono;
            UserSearch.pais = datos.pais;
            UserSearch.correoElectronico = datos.correoElectronico;
            UserSearch.contrasena = datos.contrasena;
            UserSearch.Genero = datos.Genero;

            if (datos.FotoPerfil != null && datos.FotoPerfil.Length > 0)
            {
                //En caso de que el usuario suba una nueva foto, la convertiremos en una ruta nueva en la bd

                Console.WriteLine("Subiendo nueva imagen de perfil");

                //Comenzamos por generar la ruta del archivo
                Console.WriteLine("Aqui en la busqueda de ruta absoluta");
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                //Obtiene la ruta absoluta de la carpeta uploads/ dentro de wwwroot/.
                Console.WriteLine("Aqui en dandole el nombre unico");
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(datos.FotoPerfil.FileName);
                //Genera un nombre único para el archivo.

                Console.WriteLine("Aqui generando ruta donde se guarda el archivo");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //Crea la ruta completa donde se guardará el archivo.

                // Guardar el archivo en la carpeta uploads
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await datos.FotoPerfil.CopyToAsync(fileStream);
                }

                UserSearch.FotoRuta = "/uploads/" + uniqueFileName;
            }

            _context.Usuarios.Update(UserSearch);
            await _context.SaveChangesAsync();

            if (UserSearch != null && !string.IsNullOrEmpty(UserSearch.nombreCompleto))
            {
                HttpContext?.Session.SetString("NombreUser", UserSearch.nombreCompleto);
            }

            return RedirectToAction("Perfil", "UserPerfil");

        }
        Console.WriteLine("Datos no cambiados");

        ModelState.AddModelError(string.Empty, "Rellene los campos");
        return RedirectToAction("Perfil", "UserPerfil");
    }

    [HttpPost]
    public async Task<IActionResult> ModificarJuegosAsync(Juego model)
    {
        Console.WriteLine(model.Nombre);
        if (ModelState.IsValid)
        {
            var BuscarDatos = await _context.Juegos.FirstOrDefaultAsync(j => j.Id == model.Id);
            if (BuscarDatos == null)
            {
                TempData["Error"] = "Error datos no encontrados";
                return View("EditarJuegos", model);
            }
            BuscarDatos.Nombre = model.Nombre;
            BuscarDatos.Descripcion = model.Descripcion;
            BuscarDatos.ImagenFondoUrl = model.ImagenFondoUrl;
            BuscarDatos.UrlJuego = model.UrlJuego;

            _context.Juegos.Update(BuscarDatos);
            _context.SaveChanges();

            return RedirectToAction("Juegos");
        }
        TempData["Error"] = "Error datos incorrectos";
        return View("EditarJuegos", model);
    }
}