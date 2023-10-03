using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domain.Interfaces.Core
{
    public interface IBlobsCore
    {
        Task<GenericResponseModel> CreateBlobStorage(IFormFile file);
    }
}
