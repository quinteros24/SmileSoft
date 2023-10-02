using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.Controllers
{
    public class HomeController : Controller
    {
        // Acción para la página de inicio
        public IActionResult Index()
        {
            return View();
        }

        // Acción para la página "Acerca de"
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Anuncios()
        {
            return View();
        }

        // Otras acciones relacionadas con la página de inicio
    }
}