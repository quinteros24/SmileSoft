using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebSmileSoft.Models;
using WebSmileSoft.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Mvc.Rendering;

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




        //[HttpPost]
        //public async Task<LoginViewModelResponse> Login([FromBody] LoginViewModelRequest ItemLogin)
        //{
        //    var HttpClient = new HttpClient();
        //    //var content = new StringContent(JsonConvert.SerializeObject(ItemLogin), Encoding.UTF8, "application/json");
        //    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    LoginViewModelResponse? LoginViewModelItem = new();
        //    var response = await HttpClient.PostAsJsonAsync(_settings.urlEndPoint + "/api/Session/v1/Login", ItemLogin);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        JObject jsonObject = JObject.Parse(json);
        //        var data = jsonObject["itemJson"];
        //        //mostrar el json por consola
        //        Console.WriteLine(data);

        //        //Prueba de Cookies
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, ItemLogin.UserLogin),
        //            // Agregar otros claims según sea necesario
        //        };

        //        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var authProperties = new AuthenticationProperties
        //        {
        //            IsPersistent = true, // Mantener la sesión activa después del cierre del navegador
        //        };

        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        //        string? jsonData = data != null ? data.ToString() : String.Empty;

        //        if (!String.IsNullOrEmpty(jsonData))
        //        {
        //            LoginViewModelItem = JsonConvert.DeserializeObject<LoginViewModelResponse>(jsonData);
        //        }
        //        return LoginViewModelItem!;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Credenciales no válidas");
        //        return LoginViewModelItem;
        //    }
        //}




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
        // Acción para el cierre de sesión


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    // Redirige a la página de inicio o a la página deseada después del cierre de sesión
        //    return RedirectToAction("Login", "Account");
        //}


        // Otras acciones y métodos relacionados con la autenticación
    }
}
