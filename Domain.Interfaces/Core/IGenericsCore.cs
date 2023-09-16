using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGenericsCore
    {
        Task<GenericResponseModel> GenerateJWToken(int uID, string userNameLogin, string token);
    }
}
