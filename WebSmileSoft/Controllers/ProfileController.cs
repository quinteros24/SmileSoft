using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ContentModel;
using WebSmileSoft.Models;

namespace WebSmileSoft.Controllers
{
    public class ProfileController : Controller
    {
        // GET: HomeController1
        public ActionResult Index()
        {
            var especialidadesOdontologia = new List<EspecialidadOdontologia>
            {
                new EspecialidadOdontologia { Id = 1, Nombre = "Odontología General", Descripcion = "Atención dental general" },
                new EspecialidadOdontologia { Id = 2, Nombre = "Ortodoncia", Descripcion = "Corrección de la alineación de los dientes" },
                new EspecialidadOdontologia { Id = 3, Nombre = "Endodoncia", Descripcion = "Tratamiento de conductos radiculares" },
                new EspecialidadOdontologia { Id = 4, Nombre = "Periodoncia", Descripcion = "Tratamiento de las encías y tejidos de soporte dental" },
                new EspecialidadOdontologia { Id = 5, Nombre = "Cirugía Oral", Descripcion = "Cirugía dental y maxilofacial" },
                new EspecialidadOdontologia { Id = 6, Nombre = "Implantología Dental", Descripcion = "Colocación de implantes dentales" },
                new EspecialidadOdontologia { Id = 7, Nombre = "Odontopediatría", Descripcion = "Odontología para niños" },
            };
            return View("~/Views/Profile/Profile.cshtml", especialidadesOdontologia);
        }
        // Prueba Carrousel Añadir Imagenes Automaticamente
        public IActionResult Anuncios()
        {
            var directorioImagenes = @"~\assets\img\anuncios\";
            var rutasDeImagenes = Directory.GetFiles(directorioImagenes);
            return PartialView("_Anuncios", rutasDeImagenes);
        }

        public IActionResult Especialidades()
        {
            // Recupera la lista de roles desde la base de datos
            // var roles = dbContext.Roles.ToList();
            var especialidadesOdontologia = new List<EspecialidadOdontologia>
            {
                new EspecialidadOdontologia { Id = 1, Nombre = "Odontología General", Descripcion = "Atención dental general" },
                new EspecialidadOdontologia { Id = 2, Nombre = "Ortodoncia", Descripcion = "Corrección de la alineación de los dientes" },
                new EspecialidadOdontologia { Id = 3, Nombre = "Endodoncia", Descripcion = "Tratamiento de conductos radiculares" },
                new EspecialidadOdontologia { Id = 4, Nombre = "Periodoncia", Descripcion = "Tratamiento de las encías y tejidos de soporte dental" },
                new EspecialidadOdontologia { Id = 5, Nombre = "Cirugía Oral", Descripcion = "Cirugía dental y maxilofacial" },
                new EspecialidadOdontologia { Id = 6, Nombre = "Implantología Dental", Descripcion = "Colocación de implantes dentales" },
                new EspecialidadOdontologia { Id = 7, Nombre = "Odontopediatría", Descripcion = "Odontología para niños" },
                // Puedes agregar más especialidades según sea necesario
            };

            // Pasa la lista de roles a la vista
            //ViewBag.EspecialidadList = new SelectList(especialidadesOdontologia, "Id", "Nombre");

            return PartialView("_Especialidades", especialidadesOdontologia);
        }
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
