using System.ComponentModel.DataAnnotations;

namespace WebSmileSoft.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? UserLogin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? password { get; set; }
    }
}
