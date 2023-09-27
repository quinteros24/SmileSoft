using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de pacientes

namespace WebSmileSoft.Controllers
{
    /*[Authorize]*/ // Asegura que solo los usuarios autenticados puedan acceder a este controlador
    public class PatientController : Controller
    {
        private readonly ISettings _settings;
        public PatientController(ISettings settings)
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

        public IActionResult HistoriaClinicaPacientes()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;

            return View();
        }

        

        


        

    }
}
