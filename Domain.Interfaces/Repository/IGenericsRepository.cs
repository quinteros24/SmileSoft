using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Interfaces
{
    public interface IGenericsRepository
    {
        Task<GenericResponseModel> UpdateTokenSession(int uID, string token, string newToken);
        Task<List<SelectListItem>> GetSpecialities();
        Task<List<SelectListItem>> GetDoctors(int? spID = 0);
    }
}
