using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ResponseDB
    {
        public bool Status { get; set; } = false;
        public string? CodeStatus { get; set; } = "-1";
        public string? MessageStatus { get; set; } = "Sin mensaje mapeado";
        public int RecordsQuantity { get; set; } = 0;

        public SqlDataReader? DrObject { get; set; } = null;
        public DataTable? DtObject { get; set; } = null;
        public DataSet? DsObject { get; set; } = null;

        public List<OutputParametersDB>? ListOutputParameters { get; set; }

    }
}
