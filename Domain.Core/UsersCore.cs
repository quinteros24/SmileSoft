using Domain.Entities;
using Domain.Interfaces.Core;
using Domain.Interfaces.Repository;

namespace Domain.Core
{
    public class UsersCore : IUsersCore
    {
        private readonly IUsersRepository _usersRepository;
        public UsersCore(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.ChangePassword(Item);
            return ItemGenericResponseModel;
        }

        public async Task<GenericResponseModel> ViewUsers(int utID)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.ViewUsers(utID);
            return ItemGenericResponseModel;
        }

        public async Task<GenericResponseModel> CreateUpdateUsers(UsersModelRequest Item)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.CreateUpdateUsers(Item);
            return ItemGenericResponseModel;
        }

        public async Task<GenericResponseModel> SetUserStatus(int uID, int uStatus)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.SetUserStatus(uID, uStatus);
            return ItemGenericResponseModel;
        }
        public async Task<GenericResponseModel> GetUserDetails(int? uID, string? uDocument)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.GetUserDetails(uID, uDocument);
            return ItemGenericResponseModel;
        }


    }
}
