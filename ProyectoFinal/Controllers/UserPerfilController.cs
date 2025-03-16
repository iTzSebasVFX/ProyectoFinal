using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;



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

    public IActionResult ZonadeJuegos()
    {

        return View();
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Html");
    }

}