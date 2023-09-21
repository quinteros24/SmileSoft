﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ContentModel;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace WebSmileSoft.Controllers
{
   
    public class ProfileController : Controller
    {

        private readonly ISettings _settings;

        public ProfileController(ISettings settings)

        {
            _settings = settings;
        }
     


        // GET: HomeController1
        public ActionResult Index()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
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
       
        [HttpPost]
        public async Task<ChangePasswordViewModelResponse> ChangePassword([FromBody] ChangePasswordViewModelRequest Item)
        {
            var HttpClient = new HttpClient();
            //var content = new StringContent(JsonConvert.SerializeObject(ItemLogin), Encoding.UTF8, "application/json");

            ChangePasswordViewModelResponse? ChangePasswordViewModelItem = new();
            var response = await HttpClient.PostAsJsonAsync(_settings.urlEndPoint + "/api/Users/v1/ChangePassword", Item);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(json);
                var data = jsonObject["itemJson"];
                string? jsonData = data != null ? data.ToString() : String.Empty;

                if (!String.IsNullOrEmpty(jsonData))
                {
                    ChangePasswordViewModelItem = JsonConvert.DeserializeObject<ChangePasswordViewModelResponse>(jsonData);
                }
                return ChangePasswordViewModelItem!;
            }
            else
                return ChangePasswordViewModelItem;
        }

       
    }
}