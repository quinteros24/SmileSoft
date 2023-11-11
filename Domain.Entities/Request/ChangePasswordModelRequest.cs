namespace Domain.Entities
{
    public class ChangePasswordModelRequest
    {
        public int UID { get; set; }
        public string Password { get; set; } = null!;
    }
}
