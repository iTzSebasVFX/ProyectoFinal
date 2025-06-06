using Microsoft.AspNetCore.Mvc;

namespace ChatSignalR.controller
{
    [SessionValidator]
    public class ChatController : Controller
    {
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

        public IActionResult Invitar(int itemid)
        {
            Console.WriteLine(itemid);
            return View("NewRoom");
        }
    }
}