﻿using Domain.Entities;

namespace Repository.Queries
{
    public class AppointmentsQueries
    {
        public static string GetAppointmentsList(string? filter = "")
        {
            return $"DECLARE @filter AS VARCHAR(60) = '{filter}'" +
                   $"BEGIN TRY\n" +
                   $"    SELECT\n" +
                   $"        A.aID\n" +
                   $"        ,CONCAT(U.uName,' ',U.uLastName) AS uName\n" +
                   $"        ,U.uCellphone\n" +
                   $"        ,U.uEmailAddress\n" +
                   $"        ,A.aDate\n" +
                   $"        ,A.aTime\n" +
                   $"        ,CONCAT((SELECT uName FROM Users WHERE [uID] = D.uID),' ',(SELECT uLastName FROM Users WHERE [uID] = D.uID)) AS uDoctorName\n" +
                   $"        ,'OBJECT' AS TableName\n" +
                   $"    FROM\n" +
                   $"        Appointments AS A \n" +
                   $"        INNER JOIN Users AS U ON A.uID = U.uID\n" +
                   $"        INNER JOIN Doctors AS D ON A.dID = D.dID\n" +
                   $"    WHERE\n" +
                   $"        uName LIKE @filter OR uName <> ''\n" +
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
            string fechaB = Item.uBirthDate.Value.Year.ToString() + "-" + Item.uBirthDate.Value.Month.ToString("00") + "-" + Item.uBirthDate.Value.Day.ToString("00");
            string fechaC = Item.aDate.Value.Year.ToString() + "-" + Item.aDate.Value.Month.ToString("00") + "-" + Item.aDate.Value.Day.ToString("00");
            string time = Item.aTime.Value.Hour.ToString() + ":" + Item.aTime.Value.Minute.ToString() + ":" + Item.aTime.Value.Second.ToString();
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
                             $"    ,@aDate AS DATETIME = '{fechaC}'\n" +
                             $"    ,@uBirthDate AS DATETIME = '{fechaB}'\n" +
                             $"    ,@aTime AS TIME = '{time}'\n" +
                             $"    ,@ResponseCreation AS VARCHAR(MAX) = 'Puede iniciar sesión con su nombre de usuario o correo electrónico para gestionar los detalles de la cita.'\n" +
                             $"DECLARE @uPassword AS VARBINARY(64) = (SELECT HASHBYTES('SHA2_256',Cast(@uDocument AS VARCHAR(8000))))";

            string QUERY = $"{DECLARE}\nBEGIN TRY\n";

            if (Item.aID is null || Item.aID == 0)
            {
                //CREAR
                QUERY += $"    DECLARE @id AS INT = (SELECT [uID] FROM Users WHERE uDocument = @uDocument)\n" +
                         $"    IF EXISTS(SELECT TOP(1)* FROM Appointments AS A INNER JOIN Users AS U ON U.uID = A.uID WHERE A.uID = @id AND aDate = @aDate)\n" +
                         $"    BEGIN \n" +
                         $"        SELECT '0' AS OutputCodeError, 'La cita ya se encuentra en nuestro sistema. Para gestionar sus citas puede iniciar sesión o cumuníquese con el administrador' AS OutputMessageError\n" +
                         $"    END\n" +
                         $"    ELSE\n" +
                         $"    BEGIN\n" +
                         $"        IF NOT EXISTS(SELECT TOP(1)* FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"        BEGIN\n" +
                         $"            --NO EXISTE LO CREAMOS\n" +
                         $"            INSERT INTO Users(utID,uName,uLastName,uCellphone,uLoginName,uPassword,dtID,uDocument,[oID],gID,uBirthDate)\n" +
                         $"            VALUES(3,@uName,@uLastName,@uCellphone,@uDocument,@uPassword,@dtID,@uDocument,@oID,@gID,@uBirthDate)\n" +
                         $"            SET @ResponseCreation = CONCAT(\n" +
                         $"                'Se ha registrado correctamente, para ver el detalle de las citas puede iniciar sesión con el usuario '\n" +
                         $"                ,@uDocument\n" +
                         $"                ,'\". Su contraseña es el documento de identidad')\n" +
                         $"            SET @uID = (SELECT TOP(1)[uID] FROM Users WHERE [uDocument] = @uDocument)\n" +
                         $"        END\n" +
                         $"        SET @uID = ISNULL(@uID,(SELECT TOP(1)[uID] FROM Users WHERE [uDocument] = @uDocument))\n" +
                         $"        --SE CREA LA CITA\n" +
                         $"        INSERT INTO Appointments([oID],[uID],[dID],aDate,aTime,aDescription)\n" +
                         $"        VALUES(@oID,@uID,@dID,@aDate,@aTime,@aDescription)\n" +
                         $"        SELECT '0' AS OutputCodeError, CONCAT('La cita se ha crado. ',@ResponseCreation) AS OutputMessageError\n" +
                         $"    END\n";
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