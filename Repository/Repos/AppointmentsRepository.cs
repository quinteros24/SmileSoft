using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Repository.Queries;
using System.Data;

namespace Repository
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly IConfigManager _configuration;
        public AppointmentsRepository(IConfigManager configuration)
        {
            _configuration = configuration;
        }

        public async Task<GenericResponseModel> GetAppointmentsList(string? filter = "")
        {
            string Query = AppointmentsQueries.GetAppointmentsList(filter);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(Query);
            GenericResponseModel? genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                genericResponseModel.CodeStatus = dt.Rows[0]["outputCodeError"].ToString();
                genericResponseModel.MessageStatus = dt.Rows[0]["outputMessageError"].ToString();
                genericResponseModel.Status = genericResponseModel.CodeStatus == "0";
            }
            return genericResponseModel;
        }
    }
}
