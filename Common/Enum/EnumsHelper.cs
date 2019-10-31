using GineCore.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace GineCore.Common
{
    public class EnumsHelper
    {
        /// <summary>
        /// 获取枚举信息
        /// </summary>
        /// <returns></returns>
        public static List<KeyValueModel> GetEnumDictionary<T>()
        {
            List<KeyValueModel> keyValuePairs = new List<KeyValueModel>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                object[] objAttrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objAttrs != null && objAttrs.Length > 0)
                {
                    DescriptionAttribute descAttr = objAttrs[0] as DescriptionAttribute;
                    keyValuePairs.Add(new KeyValueModel()
                    {
                        Key = value.ToString(),
                        Value = descAttr.Description
                    });
                }
            }

            return keyValuePairs;
        }

        /// <summary>
        /// 通过枚举字符串获取描述
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public static string GetDescription(Type type, string keyStr)
         {
            var theEnum = Enum.Parse(type, keyStr);
            return GetEnumDescription((Enum)theEnum);
        }

        public static string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }
}
