using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de pacientes

namespace WebSmileSoft.Controllers
{
    [Authorize] // Asegura que solo los usuarios autenticados puedan acceder a este controlador
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

       
    }
}
