using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Text;
using System.Threading.Tasks;
using WebSmileSoft.Models;


namespace WebSmileSoft.Controllers
{
    public class AccountController : Controller
    {
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        //public AccountController(
        //    SignInManager<ApplicationUser> signInManager,
        //    UserManager<ApplicationUser> userManager)
        //{
        //    _signInManager = signInManager;
        //    _userManager = userManager;
        //}

        //// Acción para la página de inicio de sesión
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<LoginViewModelResponse> Login([FromBody]LoginViewModelRequest ItemLogin)
        {
            var HttpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(ItemLogin), Encoding.UTF8, "application/json");

            //Petición
            var response = await HttpClient.PostAsJsonAsync("https://ep-smilesoft-develop.azurewebsites.net/api/Session/v1/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(json);
                var data = jsonObject["itemJson"];
                string? jsonData = data != null ? data.ToString() : String.Empty;
                LoginViewModelResponse? LoginViewModelItem = new();
                GenericResponseModel GenericResponseItem = new();

                LoginViewModelItem = JsonConvert.DeserializeObject<LoginViewModelResponse>(jsonData);
                return LoginViewModelItem;

            }
            else
            {
                // Manejar el error de autenticación
                ModelState.AddModelError("", "Error en el inicio de sesión.");
                return null;
            }
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
