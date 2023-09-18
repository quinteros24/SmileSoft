using System.ComponentModel.DataAnnotations;

namespace WebSmileSoft.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Nombre de Usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordar mi inicio de sesión")]
        public bool RememberMe { get; set; }
    }
}
