using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<GenericResponseModel> UpdateTokenSession(int uID, string token, string newToken);
    }
}
