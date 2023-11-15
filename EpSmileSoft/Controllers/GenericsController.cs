using Domain.Core;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;

namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar los procesos genéricos")]
    public class GenericsController : ControllerBase
    {
        private readonly IGenericsCore _genericsCore;
        public GenericsController(IGenericsCore genericsCore)
        {
            _genericsCore = genericsCore;
        }
        
        [HttpPost]
        [Route("v1/GenerateJWToken")]
        public async Task<GenericResponseModel> GenerateJWToken([FromHeader] int uID, [FromBody] TokenRequestModel item)
        {
            string userNameLogin = item.UserNameLogin;
            string token = item.Token;

            return await _genericsCore.GenerateJWToken(uID, userNameLogin, token);
        }

        [HttpGet]
        [Route("v1/GetSpecialities")]
        public async Task<List<SelectListItem>> GetSpecialities()
        {
            return await _genericsCore.GetSpecialities();
        }

        [HttpGet]
        [Route("v1/GetDoctors")]
        public async Task<List<SelectListItem>> GetDoctors(int? spID = 0)
        {
            return await _genericsCore.GetDoctors(spID);
        }

        [HttpGet]
        [Route("v1/GetUsersClinicStoryFormat")]
        public async Task<GenericResponseModel> GetUsersClinicStoryFormat(int oID)
        {
            return await _genericsCore.GetUsersClinicStoryFormat(oID);
        }

        //Setea la configuración del json
        [HttpPost]
        [Route("v1/StoreUsersClinicStoryFormat")]
        public async Task<GenericResponseModel> StoreUsersClinicStoryFormat([FromQuery] int oID, [FromQuery] int uIDPetition, [FromBody] string jsonObject)
        {
            return await _genericsCore.StoreUsersClinicStoryFormat(jsonObject, oID, uIDPetition);
        }

        //Setea la historia clínica del paciente
        [HttpPost]
        [Route("v1/SetUsersClinicStoryFormat")]
        public async Task<GenericResponseModel> SetUsersClinicStoryFormat([FromBody] string jsonObject, [FromQuery] int aID, [FromQuery] int uIDPetition)
        {
            return await _genericsCore.SetUsersClinicStoryFormat(jsonObject, aID, uIDPetition);
        }

        //Setea el numero de contacto para las redes sociales
        [HttpPut]
        [Route("v1/SetContactNumber")]
        public async Task<GenericResponseModel> SetContactNumber([FromBody] string cellphoneNumber, [FromQuery] int oID, [FromQuery] int uIDPetition)
        {
            return await _genericsCore.SetContactNumber(cellphoneNumber, oID, uIDPetition);
        }

        //Obtiene el numero de contacto para las redes sociales
        [HttpPut]
        [Route("v1/GetContactNumber")]
        public async Task<GenericResponseModel> GetContactNumber(int oID)
        {
            return await _genericsCore.GetContactNumber(oID);
        }

        //Obtiene las historias clínicas del usuario
        [HttpGet]
        [Route("v1/GetUserClinicStory")]
        public async Task<GenericResponseModel> GetUserClinicStory(int? uID, string? uDocument)
        {
            return await _genericsCore.GetUserClinicStory(uID, uDocument);
        }

        //Obtiene los datos de configuracion de sitio
        [HttpGet]
        [Route("v1/GetDataSite")]
        public async Task<GenericResponseModel> GetDataSite(int? uID = 0, string? IP = "")
        {
            return await _genericsCore.GetDataSite(uID, IP);
        }

        //Guarda los datos de configuración de sitio
        [HttpGet]
        [Route("v1/SetDataSiteUrlImageLogin")]
        public async Task<GenericResponseModel> SetDataSiteUrlImageLogin(int uID, string data)
        {
            return await _genericsCore.SetDataSiteUrlImageLogin(uID, data);
        }

        //Guarda los datos de configuración de sitio
        [HttpGet]
        [Route("v1/SetDataSiteUrlImageMenu")]
        public async Task<GenericResponseModel> SetDataSiteUrlImageMenu(int uID, string data)
        {
            return await _genericsCore.SetDataSiteUrlImageMenu(uID, data);
        }

        //Guarda los datos de configuración de sitio
        [HttpGet]
        [Route("v1/SetDataSiteBackgroundColor")]
        public async Task<GenericResponseModel> SetDataSiteBackgroundColor(int uID, string data)
        {
            return await _genericsCore.SetDataSiteBackgroundColor(uID, data);
        }

        //Guarda los datos de configuración de sitio
        [HttpGet]
        [Route("v1/SetDataSiteTopColor")]
        public async Task<GenericResponseModel> SetDataSiteTopColor(int uID, string data)
        {
            return await _genericsCore.SetDataSiteTopColor(uID, data);
        }

        //Guarda los datos de configuración de sitio
        [HttpGet]
        [Route("v1/SetDataSiteSideColor")]
        public async Task<GenericResponseModel> SetDataSiteSideColor(int uID, string data)
        {
            return await _genericsCore.SetDataSiteSideColor(uID, data);
        }

        [HttpGet]
        [Route("v1/GetAppointmentsUserBlob")]
        public async Task<GenericResponseModel> GetAppointmentsUserBlob(string uDocument)
        {
            return await _genericsCore.GetAppointmentsUserBlob(uDocument);
        }

        [HttpGet]
        [Route("v1/Getlogs")]
        public async Task<GenericResponseModel> Getlogs(int pageNumber = 1)
        {
            return await _genericsCore.Getlogs(pageNumber);
        }

        [HttpGet]
        [Route("v1/GetStates")]
        public async Task<List<SelectListItem>> GetStates()
        {
            return await _genericsCore.GetStates();
        }

        [HttpGet]
        [Route("v1/GetCities")]
        public async Task<List<SelectListItem>> GetCities(int sID)
        {
            return await _genericsCore.GetCities(sID);
        }

        [HttpGet]
        [Route("v1/GetDocumentTypes")]
        public async Task<List<SelectListItem>> GetDocumentTypes()
        {
            return await _genericsCore.GetDocumentTypes();
        }
    }
}
