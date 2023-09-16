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

       
        
    }
}
