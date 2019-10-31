using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace GineCore.EntityFrameworkCore.DbExtensions
{
    public class DataTableToEntityHelper
    {
        /// <summary>
        /// 将Datatable转换成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToEntity<T>(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    T entity = GetEntity<T>(dt, row);
                    return entity;
                }
                return default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将Datatable转换成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToEntityList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            try
            {
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        T entity = GetEntity<T>(dt, row);
                        list.Add(entity);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return new List<T>();
                throw ex;
            }
        }

        /// <summary>
        /// datatable to entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private static T GetEntity<T>(DataTable dt, DataRow row)
        {
            T entity = Activator.CreateInstance<T>();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (dt.Columns.Contains(info.Name) && info.CanWrite)
                {
                    object objValue = row[info.Name];
                    if (objValue != DBNull.Value)
                    {
                        if (info.PropertyType == typeof(DateTime?) || info.PropertyType == typeof(DateTime))
                        {
                            DateTime date = DateTime.MaxValue;
                            DateTime.TryParse(objValue.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
                            info.SetValue(entity, date, null);
                        }
                        else if (info.PropertyType == typeof(decimal?) || info.PropertyType == typeof(decimal))
                        {
                            decimal de = 0m;
                            decimal.TryParse(objValue.ToString(), out de);
                            info.SetValue(entity, de, null);
                        }
                        else if (info.PropertyType == typeof(bool?) || info.PropertyType == typeof(bool)
                            || info.PropertyType == typeof(Boolean) || info.PropertyType == typeof(Boolean?))
                        {
                            bool de = false;
                            if(!string.IsNullOrEmpty(objValue.ToString()) 
                                && objValue.ToString() != "0"
                                && objValue.ToString().ToLower() != "false")
                            {
                                de = true;
                            }
                            //bool.TryParse(objValue.ToString(), out de);
                            info.SetValue(entity, de, null);
                        }
                        else if (info.PropertyType == typeof(string))
                        {
                            info.SetValue(entity, objValue.ToString(), null);
                        }
                        else
                        {
                            info.SetValue(entity, objValue, null);
                        }
                    }
                    else
                    {
                        if (info.DeclaringType == typeof(string))
                        {
                            info.SetValue(entity, "", null);
                        }
                        else
                        {
                            info.SetValue(entity, null, null);
                        }
                    }
                }
            }
            return entity;
        }
    }
}
