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
            string query = AppointmentsQueries.GetAppointmentsList(filter);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.Consultds(query);
            List<AppointmentesModel> ListAppointments = new();
            GenericResponseModel genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DsObject != null)
            {
                foreach (DataTable dt in ItemResponseDB.DsObject.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.Rows[0]["TableName"])
                        {
                            case "OBJECT":
                                ListAppointments = Mapper.GetListFromDataTable<AppointmentesModel>(dt);
                                genericResponseModel.RecordsQuantity = dt.Rows.Count;
                                break;
                            case "Parameters":
                                try
                                {
                                    genericResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                                    genericResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                                }
                                catch (Exception ex)
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
                genericResponseModel.Status = false;
            }
            else
            {
                genericResponseModel.ItemJson = ListAppointments;
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> SetAppointment(AppointmentesModel Item)
        {
            string Query = AppointmentsQueries.SetAppointment(Item);
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
