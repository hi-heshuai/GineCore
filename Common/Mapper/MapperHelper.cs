using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using Unity.Interception.Utilities;

namespace GineCore.Common
{

    public static class MapperHelper
    {
        /// <summary>
        /// 将TFrom模型属性映射到TTo模型
        /// </summary>
        public static TTo Mapper<TFrom, TTo>(TFrom from) where TFrom : class
        {
            var mapConfig = new DefaultMapConfig();
            return Mapper<TFrom, TTo>(from, mapConfig);
        }

        /// <summary>
        /// 用自定义映射规则进行模型映射
        /// </summary>
        /// <param name="from"></param>
        /// <param name="config">映射规则</param>
        public static TTo Mapper<TFrom, TTo>(TFrom from, IMappingConfigurator mapConfig) where TFrom : class
        {
            var ignores = new List<string>();

            //初始化忽略映射的属性
            typeof(TFrom).GetProperties().ForEach(p =>
            {
                if (p.GetCustomAttributes(typeof(IgnoreMapperAttribute), false).Length > 0)
                    ignores.Add(p.Name);
            });
            typeof(TTo).GetProperties().ForEach(p =>
            {
                if (p.GetCustomAttributes(typeof(IgnoreMapperAttribute), false).Length > 0)
                    ignores.Add(p.Name);
            });

            if (mapConfig != null && mapConfig is MapConfigBase<TFrom>)
                (mapConfig as MapConfigBase<TFrom>).IgnoreMembers<TFrom, TTo>(ignores.ToArray());
            ObjectsMapper<TFrom, TTo> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(mapConfig);
            return mapper.Map(from);
        }

        /// <summary>
        /// 将TFrom模型属性映射到TTo模型
        /// </summary>
        public static TTo Mapper<TFrom, TTo>(TFrom from, TTo tto, IMappingConfigurator mapConfig) where TFrom : class
        {
            ObjectsMapper<TFrom, TTo> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(mapConfig);
            return mapper.Map(from, tto);
        }

        /// <summary>
        /// 将TFrom模型属性映射到TTo模型，未映射字段属性不变
        /// </summary>
        public static TTo Mapper<TFrom, TTo>(TFrom from, TTo tto) where TFrom : class
        {
            var mapConfig = new DefaultMapConfig();
            var ignores = new List<string>();

            //初始化忽略映射的属性
            typeof(TFrom).GetProperties().ForEach(p =>
            {
                if (p.GetCustomAttributes(typeof(IgnoreMapperAttribute), false).Length > 0)
                    ignores.Add(p.Name);
            });
            typeof(TTo).GetProperties().ForEach(p =>
            {
                if (p.GetCustomAttributes(typeof(IgnoreMapperAttribute), false).Length > 0)
                    ignores.Add(p.Name);
            });

            mapConfig.IgnoreMembers<TFrom, TTo>(ignores.ToArray());
            ObjectsMapper<TFrom, TTo> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(mapConfig);
            return mapper.Map(from, tto);
        }

        /// <summary>
        /// 将TFrom模型属性映射到TTo模型
        /// </summary>
        public static TTo Mapper<TFrom, TTo>(TFrom from, TTo tto, string[] fields, string[] ignores) where TFrom : class
        {
            var mapConfig = new DefaultMapConfig();

            if (fields != null)
            {
                mapConfig.MatchMembers((f, t) => fields.Contains(f));
            }
            if (ignores != null)
            {
                mapConfig.IgnoreMembers<TFrom, TTo>(ignores.ToArray());
            }

            return Mapper(from, tto, mapConfig);
        }

        /// <summary>
        /// 将datatable映射为list集合
        /// </summary>
        public static List<TModel> Mapper<TModel>(DataTable dt) where TModel : class, new()
        {
            var list = new List<TModel>();
            var propertys = typeof(TModel).GetProperties();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var model = new TModel();
                for (int j = 0; j < propertys.Count(); j++)
                {
                    string propertysName = propertys[j].Name;//columnName
                    if (dt.Columns.Contains(propertysName))
                    {
                        var modelProrperty = model.GetType().GetProperty(propertysName);
                        modelProrperty.SetValue(model, ChangeType(dt.Rows[i][propertysName], modelProrperty.PropertyType), null);
                    }
                }
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }

            //转换DbNull
            if (value == null || (value as DBNull) != null || (value as Nullable) != null)
            {
                return null;
            }
            //转换Nullable<>
            else if (conversionType.IsGenericType &&
              conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(value, conversionType);
        }
    }

    public class IgnoreMapperAttribute : System.Attribute
    {
    }

}
