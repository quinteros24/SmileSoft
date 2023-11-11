namespace WebSmileSoft.Models
{
    public class CreateUpdateUsersViewModelRequest
    {
        public string Password { get; set; } = null!;
        public int UID { get; set; }
    }
}
