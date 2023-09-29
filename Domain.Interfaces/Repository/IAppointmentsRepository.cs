using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<GenericResponseModel> GetAppointmentsList(string? filter = "");
    }
}
