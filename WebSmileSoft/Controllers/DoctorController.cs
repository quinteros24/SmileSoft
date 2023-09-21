using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de doctores

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
            return View();
        }

       
    }
}
