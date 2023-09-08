using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.Controllers
{
    public class HomeController : Controller
    {
        // Acción para la página de inicio
        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        // Acción para la página "Acerca de"
        public IActionResult About()
        {
            return View("~/Views/Home/About.cshtml");
        }

        // Otras acciones relacionadas con la página de inicio
    }
}