using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using WebSmileSoft.Models;
using WebSmileSoft.Interfaces;

namespace WebSmileSoft.Controllers
{
    public class AccountController : Controller
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
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


        [HttpPost]
        public async Task<LoginViewModelResponse> Login([FromBody] LoginViewModelRequest ItemLogin)
        {
            var HttpClient = new HttpClient();
            //var content = new StringContent(JsonConvert.SerializeObject(ItemLogin), Encoding.UTF8, "application/json");

            LoginViewModelResponse? LoginViewModelItem = new();
            var response = await HttpClient.PostAsJsonAsync(_settings.urlEndPoint + "/api/Session/v1/Login", ItemLogin);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(json);
                var data = jsonObject["itemJson"];
                string? jsonData = data != null ? data.ToString() : String.Empty;

                if(!String.IsNullOrEmpty(jsonData))
                {
                    LoginViewModelItem = JsonConvert.DeserializeObject<LoginViewModelResponse>(jsonData);
                }
                return LoginViewModelItem!;
            }
            else
                return LoginViewModelItem;
        }


        //// POST: Acción para el inicio de sesión (manejar el formulario de inicio de sesión)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(
        //            model.UserName,
        //            model.Password,
        //            model.RememberMe,
        //            lockoutOnFailure: false);

        //        if (result.Succeeded)
        //        {
        //            // Redirige a la página de inicio o a la página deseada después del inicio de sesión
        //            return RedirectToAction("Index", "Home");
        //        }
        //        // Agrega aquí el manejo de errores en el inicio de sesión si es necesario
        //    }
        //    return View(model);
        //}

        ////// Acción para la página de registro
        public IActionResult Register()
        {
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

        ////// Acción para el cierre de sesión
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    // Redirige a la página de inicio o a la página deseada después del cierre de sesión
        //    return RedirectToAction("Login", "Account");
        //}

        // Otras acciones y métodos relacionados con la autenticación
    }
}
