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

    }
}
