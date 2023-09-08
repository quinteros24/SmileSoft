using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de citas

namespace WebSmileSoft.Controllers
{
    [Authorize] // Asegura que solo los usuarios autenticados puedan acceder a este controlador
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }



        // Acción para programar una nueva cita
        public IActionResult Schedule()
        {
            // Lógica para cargar datos necesarios para programar la cita
            return View();
        }


        // Otras acciones relacionadas con la programación de citas
    }
}
