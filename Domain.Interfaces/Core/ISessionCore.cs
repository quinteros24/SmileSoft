using Domain.Entities;

namespace Domain.Interfaces.Core
{
    public interface ISessionCore
    {
        Task<GenericResponseModel> Login(LoginModelRequest Item);
    }
}
