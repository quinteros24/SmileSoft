using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Connections
    {
        public SqlConnection connectionDB;

        public Connections(string connectionString)
        {
            // Modificado para realizar el procedimiento de Vendamas
            connectionDB = new SqlConnection(strConnMSSQL(connectionString));
        }

        public void OpenConnection()
        {
            try
            {
                if (connectionDB.State == ConnectionState.Broken || connectionDB.State == ConnectionState.Closed)
                    connectionDB.Open();
            }
            catch (Exception e)
            {
                throw new Exception("Error al abrir la conexión", e);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connectionDB.State == ConnectionState.Open)
                {
                    connectionDB.Close();
                    connectionDB.Dispose();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error al cerrar la conexión", e);
            }
        }

        public static string strConnMSSQL(string connectionString)
        {
            SqlConnectionStringBuilder builder = new(connectionString);

            string Database_atual = "";

            if (!String.IsNullOrEmpty(Database_atual))
                connectionString = connectionString.Replace(builder.InitialCatalog, Database_atual);

            return connectionString;
        }

    }
}
