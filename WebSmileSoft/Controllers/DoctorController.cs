using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Models; // Asegúrate de importar el espacio de nombres de tus modelos de doctores

namespace WebSmileSoft.Controllers
{
    [Authorize] // Asegura que solo los usuarios autenticados puedan acceder a este controlador
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// Acción para ver la lista de doctores
        //public IActionResult Index()
        //{
        //    List<Doctor> doctors = _context.Doctors.ToList();
        //    return View(doctors);
        //}

        //// Acción para ver los detalles de un doctor específico
        //public IActionResult Details(int? id)
        //{s
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Doctor doctor = _context.Doctors.Find(id);

        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(doctor);
        //}

        //// Acción para editar un doctor
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Doctor doctor = _context.Doctors.Find(id);

        //    if (doctor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(doctor);
        //}

        //// POST: Acción para guardar los cambios después de editar un doctor
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("Id,Nombre,Especializacion,")] Doctor doctor)
        //{
        //    if (id != doctor.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(doctor);
        //            _context.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            // Manejar errores de actualización aquí
        //            throw;
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(doctor);
        //}

        // Otras acciones relacionadas con la gestión de doctores
    }
}
