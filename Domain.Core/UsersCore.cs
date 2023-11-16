using Domain.Entities;
using Domain.Interfaces.Core;
using Domain.Interfaces.Repository;
using Repository.Repos;

namespace Domain.Core
{
    public class UsersCore : IUsersCore
    {
        private readonly IUsersRepository _usersRepository;
        public UsersCore(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item, int uIDPetition)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.ChangePassword(Item, uIDPetition);
            return ItemGenericResponseModel;
        }

        public async Task<GenericResponseModel> ViewUsers(int utID)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.ViewUsers(utID);
            return ItemGenericResponseModel;
        }

        public async Task<GenericResponseModel> CreateUpdateUsers(UsersModelRequest Item, int uIDPetition)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.CreateUpdateUsers(Item, uIDPetition);
            return ItemGenericResponseModel;
        }

        public async Task<GenericResponseModel> SetUserStatus(int uID, int uStatus, int uIDPetition)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.SetUserStatus(uID, uStatus, uIDPetition);
            return ItemGenericResponseModel;
        }
        public async Task<GenericResponseModel> GetUserDetails(int? uID, string? uDocument)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.GetUserDetails(uID, uDocument);
            return ItemGenericResponseModel;
        }
        public async Task<GenericResponseModel> UnblockUser(int uID, int uIDPetition)
        {
            GenericResponseModel ItemGenericResponseModel = await _usersRepository.UnblockUser(uID, uIDPetition);
            return ItemGenericResponseModel;
        }


    }
}
