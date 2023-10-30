using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        [Route("v1/StoreUsersClinicStoryFormat")]
        public async Task<GenericResponseModel> StoreUsersClinicStoryFormat(int oID, string jsonObject)
        {
            return await _genericsCore.StoreUsersClinicStoryFormat(jsonObject, oID);
        }

        //Setea la historia clínica del paciente
        [HttpGet]
        [Route("v1/SetUsersClinicStoryFormat")]
        public async Task<GenericResponseModel> SetUsersClinicStoryFormat(string jsonObject, int aID)
        {
            return await _genericsCore.SetUsersClinicStoryFormat(jsonObject, aID);
        }

        //Setea el numero de contacto para las redes sociales
        [HttpPut]
        [Route("v1/SetContactNumber")]
        public async Task<GenericResponseModel> SetContactNumber([FromBody] string cellphoneNumber, int oID)
        {
            return await _genericsCore.SetContactNumber(cellphoneNumber, oID);
        }

        //Obtiene el numero de contacto para las redes sociales
        [HttpPut]
        [Route("v1/GetContactNumber")]
        public async Task<GenericResponseModel> GetContactNumber(int oID)
        {
            return await _genericsCore.GetContactNumber(oID);
        }

        //Obtiene el las historias clínicas del usuario
        [HttpGet]
        [Route("v1/GetUserClinicStory")]
        public async Task<GenericResponseModel> GetUserClinicStory(int? uID, string? uDocument)
        {
            return await _genericsCore.GetUserClinicStory(uID, uDocument);
        }

     

    }
}
