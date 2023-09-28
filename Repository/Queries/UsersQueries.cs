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
        //public static string CreateUpdateUsers(ViewUsersModelRequest Item)
        //{
        //    string query =      $"BEGIN TRY\n" +
        //                        $"    IF NOT EXISTS (SELECT TOP(1)* FROM dbo.Users WHERE uLoginName = '{Item.uLoginName}' OR uEmailAddress = '{Item.uEmailAddress}' OR uDocument = '{Item.uDocument}')\n" +
        //                        $"    BEGIN \n" +
        //                        $"        INSERT INTO dbo.Users (utID, uName, uLastName, uCellphone, uAddress, uLoginName, uEmailAddress, uPassword, dtID, uDocument, uStatus)\n" +
        //                        $"       VALUES ({Item.utID}, '{Item.uName}', '{Item.uLastName}', '{Item.uCellphone}', '{Item.uAddress}', '{Item.uLoginName}', '{Item.uEmailAddress}',  HASHBYTES('SHA2_256',Cast('{Item.uPassword}' AS VARCHAR(8000))), {Item.dtID}, '{Item.uDocument}', {Item.uStatus});\n" +
        //                        $"        SELECT '0' AS OutputCodeError, 'Usuario registrado con exito.' AS OutputMessageError\n" +
        //                        $"    END\n" +
        //                        $"    ElSE\n" +
        //                        $"       SELECT '-1' AS OutputCodeError, 'Este usuario ya existe en la base de datos.' AS OutputMessageError\n" +
        //                        $"END TRY\n" +
        //                        $"BEGIN CATCH\n" +
        //                        $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
        //                        $"END CATCH";
        //    if(Item.uID != 0)
        //    {
        //        query = $"BEGIN TRY\n" +
        //            $"    IF EXISTS (SELECT * FROM dbo.Users WHERE [uID] = {Item.uID})\n" +
        //            $"    BEGIN\n" +
        //            $"        UPDATE dbo.Users\n" +
        //            $"        SET\n" +
        //            $"            utID = {Item.utID},\n" +
        //            $"            uName = '{Item.uName}',\n" +
        //            $"            uLastName = '{Item.uLastName}',\n" +
        //            $"            uCellphone = '{Item.uCellphone}',\n" +
        //            $"            uAddress = '{Item.uAddress}',\n" +
        //            $"            uLoginName = '{Item.uLoginName}',\n" +
        //            $"            uEmailAddress = '{Item.uEmailAddress}',\n" +
        //            $"            uPassword = HASHBYTES('SHA2_256', CAST('{Item.uPassword}' AS VARCHAR(8000))),\n" +
        //            $"            dtID = {Item.dtID},\n" +
        //            $"            uDocument = '{Item.uDocument}',\n" +
        //            $"            uStatus = {Item.uStatus}\n" +
        //            $"        WHERE [uID] = {Item.uID};\n" +
        //            $"        \n" +
        //            $"        SELECT '0' AS OutputCodeError, 'Usuario actualizado con éxito.' AS OutputMessageError\n" +
        //            $"    END\n" +
        //            $"    ELSE\n" +
        //            $"    BEGIN\n" +
        //            $"        SELECT '-1' AS OutputCodeError, 'El usuario no existe en la base de datos.' AS OutputMessageError\n" +
        //            $"    END\n" +
        //            $"END TRY\n" +
        //            $"BEGIN CATCH\n" +
        //            $"    SELECT ERROR_NUMBER() AS OutputCodeError, ERROR_MESSAGE() AS OutputMessageError\n" +
        //            $"END CATCH\n";

        //    }
        //    return query;
        //}
        public static string CreateUpdateUsers(ViewUsersModelRequest Item)
        {
            string query = string.Empty;

            if (Item.uID == 0)
            {
                // Crear un nuevo usuario
                query = $"INSERT INTO dbo.Users (utID, uName, uLastName, uCellphone, uAddress, uLoginName, uEmailAddress, uPassword, dtID, uDocument, uStatus, oID, gID, uBirthDate)\n" +
                        $"VALUES ({Item.utID}, '{Item.uName}', '{Item.uLastName}', '{Item.uCellphone}', '{Item.uAddress}', '{Item.uLoginName}', " +
                        $"'{Item.uEmailAddress}', HASHBYTES('SHA2_256', CAST('{Item.uPassword}' AS VARCHAR(8000))), {Item.dtID}, '{Item.uDocument}', {Item.uStatus},{Item.oID},{Item.gID},{Item.uBirthDate});\n" +
                        $"SELECT '0' AS OutputCodeError, 'Usuario registrado con éxito.' AS OutputMessageError\n";
            }
            else
            {
                // Actualizar un usuario existente
                query = $"UPDATE dbo.Users SET ";

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

                //if (Item.uStatus)
                //    query += $"uStatus = {Item.uStatus}, ";

                if (Item.oID != 0)
                    query += $"oID = {Item.oID}, ";

                if (Item.gID != 0)
                    query += $"gID = {Item.gID}, ";

                if (Item.uBirthDate != null)
                    query += $"uBirthDate = {Item.uBirthDate}, ";

                // Eliminar la última coma y agregar la condición WHERE
                query = query.TrimEnd(',', ' ') + $" WHERE [uID] = {Item.uID};\n" +
                        $"SELECT '0' AS OutputCodeError, 'Usuario actualizado con éxito.' AS OutputMessageError\n";
            }

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
