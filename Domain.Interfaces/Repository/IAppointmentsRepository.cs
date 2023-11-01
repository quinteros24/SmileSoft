using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<GenericResponseModel> GetAppointmentsList(int? uID = 0, int? dID = 0, string? filter = "");
        Task<GenericResponseModel> SetAppointment(AppointmentesModel Item);
        Task<GenericResponseModel> UpdateAppointmentStatus(int aID, int asID);
        Task<GenericResponseModel> GetAppointmentsUserBlob(string uDocument);
    }
}
