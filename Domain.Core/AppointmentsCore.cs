using Domain.Interfaces;
using Domain.Entities;

namespace Domain.Core
{
    public class AppointmentsCore : IAppointmentsCore
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        public AppointmentsCore(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task<GenericResponseModel> GetAppointmentsList(int? uID = 0, int? dID = 0, string? filter = "")
        {
            GenericResponseModel responseModel = await _appointmentsRepository.GetAppointmentsList(uID, dID, filter);
            return responseModel;
        }

        public async Task<GenericResponseModel> SetAppointment(AppointmentesModel Item)
        {
            GenericResponseModel responseModel = await _appointmentsRepository.SetAppointment(Item);
            return responseModel;
        }

        public async Task<GenericResponseModel> UpdateAppointmentStatus(int aID, int asID, int uIDPetition)
        {
            GenericResponseModel responseModel = await _appointmentsRepository.UpdateAppointmentStatus(aID, asID, uIDPetition);
            return responseModel;
        }

       
    }
}
