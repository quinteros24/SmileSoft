using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Interfaces
{
    public interface IGenericsCore
    {
        Task<GenericResponseModel> GenerateJWToken(int uID, string userNameLogin, string token);
        Task<List<SelectListItem>> GetSpecialities();
    }
}
