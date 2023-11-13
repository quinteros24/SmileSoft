using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentsCore
    {
        Task<GenericResponseModel> GetAppointmentsList(int? uID = 0, int? dID = 0, string? filter = "");
        Task<GenericResponseModel> SetAppointment(AppointmentesModel Item);
        Task<GenericResponseModel> UpdateAppointmentStatus(int aID, int asID, int uIDPetition);
    }
}
