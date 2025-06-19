using System.Threading.Tasks;
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
        // Vistas
        public IActionResult chat(int idChat)
        {
            var CorreoUsuario = HttpContext.Session.GetString("CorreoUsuario");
            var buscar = _context.Usuarios.FirstOrDefault(u => u.correoElectronico == CorreoUsuario);
            if (buscar == null)
            {
                return RedirectToAction("Index", "Html");
            }
            HttpContext.Session.SetString("idUser", buscar.id.ToString());
            return View("chat", idChat);
        }

        public async Task<IActionResult> PruebasChatPriv(int itemid)
        {
            if (itemid != 0)
            {
                Console.WriteLine(itemid);
                // Almacenar en una sesion itemid
                HttpContext.Session.SetString("itemid", itemid.ToString());
                // Enviar un dato int por medio de tempdata y mostrar boton seleccion Room
                TempData["Habilitar"] = 1;
            }
            var CorreoUsuario = HttpContext.Session.GetString("CorreoUsuario");
            var SearchUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.correoElectronico == CorreoUsuario);

            if (SearchUser == null)
            {
                return RedirectToAction("Principal", "UserPerfil");
            }
            // Creamos una session con el id del usuario actual
            HttpContext.Session.SetString("userId", SearchUser.id.ToString());
            var rooms = await _context.UsuariosRoom
                .Include(r => r.Room)
                .Include(ur => ur.Usuario)
                .Where(u => u.IdUsuario == SearchUser.id)
                .OrderBy(r => r.Room.fechaCreacion)
                .ToListAsync();

            var conteoUsuariosPorRoom = await _context.UsuariosRoom
                .GroupBy(ur => ur.IdRoom)
                .Select(g => new { RoomId = g.Key, TotalUsuarios = g.Count() })
                .ToDictionaryAsync(g => g.RoomId, g => g.TotalUsuarios);

            // Junta ambos:
            var roomViewModels = rooms.Select(r => new RoomViewModel
            {
                RoomUsuario = r,
                TotalUsuariosEnRoom = conteoUsuariosPorRoom.ContainsKey(r.IdRoom) ? conteoUsuariosPorRoom[r.IdRoom] : 0
            }).ToList();

            // Enviar a la vista
            return View("PruebasChatPriv", roomViewModels);
        }

        // Creacion de nuevos rooms
        [HttpPost]
        public IActionResult InsertarRoom(RoomModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Datos Incorrectos";
                return RedirectToAction("PruebasChatPriv", "Chat");
            }
            Console.WriteLine($"Id: + {model.Id} + RoomName: + {model.RoomName} + CreadorName: + {model.CreadorName} + fechaCreacion: + {model.fechaCreacion} + Participantes: {model.Participantes}");
            _context.Room.Add(model);
            _context.SaveChanges();

            // Como el usuario que crea rooms tambien participa en el room, se debe agregar en la relacion con la tercer tabla entre Usuario y room
            var CorreoUsuario = HttpContext?.Session.GetString("CorreoUsuario");
            var UserSearch = _context.Usuarios.FirstOrDefault(u => u.correoElectronico == CorreoUsuario);
            if (UserSearch != null)
            {
                var relacion = new UsRoomModel
                {
                    IdRoom = model.Id,
                    IdUsuario = Convert.ToInt16(model.CreadorName)
                };

                Console.WriteLine(relacion);
                _context.UsuariosRoom.Add(relacion);
                _context.SaveChanges();

                TempData["Hecho"] = "Nuevo room insertado exitosamente";
                return RedirectToAction("PruebasChatPriv", "Chat");
            }

            TempData["Error"] = "No hay una session registrada";
            return RedirectToAction("Index", "Html");
        }

        // Agregar Usuario a room

        public async Task<IActionResult> Añadir(int inviId)
        {
            if (inviId == 0)
            {
                return RedirectToAction("ListaInvi", "UserPerfil");
            }

            var SearchInvi = await _context.invitaciones.FirstOrDefaultAsync(i => i.Id == inviId);
            if (SearchInvi == null)
            {
                return RedirectToAction("ListaInvi", "UserPerfil");
            }

            var AñadirUsRo = new UsRoomModel
            {
                IdRoom = SearchInvi.RoomId,
                IdUsuario = Convert.ToInt32(SearchInvi.Destinatario)
            };

            await _context.UsuariosRoom.AddAsync(AñadirUsRo);
            await _context.SaveChangesAsync();

            // Actulizamos la invitacion
            // En ef core cuando el dbcontext ratrea datos de una tabla ejemplo la variable "SearchInvi", entonces se crea una instancia de los datos obtenidos
            SearchInvi.Aceptada = true;
            _context.invitaciones.Update(SearchInvi); // Asi que se puede actulizar los datos de una columna incluso sin necesidad del update
            await _context.SaveChangesAsync();

            return RedirectToAction("PruebasChatPriv", "Chat");
        }
    }
}