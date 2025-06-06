using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoFinal.Models;

namespace ChatSignalR.controller
{
    [SessionValidator]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static Dictionary<int, string> Lista = new Dictionary<int, string>{
            {1, "Musica"},
            {2, "Juegos"},
            {3, "Peliculas"}
        };

        public IActionResult ListaChats()
        {
            return View("ListaChats", Lista);
        }

        public IActionResult chat(int idChat)
        {
            return View("chat", idChat);
        }

        public IActionResult CrearRoom()
        {
            var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");
            var SearchUser = _context.Usuarios.FirstOrDefault(u => u.correoElectronico == CorreoUsuario);
            if (SearchUser != null)
            {
                var datos = new RoomModel
                {
                    RoomName = null,
                    CreadorName = SearchUser.id.ToString()
                };
                return View("NewRoom", datos);
            }
            TempData["Error"] = "Error en el intento de creacion";
            return RedirectToAction("ListaChats", "Chat");
        }

        [HttpPost]
        public IActionResult InsertarRoom(RoomModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Datos Incorrectos";
                return RedirectToAction("CrearRoom");
            }
            Console.WriteLine($"Id: + {model.Id} + RoomName: + {model.RoomName} + CreadorName: + {model.CreadorName} + fechaCreacion: + {model.fechaCreacion}");
            return RedirectToAction("CrearRoom", "Chat");
        }
    }
}