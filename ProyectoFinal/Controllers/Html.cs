using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers;

public class HtmlController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

}
