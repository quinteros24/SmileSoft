namespace WebSmileSoft.Models
{
    public class ChangePasswordViewModelRequest
    {
        public string Password { get; set; } = null!;
        public int UID { get; set; }
    }
}
