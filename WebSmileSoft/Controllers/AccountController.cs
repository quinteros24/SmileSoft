using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models;

namespace WebSmileSoft.Controllers
{
    public class AccountController : Controller
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ISettings _settings;

        public AccountController(ISettings settings)
        {
            _settings = settings;
        }


        //Acción para la página de inicio de sesión
        public IActionResult Login()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel usuario)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Establece la URL del backend.
                    string urlBackend = "https://ep-smilesoft.azurewebsites.net/api/Session/v1/Login";

                    // Establece el encabezado de contenido para JSON.
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializa el modelo Usuario como JSON.
                    string jsonUsuario = Newtonsoft.Json.JsonConvert.SerializeObject(usuario);

                    // Crea el contenido de la solicitud POST.
                    HttpContent content = new StringContent(jsonUsuario, System.Text.Encoding.UTF8, "application/json");

                    // Realiza la solicitud POST al backend.
                    HttpResponseMessage response = await httpClient.PostAsync(urlBackend, content);
                    ViewBag.urlEndPoint = urlBackend;
                    ViewBag.jsonUsuario = jsonUsuario;
                    if (response.IsSuccessStatusCode)
                    {
                        // El inicio de sesión fue exitoso.
                        // Puedes procesar la respuesta JSON del backend si es necesario.
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Redirige al usuario según su rol o realiza cualquier otra acción necesaria.
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // El inicio de sesión falló.
                        // Puedes mostrar un mensaje de error o realizar cualquier otra acción necesaria.
                        ViewBag.ErrorMessage = "#" + response;
                        return View(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores si ocurre algún problema al realizar la solicitud.
                ViewBag.ErrorMessage = "Error al iniciar sesión: " + ex.Message;
                return View(usuario);
            }
        }








        public IActionResult Register()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }
        public IActionResult ForgotPassword()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        public IActionResult RequestanAppointment()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }

        [HttpPost]
        public IActionResult RequestanAppointment([FromBody] List<SelectListItem> specialities)
        {
            ViewBag.Specialities = specialities;
            return View();
        }


    }
}
