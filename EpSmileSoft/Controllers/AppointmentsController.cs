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
        public async Task<GenericResponseModel> GetAppointmentsList(int? uID = 0, int? dID = 0,  string? filter = "")
        {
            return await _appointmentsCore.GetAppointmentsList(uID, dID, filter);
        }

        [HttpPost]
        [Route("v1/SetAppointment")]
        public async Task<GenericResponseModel> SetAppointment([FromBody] AppointmentesModel Item)
        {
            return await _appointmentsCore.SetAppointment(Item);
        }

        [HttpGet]
        [Route("v1/UpdateAppointmentStatus")]
        public async Task<GenericResponseModel> UpdateAppointmentStatus(int aID, int asID, int uIDPetition)
        {
            return await _appointmentsCore.UpdateAppointmentStatus(aID, asID, uIDPetition);
        }
        

    }
}
