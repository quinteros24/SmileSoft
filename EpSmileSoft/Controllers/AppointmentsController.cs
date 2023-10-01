using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar los procesos de las citas")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsCore _appointmentsCore;
        public AppointmentsController(IAppointmentsCore appointmentsCore)
        {
            _appointmentsCore = appointmentsCore;   
        }

        [HttpGet]
        [Route("v1/GetAppointmentsList")]
        public async Task<GenericResponseModel> GetAppointmentsList(string? filter = "")
        {
            return await _appointmentsCore.GetAppointmentsList(filter);
        }

        [HttpPost]
        [Route("v1/SetAppointment")]
        public async Task<GenericResponseModel> SetAppointment([FromBody] AppointmentesModel Item)
        {
            return await _appointmentsCore.SetAppointment(Item);
        }
    }
}
