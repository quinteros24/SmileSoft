using System.ComponentModel.DataAnnotations;

namespace WebSmileSoft.Models
{
    public class RegisterViewModel
    {
        private string userName;

        public string GetUserName()
        {
            return userName;
        }

        public void SetUserName(string value)
        {
            userName = value;
        }

        private string email;

        public string GetEmail()
        {
            return email;
        }

        public void SetEmail(string value)
        {
            email = value;
        }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
