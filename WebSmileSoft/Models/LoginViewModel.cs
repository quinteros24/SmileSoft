using System.ComponentModel.DataAnnotations;

namespace WebSmileSoft.Models
{
    //public class LoginViewModel
    //{

    //        [Required]
    //        public string? UserLogin { get; set; }

    //        [Required]
    //        public string? Password { get; set; }


    //    [Display(Name = "Recordar mi inicio de sesión")]
    //    public bool RememberMe { get; set; }
    //}

    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
