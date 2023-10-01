using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Reflection;

namespace Repository
{
    public static class Mapper
    {
        public static List<T> GetListFromDataTable<T>(DataTable? dt)
        {
            List<T> data = new();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }

            }
            return data;
        }

        public static T? GetObjectFromDataTable<T>(DataTable? dt)
        {
            try
            {
                T item;
                if (dt != null)
                {
                    item = GetItem<T>(dt.Rows[0]);
                    return item;
                }
                else
                {
                    return default;
                }
            }

            catch
            {
                return default;
            }

        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("");
                        }

                    }



                    else
                        continue;
                }
            }
            return obj;
        }

        public static IEnumerable<T> GetObjectFromDataReader<T>(IDataReader DrObject)
        {
            int index = 0;
            while (DrObject.Read())
            {
                yield return (T)DrObject.GetValue(index);
            }
        }

        public static GenericResponseModel SetResponseAttributes(ResponseDB ItemResponseDB)
        {

            ItemResponseDB ??= (new());
            ItemResponseDB.ListOutputParameters ??= (new());
            GenericResponseModel GenericResponseDomain = new();
            try
            {
                bool Status = true;
                string CodeStatus = "200";

                if (ItemResponseDB != null && ItemResponseDB.ListOutputParameters != null)
                {
                    if (ItemResponseDB.ListOutputParameters.Find(x => x.Name.Equals("outputCodeError")) != null)
                    {
                        CodeStatus = ItemResponseDB.ListOutputParameters.Find(x => x.Name.Equals("outputCodeError", StringComparison.Ordinal)).Value;
                        Status = ItemResponseDB.ListOutputParameters.Find(x => x.Name.Equals("outputCodeError", StringComparison.Ordinal)).Value.Equals("0");
                    }
                }
                GenericResponseDomain.Status = Status;
                GenericResponseDomain.CodeStatus = CodeStatus;
                GenericResponseDomain.MessageStatus = ItemResponseDB.ListOutputParameters.Find(x => x.Name.Equals("outputMessageError", StringComparison.Ordinal)).Value;
                GenericResponseDomain.DateResponse = DateTime.UtcNow;
            }
            catch (Exception Ex)
            {
                GenericResponseDomain.Status = false;
                GenericResponseDomain.CodeStatus = "500";
                GenericResponseDomain.MessageStatus = Ex.Message;
                GenericResponseDomain.DateResponse = DateTime.UtcNow;
            }
            return GenericResponseDomain;
        }

        public static List<SelectListItem> ToSelectList(DataTable table, string value, string text)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        list.Add(new SelectListItem()
                        {
                            Text = row[text].ToString(),
                            Value = row[value].ToString()
                        });
                    }
                }
            }
            return list;
        }

    }
}