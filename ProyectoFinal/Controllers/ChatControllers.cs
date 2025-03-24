using Microsoft.AspNetCore.Mvc;

namespace ChatSignalR.controller
{
    public class ChatController : Controller
    {
        public static Dictionary<int, string> Lista = new Dictionary<int, string>{
            {1, "Hobbies"},
            {2, "Estudios"},
            {3, "Musica"}
        };

        public IActionResult ListaChats()
        {
            return View("ListaChats", Lista);
        }

        public IActionResult chat(int idChat)
        {
            return View("chat", idChat);
        }
    }
}