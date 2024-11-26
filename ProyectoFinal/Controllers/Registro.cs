using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

public class Registro: Controller{
    private readonly ApplicationDbContext _context;

    public Registro(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Crear(RegistroModel usuario)
    {
        if (ModelState.IsValid){
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Html");
        }
        return View(usuario);
    }
    
    // public async Task<IActionResult> Index(Usuario usuario)
    // {
    //     return View(await _context.Usuarios.ToArrayAsync());
    // }
}