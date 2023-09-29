using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Repository;

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
    }
}
