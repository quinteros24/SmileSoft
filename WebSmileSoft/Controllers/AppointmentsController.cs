using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.Controllers
{
    public class AppointmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdministrarCitas()
        {
            return PartialView();
        }
        public IActionResult GestiondeCitasDoctor()
        {
            return PartialView();
        }
        public IActionResult GestiondeCitasPatient()
        {
            return PartialView();
        }

        // Devuelve una vista parcial según el rol del usuario
        [HttpGet]
        public IActionResult GetPartialViewByRole(int role)
        {
            string partialViewName = "";

            // Determina la vista parcial según el valor de 'role'
            switch (role)
            {
                case 1:
                    partialViewName = "AdministrarCitas"; // Vista para administradores
                    break;
                case 2:
                    partialViewName = "GestiondeCitasDoctor"; // Vista para doctores
                    break;
                case 3:
                    partialViewName = "GestiondeCitasPatient"; // Vista para pacientes
                    break;
                default:
                    // Maneja otros casos o muestra una vista predeterminada
                    break;
            }

            // Devuelve la vista parcial
            return PartialView(partialViewName);
        }
    }
}
