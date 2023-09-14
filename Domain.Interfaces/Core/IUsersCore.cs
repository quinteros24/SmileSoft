using Domain.Entities;

namespace Domain.Interfaces.Core
{
    public interface IUsersCore
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item);
    }
}
