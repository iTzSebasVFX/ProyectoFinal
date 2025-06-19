using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");

        var SearchUser = _context.Usuarios.FirstOrDefault(u => u.correoElectronico == CorreoUsuario);
        if (SearchUser != null)
        {
            var datos = new UsViewModel
            {
                ListaUsu = _context.Usuarios.Where(u => u.id != SearchUser.id).ToList(),
                NuevoUsuario = SearchUser
            };
            return View("Principal", datos);
        }
        TempData["Error"] = "No hay un usuario en sesion";
        return View("Principal");
    }

    public async Task<IActionResult> PrincipalFiltrado(string? genero, string? pais, string? edad)
    {
        var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");

        var SearchUser = _context.Usuarios.FirstOrDefault(u => u.correoElectronico == CorreoUsuario);
        if (SearchUser != null)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(genero))
            {
                query = query.Where(u => u.Genero == genero);
            }
            if (!string.IsNullOrEmpty(pais))
            {
                query = query.Where(u => u.pais == pais);
            }
            if (!string.IsNullOrEmpty(edad))
            {
                switch (edad)
                {
                    case "18-25":
                        query = query.Where(u => u.Edad >= 18 && u.Edad <= 25);
                        break;
                    case "26-35":
                        query = query.Where(u => u.Edad >= 26 && u.Edad <= 35);
                        break;
                    case "36-45":
                        query = query.Where(u => u.Edad >= 36 && u.Edad <= 45);
                        break;
                    case "46+":
                        query = query.Where(u => u.Edad >= 46);
                        break;
                }
            }
            var datos = new UsViewModel
            {
                ListaUsu = await query.ToListAsync(),
                NuevoUsuario = SearchUser
            };
            return View("Principal", datos);
        }
        TempData["Error"] = "No hay un usuario en sesion";
        return View("Principal");
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

    public async Task<IActionResult> ListaInvi()
    {
        var correoUsuario = HttpContext.Session.GetString("CorreoUsuario");
        var SearchActualUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == correoUsuario);
        if (SearchActualUser == null)
        {
            return RedirectToAction("Principal", "UserPerfil");
        }

        var ListaInvitaciones = await _context.invitaciones.Where(i => i.Destinatario == SearchActualUser.id.ToString()).ToListAsync();
        return View("Invitaciones", ListaInvitaciones);
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Html");
    }

    public async Task<IActionResult> InvitarUs(int idChat)
    {
        // Buscar el usuario y el room
        var idUser = Convert.ToInt32(HttpContext.Session.GetString("itemid"));
        if (idUser == 0)
        {
            TempData["Error"] = "Error no selecciono ningun usuario";
            return RedirectToAction("Principal", "UserPerfil");
        }
        // Obtener el usuario actual
        var correoUsuario = HttpContext.Session.GetString("CorreoUsuario");

        var SearchRoom = await _context.Room.FirstOrDefaultAsync(r => r.Id == idChat);
        var SearchActualUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == correoUsuario);
        if (SearchRoom == null)
        {
            return RedirectToAction("Principal", "UserPerfil");
        }
        if (SearchActualUser == null)
        {
            TempData["Error"] = "No hay una sesion actual";
            return RedirectToAction("Principal", "UserPerfil");
        }

        var UsuInvi = await _context.invitaciones.AnyAsync(i => i.Destinatario == idUser.ToString() && i.RoomId == SearchRoom.Id);
        if (UsuInvi)
        {
            TempData["Error"] = "Este usuario ya fue invitado a ese room";
            return RedirectToAction("Principal", "UserPerfil");
        }

        var newInvitacion = new Invitaciones
        {
            RoomId = SearchRoom.Id,
            Remitente = SearchActualUser.nombreCompleto,
            Destinatario = idUser.ToString(),
            Aceptada = false
        };
        HttpContext.Session.Remove("itemid");

        await _context.invitaciones.AddAsync(newInvitacion);
        await _context.SaveChangesAsync();

        TempData["Exito"] = "Invitacion enviada exitonsamente";
        return RedirectToAction("Principal", "UserPerfil");
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarPerfil(UsuariosModel datos)
    {
        Console.WriteLine("Inicio");

        if (ModelState.IsValid)
        {
            var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");
            var UserSearch = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == CorreoUsuario);
            if (UserSearch == null)
            {
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
        TempData["ErrorDatos"] = "Rellene todos los campos antes de presionar el boton de guardar.";
        return View("Perfil", datos);
    }
}