using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de usuario y roles

namespace WebSmileSoft.Controllers
{
    //[Authorize(Roles = "Administrador")] // Asegura que solo los usuarios autenticados con el rol de "Administrador" puedan acceder a este controlador
    public class AdminController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;


        public IActionResult Table()
        {
            return View("~/Views/Admin/ViewTable.cshtml");
        }
        public IActionResult Index()
        {
            return View("~/Views/Admin/Dashboard.cshtml");
        }

        //public AdminController(
        //    UserManager<ApplicationUser> userManager,
        //    RoleManager<IdentityRole> roleManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //}

        //// Acción para la gestión de usuarios
        //public IActionResult UserManagement()
        //{
        //    List<ApplicationUser> users = _userManager.Users.ToList();
        //    return View(users);
        //}

        //// Acción para ver los detalles de un usuario específico
        //public async Task<IActionResult> UserDetails(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ApplicationUser user = await _userManager.FindByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// Acción para editar un usuario
        //public async Task<IActionResult> EditUser(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ApplicationUser user = await _userManager.FindByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Acción para guardar los cambios después de editar un usuario
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditUser(string id, [Bind("Id,UserName,Email,")] ApplicationUser user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _userManager.UpdateAsync(user);
        //        }
        //        catch (Exception)
        //        {
        //            // Manejar errores de actualización aquí
        //            throw;
        //        }

        //        return RedirectToAction(nameof(UserManagement));
        //    }

        //    return View(user);
        //}

        //// Acción para la gestión de roles
        //public IActionResult RoleManagement()
        //{
        //    List<IdentityRole> roles = _roleManager.Roles.ToList();
        //    return View(roles);
        //}

        //// Otras acciones relacionadas con la administración del sistema
    }
}
