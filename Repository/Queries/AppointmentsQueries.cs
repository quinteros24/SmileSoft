using Domain.Entities;

namespace Repository.Queries
{
    public class AppointmentsQueries
    {
        public static string GetAppointmentsList(string? filter = "")
        {
            return $"";
        }
        public static string SetAppointment(AppintmentesModel Item)
        {
            string DECLARE = $"DECLARE\n" +
                             $"    @aID AS INT = {Item.aID}\n" +
                             $"    ,@oID AS INT = {Item.oID}\n" +
                             $"    ,@uID AS INT = {Item.uID}\n" +
                             $"    ,@dtID AS INT = {Item.dtID}\n" +
                             $"    ,@gID AS INT = {Item.gID}\n" +
                             $"    ,@uDocument AS VARCHAR(20) = '{Item.uDocument}'\n" +
                             $"    ,@uName AS VARCHAR(200) = '{Item.uName}'\n" +
                             $"    ,@uLastName AS VARCHAR(200) = '{Item.uLastName}'\n" +
                             $"    ,@uCellphone AS VARCHAR(20) = '{Item.uCellphone}'\n" +
                             $"    ,@dID AS INT = {Item.dID}\n" +
                             $"    ,@aDescription AS VARCHAR(MAX) = '{Item.aDescription}'\n" +
                             $"    ,@asID AS INT = {Item.asID?? 1}\n" +
                             $"    ,@aDate AS DATETIME = {Item.aDate}\n" +
                             $"    ,@uBirthDate AS DATETIME = {Item.uBirthDate}\n" +
                             $"    ,@aTime AS TIME = '00:00:00'\n" +
                             $"    ,@ResponseCreation AS VARCHAR(MAX) = 'Puede iniciar sesión con su nombre de usuario o correo electrónico para gestionar los detalles de la cita.'\n" +
                             $"DECLARE @uPassword AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast(@uDocument AS VARCHAR(8000))))";

            string QUERY = $"{DECLARE}\nBEGIN TRY\n";

            if (Item.aID is null || Item.aID == 0)
            {
                //CREAR
                QUERY += $"IF NOT EXISTS(SELECT TOP(1)* FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"    BEGIN\n" +
                         $"        --NO EXISTE LO CREAMOS\n" +
                         $"        INSERT INTO Users(utID,uName,uLastName,uCellphone,uLoginName,uPassword,dtID,uDocument,[oID],gID,uBirthDate)\n" +
                         $"        VALUES(3,@uName,@uLastName,@uCellphone,@uDocument,@uPassword,@dtID,@uDocument,@oID,@gID,@uBirthDate)\n" +
                         $"        SET @ResponseCreation = CONCAT(\n" +
                         $"            'Se ha registrado correctamente, para ver el detalle de las citas puede iniciar sesión con el usuario '\n" +
                         $"            ,@uDocument\n" +
                         $"            ,'\". Su contraseña es el documento de identidad')\n" +
                         $"        SET @uID = (SELECT TOP(1)[uID] FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"    END\n" +
                         $"    SET @uID = ISNULL(@uID,(SELECT TOP(1)[uID] FROM Users WHERE [uDocument] = @uDocument))\n" +
                         $"    --SE CREA LA CITA\n" +
                         $"    INSERT INTO Appointments([oID],[uID],[dID],aDate,aTime,aDescription)\n" +
                         $"    VALUES(@oID,@uID,@dID,@aDate,@aTime,@aDescription)\n" +
                         $"    SELECT '0' AS OutputCodeError, CONCAT('La cita se ha crado. ',@ResponseCreation) AS OutputMessageError\n";
            }
            else
            {
                //EDITAR
                QUERY += $"IF EXISTS(SELECT TOP(1)* FROM Appointments WHERE aID = @aID)\n" +
                         $"    BEGIN \n" +
                         $"        UPDATE Appointments\n" +
                         $"        SET\n" +
                         $"            asID = @asID\n" +
                         $"            ,aDate = @aDate\n" +
                         $"            ,aTime = @aTime\n" +
                         $"            ,aDescription = @aDescription\n" +
                         $"            ,dID = @dID\n" +
                         $"        WHERE \n" +
                         $"            aID = @aID\n" +
                         $"        SELECT '0' AS OutputCodeError, CONCAT('Se han actualizado los datos de la cita ',@aID) AS OutputMessageError\n" +
                         $"    END\n" +
                         $"    ELSE\n" +
                         $"        SELECT '-1' AS OutputCodeError, 'No se encontraron datos, por favor intente nuevamente' AS OutputMessageError\n";
            }

            QUERY += "END TRY\n" +
                     "BEGIN CATCH\n" +
                     "    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
                     "END CATCH";

            return QUERY;
        }
    }
}
