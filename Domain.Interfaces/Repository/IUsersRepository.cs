
using Domain.Entities;


namespace Domain.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item, int uIDPetition);
        Task<GenericResponseModel> ViewUsers(int utID);
        Task<GenericResponseModel> CreateUpdateUsers(UsersModelRequest Item, int uIDPetition);
        Task<GenericResponseModel> SetUserStatus(int uID, int uStatus, int uIDPetition);
        Task<GenericResponseModel> GetUserDetails(int? uID, string? uDocument);
        Task<GenericResponseModel> UnblockUser(int uID, int uIDPetition);
    }

}
