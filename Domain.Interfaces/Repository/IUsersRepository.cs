
using Domain.Entities;


namespace Domain.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item);
       
    }
}
