using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de usuario y roles

namespace WebSmileSoft.Controllers
{
    //[Authorize(Roles = "Administrador")] // Asegura que solo los usuarios autenticados con el rol de "Administrador" puedan acceder a este controlador
    public class AdminController : Controller
    {
        private readonly ISettings _settings;
        public AdminController(ISettings settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult ViewTableUsers()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;

            // Pasa los datos a la vista
            return View();
        }

        [HttpPost]
        public IActionResult ViewTableUsers([FromBody] List<SelectListItem> specialities)
        {
            ViewBag.Specialities = specialities;
            return View();
        }

        public IActionResult UserManagement()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult SiteManagement()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult AddUsers()
        {

            return PartialView();
        }
        public IActionResult ChangePass()
        {

            return PartialView();
        }
        public IActionResult EditUser(UsuariosModel Modelo)
        {

            return PartialView(Modelo);
        }




    }
}
