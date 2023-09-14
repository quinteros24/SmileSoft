using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Repository.Queries;
using System.Data;

namespace Repository
{
    public class GenericsRepository : IGenericsRepository
    {
        private readonly IConfigManager _configuration;
        public GenericsRepository(IConfigManager configuration)
        {
            _configuration = configuration;
        }

        public async Task<GenericResponseModel> UpdateTokenSession(int uID, string token, string newToken)
        {
            string Query = GenericsQueries.UpdateTokenSession(uID, token, newToken);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(Query);
            GenericResponseModel? genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;

            }
            return genericResponseModel;
        }
    }
}
