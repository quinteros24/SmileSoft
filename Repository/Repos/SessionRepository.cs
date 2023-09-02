using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Repository.Queries;

namespace Repository.Repos
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IConfigManager _configuration;
        public SessionRepository(IConfigManager configuration)
        {
            this._configuration = configuration;
        }
        public async Task<GenericResponseModel> Login(LoginModelRequest Item)
        {
            string Query = SessionQueries.Login(Item);
            GenericResponseModel GenericResponseModel = new();
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);

            ResponseDB ItemResponse = await dl.ConsultSqlDataTableAsync(Query);
            //ManageDistribuitors = AutoMapperRepository.GetListFromDataTable<DistributorsModelResponse>(ItemResponse.DtObject);
            //ResponseDomain ItemResponseDomain = AutoMapperRepository.SetResponseAttributes(ItemResponse);
            //ItemResponseDomain.ItemJson = ManageDistribuitors;
            if (ItemResponse.RecordsQuantity == 0)
            {
                GenericResponseModel.MessageStatus = "No hay distribuidores";
                GenericResponseModel.Status = false;
            }
            return GenericResponseModel;
        }
    }
}
