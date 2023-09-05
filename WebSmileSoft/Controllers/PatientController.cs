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

        //// Acción para ver la lista de pacientes
        //public IActionResult Index()
        //{
        //    List<Patient> patients = _context.Patients.ToList();
        //    return View(patients);
        //}

        //// Acción para ver los detalles de un paciente específico
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Patient patient = _context.Patients.Find(id);

        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}

        //// Acción para editar un paciente
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Patient patient = _context.Patients.Find(id);

        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(patient);
        //}

        // POST: Acción para guardar los cambios después de editar un paciente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Apellido,Edad,")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    // Manejar errores de actualización aquí
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }

        // Otras acciones relacionadas con la gestión de pacientes
    }
}
