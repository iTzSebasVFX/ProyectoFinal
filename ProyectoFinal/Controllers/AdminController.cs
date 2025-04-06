using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult principalAdmin()
        {
            var viewModel = new AdminViewModel
            {
                ListaAdmins = _context.AdminUsers.ToList(),
                NuevoAdmin = new AdminModel()
            };

            return View("principalAdmin", viewModel);
        }

        public IActionResult UsuarioLista()
        {
            return View();
        }


        [HttpPost]
        public IActionResult InsertarUs(AdminViewModel model)
        {
            model.NuevoAdmin.FechaRegistro = DateTime.Now;
            Console.WriteLine(model.NuevoAdmin.FechaRegistro);
            Console.WriteLine(model.NuevoAdmin.Activo);
            if (ModelState.IsValid)
            {
                var BuscarUs = _context.AdminUsers.FirstOrDefault(a => a.Email == model.NuevoAdmin.Email);

                if (BuscarUs == null)
                {
                    _context.AdminUsers.Add(model.NuevoAdmin);
                    _context.SaveChanges();
                    TempData["Valido"] = "Correcto, Un nuevo admin a sido ingresado";
                    return RedirectToAction("principalAdmin");
                }
                TempData["Error"] = "Error usuario registrado con ese email";
                model.ListaAdmins = _context.AdminUsers.ToList();
                return View("PrincipalAdmin", model);

            }
            TempData["Error"] = "Error los datos son invalidos";
            model.ListaAdmins = _context.AdminUsers.ToList();
            return View("PrincipalAdmin", model);
        }

        [HttpPost]
        public IActionResult EditarUs(AdminViewModel model)
        {
            Console.WriteLine(model.NuevoAdmin.Id);
            Console.WriteLine(model.NuevoAdmin.Nombre);
            Console.WriteLine(model.NuevoAdmin.Email);
            Console.WriteLine(model.NuevoAdmin.Contraseña);
            Console.WriteLine(model.NuevoAdmin.FechaRegistro);
            Console.WriteLine(model.NuevoAdmin.Activo);
            if (ModelState.IsValid)
            {
                var BuscarUs = _context.AdminUsers.FirstOrDefault(a => a.Id == model.NuevoAdmin.Id);

                if (BuscarUs == null)
                {
                    TempData["Error"] = "Error Admin no encontrado";
                    model.ListaAdmins = _context.AdminUsers.ToList();
                    return View("PrincipalAdmin", model);
                }
                BuscarUs.Id = model.NuevoAdmin.Id;
                BuscarUs.Nombre = model.NuevoAdmin.Nombre;
                BuscarUs.Email = model.NuevoAdmin.Email;
                BuscarUs.Contraseña = model.NuevoAdmin.Contraseña;
                BuscarUs.Activo = model.NuevoAdmin.Activo;

                Console.WriteLine(BuscarUs);
                _context.AdminUsers.Update(BuscarUs);
                _context.SaveChanges();

                TempData["Valido"] = "Datos actualizados correctamente";
                model.ListaAdmins = _context.AdminUsers.ToList();
                return View("PrincipalAdmin", model);
            }
            TempData["Error"] = "Error campos no rellenado";
            model.ListaAdmins = _context.AdminUsers.ToList();
            return View("PrincipalAdmin", model);
        }

        public IActionResult EliminarUs(int Id)
        {
            Console.WriteLine(Id);
            var BuscarUs = _context.AdminUsers.FirstOrDefault(a => a.Id == Id);

            if (BuscarUs != null)
            {
                Console.WriteLine(BuscarUs.Nombre);
                _context.AdminUsers.Remove(BuscarUs);
                _context.SaveChanges();
                TempData["Valido"] = "Admin eliminado correctamente";
                return RedirectToAction("principalAdmin");
            }
            TempData["Error"] = "Error usuario no encontrado";
            return RedirectToAction("principalAdmin");
        }
    }
}