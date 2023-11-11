using Domain.Entities.Response;

namespace Domain.Entities
{
    public class UsersModelRequest : UsersModelResponse
    {
        public string? uPassword { get; set; }
    }
}
