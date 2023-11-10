namespace Domain.Entities
{
    public class LoginModelRequest
    {
        public string UserLogin { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ipAddress { get; set; }
    }
}
