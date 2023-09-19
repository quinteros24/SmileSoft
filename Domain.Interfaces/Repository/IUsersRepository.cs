﻿
using Domain.Entities;


namespace Domain.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item);
        Task<GenericResponseModel> ViewUsers(int utID);
        Task<GenericResponseModel> CreateUpdateUsers(ViewUsersModelRequest Item);



    }
}
