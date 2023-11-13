using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Interfaces;

namespace WebSmileSoft.Controllers
{


    public class DoctorController : Controller
    {

        private readonly ISettings _settings;
        public DoctorController(ISettings settings)
        {
            _settings = settings;
        }



        public IActionResult Index()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult GestiondeCitas()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult AdministrarCitas()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult HistoriasClinicas()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult GestionPacientes()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }


    }
}
