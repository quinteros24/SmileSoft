﻿using Domain.Entities;
using Domain.Interfaces.Repository;

namespace Domain.Interfaces.Core
{
    public interface IUsersCore
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item);
        Task<GenericResponseModel> ViewUsers(int utID);

        Task<GenericResponseModel> CreateUpdateUsers(ViewUsersModelRequest Item);
    }
}