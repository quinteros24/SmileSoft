using Domain.Entities;

namespace Repository.Queries
{
    public static class SessionQueries
    {
        public static string Login(LoginModelRequest ItemLogin)
        {
            string ip = string.Empty;

            if (!string.IsNullOrEmpty(ItemLogin.ipAddress))
            {
                ip = $"                    IF NOT EXISTS(SELECT TOP(1)* FROM ipAdressPerOffices WHERE ipAddress = '{ItemLogin.ipAddress}' AND [oID] = (SELECT [oID] FROM Users WHERE [uID] = @ExistantUser))\n" +
                     $"                    BEGIN\n" +
                     $"                        INSERT INTO ipAdressPerOffices([oID],ipAddress,ippoCreationDate)\n" +
                     $"                        VALUES((SELECT [oID] FROM Users WHERE [uID] = @ExistantUser),'{ItemLogin.ipAddress}',GETUTCDATE())\n" +
                     $"                    END\n";
            }

            return  $"BEGIN TRY\n" +
                    $"    DECLARE @Login AS VARCHAR(MAX) = '{ItemLogin.UserLogin}'\n" +
                    $"    DECLARE @Password AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast('{ItemLogin.Password}' AS VARCHAR(8000))))\n" +
                    $"    --DECLARE @DeveloperPass AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast('smile' AS VARCHAR(8000))))\n" +
                    $"    IF EXISTS (SELECT TOP(1)U.uID FROM dbo.Users AS U WHERE U.uLoginName = @Login OR U.uEmailAddress = @Login OR U.uDocument = @Login)\n" +
                    $"    BEGIN\n" +
                    $"        DECLARE @ExistantUser AS INT = (SELECT TOP(1)U.uID FROM dbo.Users AS U WHERE U.uLoginName = @Login OR U.uEmailAddress = @Login OR U.uDocument = @Login)\n" +
                    $"        IF EXISTS (SELECT TOP(1)uID FROM dbo.Users WHERE uID = @ExistantUser AND uIsBlocked = 1)\n" +
                    $"        BEGIN\n" +
                    $"            SELECT '-1' AS OutputCodeError, 'Usuario bloqueado' AS OutputMessageError, 'Parameters' AS TableName\n" +
                    $"        END\n" +
                    $"        ELSE\n" +
                    $"        BEGIN\n" +
                    $"            IF EXISTS (SELECT TOP(1)uID FROM dbo.Users WHERE uID = @ExistantUser AND uStatus = 0)\n" +
                    $"            BEGIN\n" +
                    $"                SELECT '-1' AS OutputCodeError, 'Usuario inactivo' AS OutputMessageError, 'Parameters' AS TableName\n" +
                    $"            END\n" +
                    $"            ELSE\n" +
                    $"            BEGIN" +
                    $"                --Existe un usuario, ahora revisamos si la contraseña es correcta\n" +
                    $"                IF NOT EXISTS (SELECT uID FROM dbo.Users WHERE uID = @ExistantUser AND uPassword = @Password)\n" +
                    $"                BEGIN\n" +
                    $"                    --Contraseña incorrecta\n" +
                    $"                    UPDATE dbo.Users SET uFailedAttempts += 1, uLastAttemptDate = GETUTCDATE() \n" +
                    $"                    WHERE uID = @ExistantUser\n" +
                    $"                    SELECT '-1' AS OutputCodeError, 'Contraseña incorrecta' AS OutputMessageError, 'Parameters' AS TableName\n" +
                    $"                END\n" +
                    $"                ELSE\n" +
                    $"                BEGIN\n" +
                    $"                    SELECT\n" +
                    $"                        U.uID,\n" +
                    $"                        U.utID,\n" +
                    $"                        uName,\n" +
                    $"                        uLastName,\n" +
                    $"                        uCellphone,\n" +
                    $"                        uAddress,\n" +
                    $"                        uLoginName,\n" +
                    $"                        uEmailAddress,\n" +
                    $"                        dtID,\n" +
                    $"                        oID,\n" +
                    $"                        IIF(U.utID = 2,(SELECT dID FROM Doctors WHERE uID = @ExistantUser),0) AS dID,\n" +
                    $"                        uDocument,\n" +
                    $"                        uToken,\n" +
                    $"                        IIF(uStatus = 1, 1, 0) AS uStatus,\n" +
                    $"                        IIF(uIsBlocked = 1, 1, 0) AS uIsBlocked,\n" +
                    $"                        'OBJECT' AS TableName\n" +
                    $"                    FROM\n" +
                    $"                        dbo.Users AS U\n" +
                    $"                    WHERE\n" +
                    $"                        U.uID = @ExistantUser AND U.uPassword = @Password\n" +
                    $"{ip}" +
                    $"                    SELECT '0' AS OutputCodeError, 'Inicio de sesión con éxito' AS OutputMessageError, 'Parameters' AS TableName\n" +
                    $"                    UPDATE dbo.Users SET uIsBlocked = 0, uFailedAttempts = 0 WHERE uID = @ExistantUser\n" +
                    $"                END\n" +
                    $"            END\n" +
                    $"        END\n" +
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
