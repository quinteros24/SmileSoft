using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGenericsRepository
    {
        Task<GenericResponseModel> UpdateTokenSession(int uID, string token, string newToken);
    }
}
