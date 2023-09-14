namespace Domain.Entities
{
    public class ChangePasswordModelRequest
    {
        public string Password { get; set; } = null!;
        public int UID { get; set; } 
    }
}
