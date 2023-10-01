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

        public async Task<GenericResponseModel> GetAppointmentsList(string? filter = "")
        {
            GenericResponseModel responseModel = await _appointmentsRepository.GetAppointmentsList(filter);
            return responseModel;
        }

        public async Task<GenericResponseModel> SetAppointment(AppointmentesModel Item)
        {
            GenericResponseModel responseModel = await _appointmentsRepository.SetAppointment(Item);
            return responseModel;
        }
    }
}
