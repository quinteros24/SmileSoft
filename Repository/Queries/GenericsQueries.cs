using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Repository.Queries
{
    public class GenericsQueries
    {
        public static string UpdateTokenSession(int uID, string token, string newToken)
        {
            return  $"DECLARE @uID AS INT = {uID}\n" +
                    $"DECLARE \n" +
                    $"    @token AS NVARCHAR(MAX) = '{token}',\n" +
                    $"    @newToken AS NVARCHAR(MAX) = '{newToken}',\n" +
                    $"    @oldToken AS NVARCHAR(MAX) = (SELECT uToken FROM dbo.Users WHERE [uID] = @uID)\n" +
                    $"BEGIN TRY\n" +
                    $"    IF (@token <> '' AND @token <> @oldToken)\n" +
                    $"        SELECT '-1' AS OutputCodeError, 'Diferente inicio de sesión' AS OutputMessageError\n" +
                    $"    ELSE\n" +
                    $"    BEGIN\n" +
                    $"        UPDATE dbo.Users SET uToken = @newToken WHERE [uID] = @uID AND uStatus = 1\n" +
                    $"        SELECT '0' AS OutputCodeError, 'Token actualizado' AS OutputMessageError\n" +
                    $"    END\n" +
                    $"END TRY\n" +
                    $"BEGIN CATCH\n" +
                    $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
                    $"END CATCH";
        }

        public static string GetUserClinicStory(int? uID, string? uDocument)
        {
            return  $"SELECT A.MedicalRecordObject\n" +
                    $"FROM Appointments AS A\n" +
                    $"LEFT JOIN Users AS U ON A.uID = U.uID\n" +
                    $"WHERE (A.uID = '{uID}' OR U.uDocument = '{uDocument}')\n" +
                    $"    AND A.oID = 1\n" +
                    $"    AND A.MedicalRecordObject IS NOT NULL\n" +
                    $"ORDER BY A.aDate DESC";
        }

        public static string GetAppointmentsUserBlob(string uDocument)
        {
            return $"SELECT A.aUrlImage\n" +
                $"FROM Appointments AS A\n " +
                $"INNER JOIN Users AS U ON A.uID = U.uID\n" +
                $"WHERE U.uDocument = '{uDocument}' AND A.aUrlImage IS NOT NULL\n" +
                $"ORDER BY A.aDate DESC";
        }

        public static string GetDataSite(int? uID, string? IP = "")
        {
            string query = $"SELECT TOP(1) UrlImageLogin, UrlImageMenu, BackgroundColor, TopColor, SideColor FROM Offices AS O \n";

            string WHERE = $" WHERE [oID] = 0";

            if (uID != null && uID != 0)
            {
                WHERE = $"INNER JOIN Users AS U ON O.oID = U.oID AND [uID] = {uID}";
            }

            if (!string.IsNullOrEmpty(IP))
            {
                WHERE = $"INNER JOIN ipAddressPerOffices AS IPPO ON O.oID = IPPO.oID AND ipAddress = '{IP}'";
            }

            query += WHERE;
            return query;
        }

        public static string Getlogs(int pageNumber = 1)
        {
            return $"SELECT\n" +
                   $"    L.uID\n" +
                   $"    ,U.uName\n" +
                   $"    ,U.uLoginName\n" +
                   $"    ,L.utID\n" +
                   $"    ,L.logAction\n" +
                   $"    ,L.logDescription\n" +
                   $"    ,L.logJSON\n" +
                   $"    ,L.logCreationDate\n" +
                   $"    ,(SELECT COUNT(*) FROM Logs) AS TotalRecords\n" +
                   $"FROM\n" +
                   $"    Logs AS L INNER JOIN Users AS U ON L.uID = U.uID\n" +
                   $"ORDER BY\n" +
                   $"    L.logCreationDate DESC\n" +
                   $"    ,U.uName ASC\n" +
                   $"    OFFSET 10 *({pageNumber} - 1) ROWS FETCH NEXT 10 ROWS ONLY";
        }
    }
}
