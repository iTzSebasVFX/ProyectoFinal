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

    public IActionResult ZonaChat()
    {
        return RedirectToAction("ListaChats", "Chat");
    }

    public IActionResult ZonadeJuegos()
    {

        return View();
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Html");
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

            HttpContext?.Session.SetString("NombreUser", UserSearch.nombreCompleto);

            return RedirectToAction("Perfil", "UserPerfil");

        }
        Console.WriteLine("Datos no cambiados");

        ModelState.AddModelError(string.Empty, "Rellene los campos");
        return RedirectToAction("Perfil", "UserPerfil");
    }
}