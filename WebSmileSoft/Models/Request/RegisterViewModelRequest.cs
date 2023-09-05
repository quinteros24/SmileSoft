namespace WebSmileSoft.Models
{
    public class RegisterViewModelRequest
    {
        public string UserNameRegister { get; set; } = null!;
        public string UserLastNameRegister { get; set; } = null!;
        public string DocumentRegister { get; set; } = null!;
        public string DateRegister { get; set; } = null!;
        public string GenderRegistrer { get; set; } = null!;
        public string MobileRegister { get; set; } = null!;
        public string AdressRegister { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
