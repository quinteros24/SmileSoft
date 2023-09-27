using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de usuario y roles

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
