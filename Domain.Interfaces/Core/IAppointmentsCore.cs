using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentsCore
    {
        Task<GenericResponseModel> GenerateJWToken(int uID, string userNameLogin, string token);
    }
}
