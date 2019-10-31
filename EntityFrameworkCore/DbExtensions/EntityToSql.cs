using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.EntityFrameworkCore.DbExtensions
{
    /// <summary>
    /// 实体sql语句拼接  sqlserver 
    /// </summary>
    public class EntityToSql
    {
        /// <summary>
        /// select语句模板(获取单个实体)
        /// </summary>
        public static string SelectSql<T>(int id) where T : class
        {
            var tabelName = typeof(T).Name;
            return string.Format("select * from {0} where {0}.Id = {1}", tabelName, id);
        }

        /// <summary>
        /// select语句模板(获取多个实体)
        /// </summary>
        public static string SelectSql<T>(string where = null)
        {
            var tabelName = typeof(T).Name;
            return string.Format("select * from {0} where {1}", tabelName, string.IsNullOrEmpty(where) ? "1=1" : where);
        }

        public static string SelectSql<T>(int top, string where = null)
        {
            var tabelName = typeof(T).Name;
            return string.Format("select top {0} * from {1} where {2}", top, tabelName, string.IsNullOrEmpty(where) ? "1=1" : where);
        }

        /// <summary>
        /// Add语句
        /// </summary>
        public static string AddSql<T>(T entity, bool isContainsKey = false) where T : class
        {
            var insertTemplate = "insert into {0} ({1})values({2})";
            var tabelName = typeof(T).Name;
            var fields = string.Empty;
            var setting = string.Empty;
            var propertys = entity.GetType().GetProperties();
            var dataBaseType = DataBaseType<T>();

            foreach (var p in propertys)
            {
                var value = p.GetValue(entity, null);
                if (value != null)
                {
                    value = value.ToString().Replace("'", "‘");
                }
                var str = p.PropertyType.ToString().ToLower();
                if (!isContainsKey)
                {
                    if (p.Name.ToLower() == "id" && (p.PropertyType.ToString().ToLower().Contains("system.int")
                        || p.PropertyType.ToString().ToLower().Contains("system.decimal")) && int.Parse(value.ToString()) <= 0) continue;
                }
                if (p.PropertyType.ToString().ToLower().Contains("system.datetime"))
                {
                    value = Convert.ToDateTime(value).ToString("yyyy-MM-dd hh:mm:ss");
                }

                switch (dataBaseType)
                {
                    case "sqlserver":
                        fields += string.Format("[{0}], ", p.Name);
                        break;
                    case "mysql":
                        fields += string.Format("`{0}`, ", p.Name);
                        break;
                    default:
                        fields += string.Format("{0}, ", p.Name);
                        break;
                }

                if (value == null)
                {
                    setting += string.Format("null, ");
                }
                else
                {
                    if (p.PropertyType.ToString().ToLower().Contains("system.boolean") ||
                        p.PropertyType.ToString().ToLower().Contains("system.boolean?"))
                    {
                        setting += string.Format("{0}, ", value);
                    }
                    else
                    {
                        setting += string.Format("'{0}', ", value);
                    }
                }
            }

            if (!string.IsNullOrEmpty(fields) && fields.Length >= 2)
            {
                fields = fields.Substring(0, fields.Length - 2);
                setting = setting.Substring(0, setting.Length - 2);
            }

            //"SELECT @@IDENTITY AS 'Id'"会在插入后返回插入的自增id
            return string.Format(insertTemplate, tabelName, fields, setting) + "; SELECT @@IDENTITY AS 'Id'";
        }

        /// <summary>
        /// update语句
        /// </summary>
        public static string UpdateSql<T>(T entity, params string[] fileds) where T : class
        {
            var updateTemplate = "update {0} {1} {2}";
            var tabelName = typeof(T).Name;

            var propertys = entity.GetType().GetProperties();
            var setting = "set ";
            var id = 0;
            var dataBaseType = DataBaseType<T>();

            foreach (var p in propertys)
            {
                if (p.Name.ToLower() == "id")
                {
                    id = Convert.ToInt32(p.GetValue(entity, null));
                    continue;
                }
                else if (fileds != null && fileds.Length != 0)
                {
                    if (!fileds.Contains(p.Name))
                        continue;
                }

                var value = p.GetValue(entity, null);

                if (p.PropertyType.ToString().ToLower().Contains("system.datetime"))
                {
                    value = ((DateTime)(value)).ToString("yyyy-MM-dd hh:mm:ss");
                }

                if (value == null)
                {
                    switch (dataBaseType)
                    {
                        case "sqlserver":
                            setting += string.Format("[{0}]={1}, ", p.Name, "null");
                            break;
                        case "mysql":
                            setting += string.Format("`{0}`={1}, ", p.Name, "null");
                            break;
                        default:
                            setting += string.Format("{0}={1}, ", p.Name, "null");
                            break;
                    }
                }
                else
                {
                    switch (dataBaseType)
                    {
                        case "sqlserver":
                            setting += string.Format("[{0}]='{1}', ", p.Name, value);
                            break;
                        case "mysql":
                            setting += string.Format("`{0}`='{1}', ", p.Name, value);
                            break;
                        default:
                            setting += string.Format("{0}='{1}', ", p.Name, value);
                            break;
                    }
                }
                    
            }
            setting = setting.Substring(0, setting.Length - 2);

            return string.Format(updateTemplate, tabelName, setting, " where id=" + id);
        }

        /// <summary>
        /// delete语句模板
        /// </summary>
        public static string DeleteTemplate<T>(T entity) where T : class
        {
            var tabelName = typeof(T).Name;

            var propertys = entity.GetType().GetProperties();
            var id = 0;

            foreach (var p in propertys)
            {
                if (p.Name.ToLower() == "id")
                {
                    id = Convert.ToInt32(p.GetValue(entity, null));
                    break;
                }
            }
            return string.Format("delete from {0} where Id = {1}", tabelName, id);
        }

        public static string Delete(string tableName, string key, string keyvalue)
        {
            return string.Format("delete from {0} where {0} = '{1}'", tableName, key, keyvalue);
        }

        public static string GetCount<T>(string where = "") where T : class
        {
            var tabelName = typeof(T).Name;

            if (string.IsNullOrEmpty(where))
            {
                return string.Format("select count(*) from {0}", tabelName);
            }

            return string.Format("select count(*) from {0} where {0}", tabelName, where);
        }

        /// <summary>
        /// 获取链接数据库类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DataBaseType<T>()
        {
            string type = "sqlserver";
            var assembly = typeof(T).Assembly.GetName().Name;

            switch (assembly)
            {
                case "GineCore.Entity":
                    type = "mysql";
                    break;
            }
            return type;
        }
    }
}
