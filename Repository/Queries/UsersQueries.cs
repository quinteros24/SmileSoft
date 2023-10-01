using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Queries
{
    public class UsersQueries
    {
        public static string ChangePassword(ChangePasswordModelRequest Item)
        {
            //SI EL USUARIO NO SE ENCUENTRA EL MENSAJE SIGUE SIENDO EL MISMO
            return $"BEGIN TRY\n" +
                    $"    UPDATE dbo.Users SET uPassword = HASHBYTES('SHA2_256',Cast('{Item.Password}' AS VARCHAR(8000)))\n" +
                    $"    WHERE [uID] = {Item.UID} \n" +
                    $"    SELECT '0' AS OutputCodeError, 'Se ha cambiado la contraseña.' AS OutputMessageError\n" +
                    $"END TRY\n" +
                    $"BEGIN CATCH\n" +
                    $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
                    $"END CATCH";
        }
        public static string ViewUsers(int utID)
        {
            return $"BEGIN TRY\n " +
                $"  SELECT *, 'OBJECT' AS TableName FROM dbo.Users WHERE [utID] = {utID}\n" +
                $"    SELECT '0' AS OutputCodeError, 'Consulta correcta' AS OutputMessageError, 'Parameters' AS TableName\n" +
                $"END TRY\n" +
                $"BEGIN CATCH\n" +
                $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError, 'Parameters' AS TableName\n" +
                $"END CATCH";
        }

        public static string GetUserDetails(int uID)
        {
            return $"BEGIN TRY\n " +
                $"  SELECT *, 'OBJECT' AS TableName FROM dbo.Users AS U LEFT JOIN dbo.Doctors AS D ON U.uID = D.uID WHERE U.[uID] = {uID}\n" +
                $"    SELECT '0' AS OutputCodeError, 'Consulta correcta' AS OutputMessageError, 'Parameters' AS TableName\n" +
                $"END TRY\n" +
                $"BEGIN CATCH\n" +
                $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError, 'Parameters' AS TableName\n" +
                $"END CATCH";
        }
       
        public static string CreateUpdateUsers(ViewUsersModelRequest Item)
        {
            string fecha = Item.uBirthDate.Value.Year.ToString() + "-" + Item.uBirthDate.Value.Month.ToString("00") + "-" + Item.uBirthDate.Value.Day.ToString("00");

            string query = $"DECLARE @aux AS TABLE([uID] INT)\n" +
                             $"DECLARE @Response AS VARCHAR(MAX) = 'Usuario '\n" +
                             $"BEGIN TRY\n";

            if (Item.uID == 0)
            {
                // Crear un nuevo usuario
                query += $"    INSERT INTO dbo.Users (utID, uName, uLastName, uCellphone, uAddress, uLoginName, uEmailAddress, uPassword, dtID, uDocument, uStatus, oID, gID, uBirthDate)\n" +
                        $"    OUTPUT inserted.uID INTO @aux\n" +
                        $"    VALUES ({Item.utID}, '{Item.uName}', '{Item.uLastName}', '{Item.uCellphone}', '{Item.uAddress}', '{Item.uLoginName}', \n" +
                        $"    '{Item.uEmailAddress}', HASHBYTES('SHA2_256', CAST('{Item.uPassword}' AS VARCHAR(8000))), {Item.dtID}, '{Item.uDocument}', {(Item.uStatus? 1:0)},{Item.oID},{Item.gID},'{fecha}');\n" +
                        $"    SET @Response = CONCAT(@Response,'registrado con éxito.')\n";

            }
            else if (Item.uID != 0)
            {
                // Actualizar un usuario existente
                query += $"    UPDATE dbo.Users SET ";

                // Verificar si se ha proporcionado un valor para cada campo y actualizar solo los que han cambiado
                if (!string.IsNullOrEmpty(Item.uName))
                    query += $"uName = '{Item.uName}', ";

                if (!string.IsNullOrEmpty(Item.uLastName))
                    query += $"uLastName = '{Item.uLastName}', ";

                if (!string.IsNullOrEmpty(Item.uCellphone))
                    query += $"uCellphone = '{Item.uCellphone}', ";

                if (!string.IsNullOrEmpty(Item.uAddress))
                    query += $"uAddress = '{Item.uAddress}', ";

                if (!string.IsNullOrEmpty(Item.uLoginName))
                    query += $"uLoginName = '{Item.uLoginName}', ";

                if (!string.IsNullOrEmpty(Item.uEmailAddress))
                    query += $"uEmailAddress = '{Item.uEmailAddress}', ";

                if (!string.IsNullOrEmpty(Item.uPassword))
                    query += $"uPassword = HASHBYTES('SHA2_256', CAST('{Item.uPassword}' AS VARCHAR(8000))), ";

                if (Item.dtID != 0)
                    query += $"dtID = {Item.dtID}, ";

                if (!string.IsNullOrEmpty(Item.uDocument))
                    query += $"uDocument = '{Item.uDocument}', ";

                //if (Item.uStatus != true)
                //    query += $"uStatus = {Item.uStatus}, ";

                if (Item.oID != 0)
                    query += $"oID = {Item.oID}, ";

                if (Item.gID != 0)
                    query += $"gID = {Item.gID}, ";

                if (!string.IsNullOrEmpty(fecha))
                    query += $"uBirthDate = '{fecha}', ";

                // Eliminar la última coma y agregar la condición WHERE
                query = query.TrimEnd(',', ' ') + $"\n    WHERE [uID] = {Item.uID};\n" +
                        $"    INSERT INTO @aux([uID])VALUES({Item.uID})\n" + 
                        $"    SET @Response = CONCAT(@Response,'actualizado con éxito.')\n";

            }

            if(Item.utID == 2)
            {
                query += $"    IF NOT EXISTS(SELECT TOP(1)* FROM Doctors WHERE [uID] = (SELECT [uID] FROM @aux))\n" +
                         $"    BEGIN\n" +
                         $"        INSERT INTO Doctors([uID],utID,dAcademicLevel,dDegree,dUniversityName,dSpeciality,dProfessionalCard)\n" +
                         $"        VALUES((SELECT [uID] FROM @aux),2,'{Item.dAcademicLevel}','{Item.dDegree}','{Item.dUniversityName}','{Item.dSpeciality}','{Item.dProfessionalCard}')\n" +
                         $"    END\n" +
                         $"    ELSE\n" +
                         $"    BEGIN\n" +
                         $"        UPDATE Doctors\n" +
                         $"        SET\n" +
                         $"            dAcademicLevel = '{Item.dAcademicLevel}'\n" +
                         $"            ,dDegree = '{Item.dDegree}'\n" +
                         $"            ,dUniversityName = '{Item.dUniversityName}'\n" +
                         $"            ,dSpeciality = '{Item.dSpeciality}'\n" +
                         $"            ,dProfessionalCard = '{Item.dProfessionalCard}'\n" +
                         $"        WHERE [uID] = (SELECT [uID] FROM @aux)\n" +
                         $"    END\n";
            }

            query +=     $"    SELECT '0' AS OutputCodeError, @Response AS OutputMessageError\n" +
                         $"END TRY\n" +
                         $"BEGIN CATCH\n" +
                         $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError, 'Parameters' AS TableName\n" +
                         $"END CATCH";

            return query;
        }

        public static string SetUserStatus(int uID, int uStatus)
        {
            string status = uStatus == 1 ? "activado" : "desactivado";
            return $"IF EXISTS(SELECT uName FROM dbo.Users WHERE uID = {uID})\n" +
                   $"BEGIN\n" +
                   $"    DECLARE @name AS VARCHAR(100) = (SELECT uName FROM dbo.Users WHERE uID = {uID})\n" +
                   $"    UPDATE dbo.Users SET uStatus = {uStatus} WHERE uID = {uID}\n" +
                   $"    SELECT '0' AS OutputCodeError, 'El usuario @name se ha {status}' AS OutputMessageError\n" +
                   $"END\n" +
                   $"ELSE\n" +
                   $"    SELECT '-1' AS OutputCodeError, 'El usuario no se ha encontrado' AS OutputMessageError\n";
        }
    }
}
