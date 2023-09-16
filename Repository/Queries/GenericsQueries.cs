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
                    $"    @token AS NVARCHAR(100) = '{token}',\n" +
                    $"    @newToken AS NVARCHAR(100) = '{newToken}',\n" +
                    $"    @oldToken AS NVARCHAR(100) = (SELECT uToken FROM dbo.Users WHERE [uID] = @uID)\n" +
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
    }
}
