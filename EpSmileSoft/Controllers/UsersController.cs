using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Core;


namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar la sesión de los usuarios")]

    public class UsersController : ControllerBase
    {
        private readonly IUsersCore _usersCore;
        public UsersController(IUsersCore userscore)
        {
            _usersCore = userscore;
        }
        /// <summary> Verificación de datos e inicio de sesión </summary>
        [HttpPost]
        [Route("v1/ChangePassword")]
        public async Task<GenericResponseModel> ChangePassword([FromBody] ChangePasswordModelRequest Item)
        {
            try
            {
                GenericResponseModel ItemGenericResponseModel = await _usersCore.ChangePassword(Item);
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }

    }
}
