using Domain.Entities;
using Domain.Interfaces.Repository;

namespace Domain.Interfaces.Core
{
    public interface IUsersCore
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item);
        Task<GenericResponseModel> ViewUsers(int utID);
        Task<GenericResponseModel> CreateUpdateUsers(UsersModelRequest Item);
        Task<GenericResponseModel> SetUserStatus(int uID, int uStatus);
        Task<GenericResponseModel> GetUserDetails(int uID);

    }
}
