using Microsoft.AspNetCore.Mvc;

namespace ChatSignalR.controller
{
    public class ChatController : Controller
    {
        public static Dictionary<int, string> Lista = new Dictionary<int, string>{
            {1, "Pedro"},
            {2, "Juan"},
            {3, "carlos"}
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