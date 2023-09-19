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

namespace WebSmileSoft.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISettings _settings;

        public AccountController(ISettings settings)
        {
            _settings = settings;
        }


        //// Acción para la página de inicio de sesión
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


        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginViewModelRequest ItemLogin)
        //{
        //    try
        //    {
        //        using var httpClient = new HttpClient();

        //        // Añade el encabezado Content-Type para indicar que se envía JSON.
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // Serializa el objeto ItemLogin como JSON y envíalo en el cuerpo de la solicitud.
        //        var jsonContent = new StringContent(JsonConvert.SerializeObject(ItemLogin), Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync("https://ep-smilesoft-develop.azurewebsites.net/api/Session/v1/Login", jsonContent);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            JObject jsonObject = JObject.Parse(json);

        //            // Verifica si la respuesta contiene un token de acceso y otros datos necesarios.
        //            if (jsonObject.TryGetValue("itemJson", out var data))
        //            {
        //                // Prueba de Cookies
        //                var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, ItemLogin.UserLogin),
        //            // Agregar otros claims según sea necesario
        //        };

        //                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //                var authProperties = new AuthenticationProperties
        //                {
        //                    IsPersistent = true, // Mantener la sesión activa después del cierre del navegador
        //                };

        //                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        //                return RedirectToAction("Index", "Home");
        //            }
        //        }

        //        ModelState.AddModelError(string.Empty, "Credenciales no válidas");
        //        return View(); // Puedes personalizar esta parte para mostrar un mensaje de error.
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones
        //        ModelState.AddModelError(string.Empty, "Error en la solicitud: " + ex.Message);
        //        return View(); // Puedes personalizar esta parte para mostrar un mensaje de error.
        //    }
        //}


        public IActionResult Register()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        ////// POST: Acción para el registro (manejar el formulario de registro)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.GetUserName(),
        //            // Configura otras propiedades de usuario aquí
        //        };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            // Inicia sesión automáticamente al usuario después del registro
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            // Redirige a la página de inicio o a la página deseada después del registro
        //            return RedirectToAction("Index", "Home");
        //        }
        //        // Agrega aquí el manejo de errores en el registro si es necesario
        //    }
        //    return View(model);
        //}


        // Acción para el cierre de sesión


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirige a la página de inicio o a la página deseada después del cierre de sesión
            return RedirectToAction("Login", "Account");
        }
        

        // Otras acciones y métodos relacionados con la autenticación
    }
}
