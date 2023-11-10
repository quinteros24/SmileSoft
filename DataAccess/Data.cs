using System.Data;
using System.Data.SqlClient;


namespace DataAccess
{
    public class Data
    {
        private readonly Connections? ObjConnection = null;
        private readonly SqlCommand ObjCommand = new();
        private Exception ex = new();

        public Data(string connectionString)
        {
            ObjConnection = new Connections(connectionString);
        }

        public DataTable? Consult(string query)
        {
            try
            {
                ObjCommand.Connection = ObjConnection!.connectionDB;
                ObjCommand.CommandText = query;
                DataSet ds = new();
                DataTable dt = new();
                SqlDataAdapter da = new();

                ObjConnection.OpenConnection();
                try
                {
                    da.SelectCommand = ObjCommand;
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ObjConnection.CloseConnection();
                    return null;
                }
                finally
                {
                    DataTable dterror;

                    if (ds.Tables.Count > 1)
                    {
                        dterror = ds.Tables[1];
                        DataRow row = dterror.Rows[0];
                        int error = Convert.ToInt32(row["error_code"]);
                        if (error > 0)
                        {
                            dt = dterror;
                        }
                    }
                }
                return dt;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                CloseConnectionDB();
            }

        }

        public async Task<ResponseDB> ConsultSqlDataTableAsync(string sql)
        {
            ResponseDB ItemResponseDB = new();
            if (ObjConnection != null)
            {

                ObjCommand.Connection = ObjConnection.connectionDB;
                ObjCommand.CommandText = sql;
                ObjCommand.CommandTimeout = 120;
                ObjConnection.OpenConnection();

                try
                {
                    ItemResponseDB.DtObject = new DataTable();
                    ItemResponseDB.DrObject = await ObjCommand.ExecuteReaderAsync();
                    ItemResponseDB.DtObject.Load(ItemResponseDB.DrObject);

                    ItemResponseDB.ListOutputParameters = new List<OutputParametersDB>();
                    OutputParametersDB item = new()
                    {
                        Name = "outputMessageError",
                        Value = "Consulta correcta"
                    };
                    ItemResponseDB.ListOutputParameters.Add(item);

                    item = new OutputParametersDB
                    {
                        Name = "outputCodeError",
                        Value = "0"
                    };
                    ItemResponseDB.ListOutputParameters.Add(item);
                    ItemResponseDB.RecordsQuantity = ItemResponseDB.DtObject.Rows.Count;

                }
                catch (Exception ex)
                {
                    this.ex = ex;

                }
                finally
                {
                    CloseConnectionDB();
                }

            }


            SetTableResponseDB(ex, 0, ref ItemResponseDB);

            return ItemResponseDB;
        }

        public async Task<ResponseDB> Consultds(string Query)
        {
            ResponseDB ItemResponseDB = new();
            if (ObjConnection != null)
            {
                ObjCommand.Connection = ObjConnection.connectionDB;
                ObjCommand.CommandText = Query;
                ItemResponseDB.DsObject = new DataSet();
                SqlDataAdapter da = new();
                ObjConnection.OpenConnection();

                try
                {
                    da.SelectCommand = ObjCommand;
                    await Task.Run(() =>
                    {
                        ItemResponseDB.DsObject = new DataSet();
                        da.Fill(ItemResponseDB.DsObject);
                    });

                    ItemResponseDB.ListOutputParameters = new List<OutputParametersDB>();
                    OutputParametersDB item = new()
                    {
                        Name = "outputMessageError",
                        Value = "Consulta correcta"
                    };
                    ItemResponseDB.ListOutputParameters.Add(item);

                    item = new OutputParametersDB
                    {
                        Name = "outputCodeError",
                        Value = "0"
                    };
                    ItemResponseDB.ListOutputParameters.Add(item);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    this.ex = ex;

                }
                finally
                {
                    CloseConnectionDB();
                }

            }
            SetTableResponseDB(ex, 0, ref ItemResponseDB);
            return ItemResponseDB;
        }

        private static void SetTableResponseDB(Exception ex, int result, ref ResponseDB tr)
        {
            OutputParametersDB item;
            string outputMessageError;
            string outputCodeError;
            int AlteredRows = result;
            if (ex == null)
            {
                outputMessageError = "Consulta correcta";
                outputCodeError = "0";
            }
            else
            {
                outputMessageError = "Catch c#: " + ex.Message;
                outputCodeError = ex.HResult.ToString();
            }

            tr = tr == null ? new ResponseDB() : tr;
            tr.ListOutputParameters ??= new List<OutputParametersDB>();

            if (tr.ListOutputParameters.Find(x => x.Name!.Equals("outputMessageError")) == null)
            {
                item = new OutputParametersDB
                {
                    Name = "outputMessageError",
                    Value = outputMessageError
                };
                tr.ListOutputParameters.Add(item);
            }

            if (tr.ListOutputParameters.Find(x => x.Name!.Equals("outputCodeError")) == null)
            {
                item = new OutputParametersDB
                {
                    Name = "outputCodeError",
                    Value = outputCodeError
                };
                tr.ListOutputParameters.Add(item);
            }

            if (tr.ListOutputParameters.Find(x => x.Name!.Equals("AlteredRows")) == null)
            {
                item = new OutputParametersDB
                {
                    Name = "AlteredRows",
                    Value = AlteredRows.ToString()
                };
                tr.ListOutputParameters.Add(item);
            }
        }

        public void CloseConnectionDB()
        {
            ObjConnection?.CloseConnection();
            ObjCommand.Dispose();
        }

    }
}
