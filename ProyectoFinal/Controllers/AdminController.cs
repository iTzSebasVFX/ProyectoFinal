using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoFinal.Models;
using ZstdSharp.Unsafe;

namespace ProyectoFinal.Controllers
{
    [SVAdmin]
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
            var viewModel = new UsViewModel
            {
                ListaUsu = _context.Usuarios.ToList(),
                NuevoUsuario = new UsuariosModel()
            };

            return View("UsuarioLista", viewModel);
        }

        /*------------------------Zona Admins---------------------------------------*/

        [HttpPost]
        public IActionResult InsertarAd(AdminViewModel model)
        {
            model.NuevoAdmin.FechaRegistro = DateTime.Now;
            model.NuevoAdmin.Contraseña = BCrypt.Net.BCrypt.HashPassword(model.NuevoAdmin.Contraseña);
            Console.WriteLine(model.NuevoAdmin.FechaRegistro);
            Console.WriteLine(model.NuevoAdmin.Activo);
            if (ModelState.IsValid)
            {
                var BuscarAd = _context.AdminUsers.FirstOrDefault(a => a.Email == model.NuevoAdmin.Email);

                if (BuscarAd == null)
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
        public IActionResult EditarAd(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var BuscarAd = _context.AdminUsers.FirstOrDefault(a => a.Id == model.NuevoAdmin.Id);
                if (BuscarAd == null)
                {
                    TempData["Error"] = "Error Admin no encontrado";
                    model.ListaAdmins = _context.AdminUsers.ToList();
                    return View("PrincipalAdmin", model);
                }
                BuscarAd.Id = model.NuevoAdmin.Id;
                BuscarAd.Nombre = model.NuevoAdmin.Nombre;
                BuscarAd.Email = model.NuevoAdmin.Email;
                BuscarAd.Activo = model.NuevoAdmin.Activo;
                var verificar = model.NuevoAdmin.Contraseña == BuscarAd.Contraseña;
                Console.WriteLine(verificar);
                if (verificar == false)
                {
                    BuscarAd.Contraseña = BCrypt.Net.BCrypt.HashPassword(model.NuevoAdmin.Contraseña);
                }

                Console.WriteLine(BuscarAd);
                _context.AdminUsers.Update(BuscarAd);
                _context.SaveChanges();

                TempData["Valido"] = "Datos actualizados correctamente";
                return RedirectToAction("PrincipalAdmin");
            }
            TempData["Error"] = "Error campos no rellenado";
            model.ListaAdmins = _context.AdminUsers.ToList();
            return View("PrincipalAdmin", model);
        }

        public IActionResult EliminarAd(int Id)
        {
            Console.WriteLine(Id);
            var BuscarAd = _context.AdminUsers.FirstOrDefault(a => a.Id == Id);

            if (BuscarAd != null)
            {
                Console.WriteLine(BuscarAd.Nombre);
                _context.AdminUsers.Remove(BuscarAd);
                _context.SaveChanges();
                TempData["Valido"] = "Admin eliminado corr  ectamente";
                return RedirectToAction("principalAdmin");
            }
            TempData["Error"] = "Error usuario no encontrado";
            return RedirectToAction("principalAdmin");
        }

        /*------------------------Zona Usuarios---------------------------------------*/

        [HttpPost]
        public IActionResult InsertarUs(UsViewModel model)
        {
            Console.WriteLine(model.NuevoUsuario.nombreCompleto);
            if (ModelState.IsValid)
            {
                if (model.NuevoUsuario.clave > 99999 && model.NuevoUsuario.clave < 1000000)
                {
                    var BuscarUs = _context.Usuarios.FirstOrDefault(u => u.correoElectronico == model.NuevoUsuario.correoElectronico);

                    if (BuscarUs != null)
                    {
                        TempData["Error"] = "Datos incorrectos o no ingresados.";
                        model.ListaUsu = _context.Usuarios.ToList();
                        return View("UsuarioLista", model);
                    }

                    _context.Usuarios.Add(model.NuevoUsuario);
                    _context.SaveChanges();
                    TempData["Valido"] = "Correcto, Un nuevo usuario ha sido ingresado";
                    return RedirectToAction("UsuarioLista");
                }
                TempData["Error"] = "Clave mal ingresado, debe contener 6 digitos.";
                model.ListaUsu = _context.Usuarios.ToList();
                return View("UsuarioLista", model);
            }
            TempData["Error"] = "Datos incorrectos o no ingresados.";
            return RedirectToAction("UsuarioLista");
        }

        [HttpPost]
        public IActionResult EditarUs(UsViewModel model)
        {
            Console.WriteLine(model.NuevoUsuario.nombreCompleto);
            if (ModelState.IsValid)
            {
                var BuscarUs = _context.Usuarios.FirstOrDefault(u => u.id == model.NuevoUsuario.id);

                if (BuscarUs != null)
                {
                    BuscarUs.nombreCompleto = model.NuevoUsuario.nombreCompleto;
                    BuscarUs.apellidoCompleto = model.NuevoUsuario.apellidoCompleto;
                    BuscarUs.Edad = model.NuevoUsuario.Edad;
                    BuscarUs.numeroTelefono = model.NuevoUsuario.numeroTelefono;
                    BuscarUs.pais = model.NuevoUsuario.pais;
                    BuscarUs.correoElectronico = model.NuevoUsuario.correoElectronico;
                    BuscarUs.contrasena = model.NuevoUsuario.contrasena;
                    BuscarUs.clave = model.NuevoUsuario.clave;
                    BuscarUs.Genero = model.NuevoUsuario.Genero;

                    if (BuscarUs.clave > 99999 && BuscarUs.clave < 1000000)
                    {
                        // _context.Usuarios.Update(BuscarUs);
                        // _context.SaveChanges();
                        TempData["Valido"] = "Usuario actualizado correctamente";
                        model.ListaUsu = _context.Usuarios.ToList();
                        return View("UsuarioLista", model);
                    }
                    TempData["Error"] = "Error la clave debe contener 6 digitos";
                    model.ListaUsu = _context.Usuarios.ToList();
                    return View("UsuarioLista", model);
                }
                TempData["Error"] = "Usuario no encontrado";
                model.ListaUsu = _context.Usuarios.ToList();
                return View("UsuarioLista", model);
            }
            TempData["Error"] = "Error datos invalidos o no ingresados";
            model.ListaUsu = _context.Usuarios.ToList();
            return View("UsuarioLista", model);
        }
    
        public IActionResult EliminarUs(int id)
        {
            Console.WriteLine(id);
            var BuscarUs = _context.Usuarios.FirstOrDefault(u => u.id == id);
            if (BuscarUs != null)
            {
                Console.WriteLine(BuscarUs.nombreCompleto);
                _context.Usuarios.Remove(BuscarUs);
                _context.SaveChanges();
                TempData["Valido"] = "Usuario eliminado correctamente";
                return RedirectToAction("UsuarioLista");
            }
            TempData["Error"] = "Error usuario no encontrado";
            return RedirectToAction("UsuarioLista");
        }
    }
}