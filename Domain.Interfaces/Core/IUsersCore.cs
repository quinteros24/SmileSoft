using Domain.Entities;
using Domain.Interfaces.Repository;

namespace Domain.Interfaces.Core
{
    public interface IUsersCore
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item, int uIDPetition);
        Task<GenericResponseModel> ViewUsers(int utID);
        Task<GenericResponseModel> CreateUpdateUsers(UsersModelRequest Item, int uIDPetition);
        Task<GenericResponseModel> SetUserStatus(int uID, int uStatus, int uIDPetition);
        Task<GenericResponseModel> GetUserDetails(int? uID, string? uDocument);
        Task<GenericResponseModel> UnblockUser(int uID, int uIDPetition);
    }
}
