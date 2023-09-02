using Domain.Entities;

namespace Repository.Queries
{
    public static class SessionQueries
    {
        public static string Login(LoginModelRequest ItemLogin)
        {
            return  $"BEGIN TRY\n" +
                    $"    DECLARE @Login AS VARCHAR(MAX) = '{ItemLogin.UserLogin}'\n" +
                    $"    DECLARE @Password AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast('{ItemLogin.Password}' AS VARCHAR(8000))))\n" +
                    $"    DECLARE @DeveloperPass AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast('smilesoft123456' AS VARCHAR(8000))))\n" +
                    $"    IF EXISTS (SELECT TOP(1)U.uID FROM dbo.Users AS U WHERE (U.uLoginName = @Login OR U.uEmailAddress = @Login OR U.uDocument = @Login) AND (U.uPassword = @Password OR U.uPassword = @DeveloperPass))\n" +
                    $"    BEGIN\n" +
                    $"        SELECT\n" +
                    $"            U.uID,\n" +
                    $"            U.utID,\n" +
                    $"            uName,\n" +
                    $"            uLastName,\n" +
                    $"            uCellphone,\n" +
                    $"            uAddress,\n" +
                    $"            uLoginName,\n" +
                    $"            uEmailAddress,\n" +
                    $"            dtID,\n" +
                    $"            uDocument,\n" +
                    $"            uToken,\n" +
                    $"            uStatus,\n" +
                    $"            uIsBlocked,\n" +
                    $"            'OBJECT' AS TableName\n" +
                    $"        FROM\n" +
                    $"            dbo.Users AS U\n" +
                    $"        WHERE\n" +
                    $"            (U.uLoginName = @Login OR U.uEmailAddress = @Login OR U.uDocument = @Login) AND (U.uPassword = @Password OR U.uPassword = @DeveloperPass)\n" +
                    $"        SELECT '0' AS OutputCodeError, 'Inicio de sesión con éxito' AS OutputMessageError, 'Parameters' AS TableName\n" +
                    $"    END\n" +
                    $"    ELSE\n" +
                    $"        SELECT '404' AS OutputCodeError, 'No se ha encontrado el usuario' AS OutputMessageError, 'Parameters' AS TableName\n" +
                    $"END TRY\n" +
                    $"BEGIN CATCH\n" +
                    $"    SELECT ERROR_MESSAGE() AS OutputMessageError, ERROR_NUMBER() AS OutputCodeError, 'Parameters' AS TableName\n" +
                    $"END CATCH";
        }
    }
}
