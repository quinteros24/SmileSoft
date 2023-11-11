using Domain.Entities.Response;

namespace Domain.Entities
{
    public class DoctorsModelRequest : UsersModelResponse
    {
        public string? uPassword { get; set; }
    }
}
