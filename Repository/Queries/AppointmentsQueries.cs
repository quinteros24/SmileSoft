using Domain.Entities;

namespace Repository.Queries
{
    public class AppointmentsQueries
    {
        public static string GetAppointmentsList(int? uID = 0, int? dID = 0, string? filter = "")
        {
            string WHERE_USER_DOCTOR = String.Empty;

            if (uID != null && uID != 0)
            {
                WHERE_USER_DOCTOR = $" AND U.uID = {uID}";
            }

            if (dID != null && dID != 0)
            {
                WHERE_USER_DOCTOR = $" AND D.dID = {dID}";
            }

            return $"DECLARE @filter AS VARCHAR(60) = '{filter}'\n" +
                   $"BEGIN TRY\n" +
                   $"    SELECT\n" +
                   $"        A.aID\n" +
                   $"        ,CONCAT(U.uName,' ',U.uLastName) AS uName\n" +
                   $"        ,U.uCellphone\n" +
                   $"        ,U.uEmailAddress\n" +
                   $"        ,A.aDate\n" +
                   $"        ,A.aTime\n" +
                   $"        ,A.aDescription\n" +
                   $"        ,CONCAT((SELECT uName FROM Users WHERE [uID] = D.uID),' ',(SELECT uLastName FROM Users WHERE [uID] = D.uID)) AS uDoctorName\n" +
                   $"        ,A.asID\n" +
                   $"        ,(SELECT asDescription FROM AppointmentStates WHERE asID = A.asId) AS asName\n" +
                   $"        ,'OBJECT' AS TableName\n" +
                   $"    FROM\n" +
                   $"        Appointments AS A \n" +
                   $"        INNER JOIN Users AS U ON A.uID = U.uID\n" +
                   $"        INNER JOIN Doctors AS D ON A.dID = D.dID\n" +
                   $"    WHERE\n" +
                   $"        (uName LIKE @filter OR uName <> '') {WHERE_USER_DOCTOR}\n" +
                   $"    ORDER BY\n" +
                   $"        uName, aDate\n" +
                   $"    SELECT 0 AS OutputCodeError, 'Datos cargados correctamente' AS OutputMessageError, 'Parameters' AS TableName\n" +
                   $"END TRY\n" +
                   $"BEGIN CATCH\n" +
                   $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError, 'Parameters' AS TableName\n" +
                   $"END CATCH";
        }

        public static string SetAppointment(AppointmentesModel Item)
        {
            string fechaB = String.Empty, fechaC = String.Empty, time = String.Empty;

            if (Item.uBirthDate != null)
            {
                fechaB = Item.uBirthDate.Value.Year.ToString() + "-" + Item.uBirthDate.Value.Month.ToString("00") + "-" + Item.uBirthDate.Value.Day.ToString("00");
            }
            if (Item.aDate != null)
            {
                fechaC = Item.aDate.Value.Year.ToString() + "-" + Item.aDate.Value.Month.ToString("00") + "-" + Item.aDate.Value.Day.ToString("00");
            }
            if (Item.aTime != null)
            {
                time = Item.aTime.Value.Hours.ToString() + ":" + Item.aTime.Value.Minutes.ToString() + ":" + Item.aTime.Value.Seconds.ToString();
            }

            string logAction = String.Empty;

            string DECLARE = $"DECLARE\n" +
                             $"    @aID AS INT = {Item.aID ?? 0}\n" +
                             $"    ,@oID AS INT = {Item.oID ?? 0}\n" +
                             $"    ,@uID AS INT = {(Item.uID ?? 0)}\n" +
                             $"    ,@dtID AS INT = {Item.dtID ?? 1}\n" +
                             $"    ,@gID AS INT = {Item.gID ?? 0}\n" +
                             $"    ,@uDocument AS VARCHAR(20) = '{Item.uDocument}'\n" +
                             $"    ,@uName AS VARCHAR(200) = '{Item.uName}'\n" +
                             $"    ,@uLastName AS VARCHAR(200) = '{Item.uLastName}'\n" +
                             $"    ,@uCellphone AS VARCHAR(20) = '{Item.uCellphone}'\n" +
                             $"    ,@dID AS INT = {Item.dID}\n" +
                             $"    ,@aDescription AS VARCHAR(MAX) = '{Item.aDescription}'\n" +
                             $"    ,@asID AS INT = {Item.asID ?? 1}\n" +
                             $"    ,@aDate AS DATETIME = '{fechaC}'\n" +
                             $"    ,@logDescription AS VARCHAR(MAX) = ''\n" +
                             $"    ,@uBirthDate AS DATETIME = '{fechaB}'\n" +
                             $"    ,@aTime AS TIME = '{time}'\n" +
                             $"    ,@ResponseCreation AS VARCHAR(MAX) = 'Puede iniciar sesión con su nombre de usuario o correo electrónico para gestionar los detalles de la cita.'\n" +
                             $"DECLARE @uPassword AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast(@uDocument AS VARCHAR(8000))))";

            string QUERY = $"{DECLARE}\nBEGIN TRY\n";

            if (Item.aID is null || Item.aID == 0)
            {
                logAction = "CREAR";
                //CREAR
                QUERY += $"    DECLARE @id AS INT = (SELECT [uID] FROM Users WHERE uDocument = @uDocument)\n" +
                         $"    IF EXISTS(SELECT TOP(1)* FROM Appointments AS A INNER JOIN Users AS U ON U.uID = A.uID WHERE A.uID = @id AND A.aDate = @aDate AND A.aTime = @aTime AND A.dID = @dID  )\n" +
                         $"    BEGIN \n" +
                         $"        SELECT '-1' AS OutputCodeError, 'Ya tiene una cita en este horario. Para gestionar sus citas puede iniciar sesión o comuníquese con el administrador' AS OutputMessageError\n" +
                         $"    END\n" +
                         $"    ELSE\n" +
                         $"    BEGIN\n" +
                         $"        IF EXISTS(SELECT TOP(1)* FROM Appointments WHERE dID = @dID AND aDate = @aDate AND aTime = @aTime)\n" +
                         $"        BEGIN\n" +
                         $"            SELECT '-1' AS OutputCodeError, 'Este horario no se encuentra disponible, por favor intente nuevamente' AS OutputMessageError\n" +
                         $"        END\n" +
                         $"        ELSE\n" +
                         $"        BEGIN" +
                         $"            IF(@uID = 0)\n" +
                         $"            BEGIN\n" +
                         $"                IF NOT EXISTS(SELECT TOP(1)* FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"                BEGIN\n" +
                         $"                    --NO EXISTE LO CREAMOS\n" +
                         $"                    INSERT INTO Users(utID,uName,uLastName,uCellphone,uLoginName,uPassword,dtID,uDocument,[oID],gID,uBirthDate,uEmailAddress)\n" +
                         $"                    VALUES(3,@uName,@uLastName,@uCellphone,@uDocument,@uPassword,@dtID,@uDocument,@oID,@gID,@uBirthDate,'{Item.uEmailAddress}')\n" +
                         $"                    SET @ResponseCreation = CONCAT(\n" +
                         $"                        'Se ha registrado correctamente, para ver el detalle de las citas puede iniciar sesión con el usuario \"'\n" +
                         $"                        ,@uDocument\n" +
                         $"                        ,'\". Su contraseña es el documento de identidad')\n" +
                         $"                    SET @uID = (SELECT TOP(1)[uID] FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"                    SET @logDescription = CONCAT(@logDescription,'Se ha creado el usuario \"@uDocument\". ')\n" +
                         $"                END\n" +
                         $"                SET @uID = (SELECT TOP(1)[uID] FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"            END\n" +
                         $"            BEGIN\n" +
                         $"                IF EXISTS(SELECT TOP(1)* FROM Users WHERE [uID] = @uID)\n" +
                         $"                BEGIN\n" +
                         $"                    --SE CREA LA CITA\n" +
                         $"                    INSERT INTO Appointments([oID],[uID],[dID],aDate,aTime,aDescription)\n" +
                         $"                    VALUES(@oID,@uID,@dID,@aDate,@aTime,@aDescription)\n" +
                         $"                    SET @ResponseCreation = CONCAT('La cita se ha crado. ',@ResponseCreation)\n" +
                         $"                    SET @logDescription = CONCAT(@logDescription,'Se ha creado la cita.')\n" +
                         $"                END\n" +
                         $"                ELSE\n" +
                         $"                    SET @ResponseCreation = 'El usuario no se ha podidio encontrar, por favor intente de nuevo'\n" +
                         $"            END\n" +
                         $"            SELECT '0' AS OutputCodeError, @ResponseCreation AS OutputMessageError\n" +
                         $"        END\n" +
                         $"    END\n";
            }
            else
            {
                logAction = "EDITAR";
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
                         $"        SET @logDescription = 'Se ha editado la cita'\n" +
                         $"    END\n" +
                         $"    ELSE\n" +
                         $"        SELECT '-1' AS OutputCodeError, 'No se encontraron datos, por favor intente nuevamente' AS OutputMessageError\n";
            }
            
            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(Item);


            string LOG = $"DECLARE @utID INT = (SELECT utID FROM Users WHERE [uID] = @uID)\n" +
                         $"IF(@logDescription != '')\n" +
                         $"BEGIN\n" +
                         $"    INSERT INTO Logs([uID],utID,logAction,logDescription,logJSON)\n" +
                         $"    VALUES(@uID,@utID,'{logAction}',@logDescription, '{JSON}')\n" +
                         $"END\n";

            QUERY += $"    {LOG}\n" +
                      "END TRY\n" +
                      "BEGIN CATCH\n" +
                      "    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
                      "END CATCH";

            return QUERY;
        }

        public static string UpdateAppointmentStatus(int aID, int asID)
        {
            return $"DECLARE \n" +
                   $"    @asID AS INT = {asID},\n" +
                   $"    @aID AS INT = {aID},\n" +
                   $"    @aDate AS DATE,\n" +
                   $"    @response AS VARCHAR(100),\n" +
                   $"    @responseCode AS VARCHAR(5),\n" +
                   $"    @dateNow AS DATE = DATEADD(dd, 0, DATEDIFF(dd, 0, GETUTCDATE()))\n\n" +
                   $"BEGIN TRY\n" +
                   $"    IF NOT EXISTS(SELECT TOP(1)* FROM Appointments WHERE aID = @aID)\n" +
                   $"    BEGIN\n" +
                   $"        SET @responseCode = '-1'\n" +
                   $"        SET @response = 'No se han encontrado los datos de la cita seleccionada, intente nuevamente'\n" +
                   $"    END\n" +
                   $"    ELSE\n" +
                   $"    BEGIN\n" +
                   $"        SET @aDate = (SELECT aDate FROM Appointments WHERE aID = @aID)\n" +
                   $"        IF (@asID = 2 AND @dateNow > (SELECT DATEADD(day, 1, @aDate)))\n" +
                   $"        BEGIN\n" +
                   $"            SET @responseCode = '-1'\n" +
                   $"            SET @response = 'La fecha de confirmación no es correcta, por favor verifíquela'\n" +
                   $"        END\n" +
                   $"        ELSE\n" +
                   $"        BEGIN\n" +
                   $"            UPDATE Appointments \n" +
                   $"            SET\n" +
                   $"                asID = @asID\n" +
                   $"            WHERE\n" +
                   $"                aID = @aID\n" +
                   $"            SET @responseCode = '0'\n" +
                   $"            SET @response = CONCAT('La cita número ',@aID,' ha pasado a estado ',(SELECT asDescription FROM AppointmentStates WHERE asID = @asID))\n" +
                   $"        END\n" +
                   $"    END\n" +
                   $"    SELECT @responseCode AS OutputCodeError, @response AS OutputMessageError\n" +
                   $"END TRY\n" +
                   $"BEGIN CATCH\n" +
                   $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
                   $"END CATCH";
        }


        public static string UpdateAppointmentDate(int uID, int aID, DateTime newDate, TimeOnly newTime)
        {
            return $"";
        }
    }
}
