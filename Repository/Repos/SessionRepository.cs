using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Repository.Queries;
using System.Data;
using System.Runtime.CompilerServices;

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
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.Consultds(Query);
            LoginModelResponse? loginModelResponse = new();
            GenericResponseModel? genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DsObject != null) 
            {
                foreach (DataTable dt in ItemResponseDB.DsObject.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.Rows[0]["TableName"])
                        {
                            case "OBJECT":
                                loginModelResponse = Mapper.GetObjectFromDataTable<LoginModelResponse?>(dt);
                                genericResponseModel.RecordsQuantity = dt.Rows.Count;
                                break;
                            case "Parameters":
                                try
                                {
                                    genericResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                                    genericResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (genericResponseModel.RecordsQuantity == 0)
            {
                genericResponseModel.MessageStatus = "USER HAS NOT BEEN FOUND";
                genericResponseModel.CodeStatus = "404";
                genericResponseModel.Status = false;
            }
            else
            {
                genericResponseModel.ItemJson = loginModelResponse;
                genericResponseModel.Status = true;
            } 
            return genericResponseModel;
        }
    }
}
