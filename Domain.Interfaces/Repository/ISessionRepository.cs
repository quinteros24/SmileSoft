using Domain.Entities;

namespace Domain.Interfaces.Repository
{
    public interface ISessionRepository
    {
        Task<GenericResponseModel> Login(LoginModelRequest Item);
    }
}
