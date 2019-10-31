using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common.Utilities
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举值的描述（注解）。
        /// </summary>
        /// <returns></returns>
        public static string GetEnumDescription(Enum enumValue)
        {
            var str = enumValue.ToString();
            var field = enumValue.GetType().GetField(str);
            var objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;

            var da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        /// <summary>
        /// 检查字符串是否匹配某个枚举值
        /// </summary>
        /// <param name="p">枚举类型</param>
        /// <param name="enumStr">枚举值字符串</param>
        /// <returns>如果找到匹配值，否者返回null</returns>
        public static bool IsEnum(Type p, string enumStr)
        {
            var e = GetEnum(p, enumStr);

            return e != null;
        }

        /// <summary>
        /// 检查字符串是否匹配某个枚举值
        /// </summary>
        /// <param name="p">枚举类型</param>
        /// <param name="enumStr">枚举值字符串</param>
        /// <returns>如果找到匹配值，否者返回null</returns>
        public static bool IsEnum<T>(string enumStr)
        {
            var e = GetEnum<T>(enumStr);

            return e != null;
        }

        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <param name="p"></param>
        /// <param name="enumStr"></param>
        /// <returns></returns>
        public static object GetEnum(Type p, string enumStr)
        {
            foreach (var myCode in Enum.GetValues(p))
            {
                var strName = Enum.GetName(p, myCode);//获取名称
                if (!string.IsNullOrEmpty(strName) && strName == enumStr) return Enum.Parse(p, strName);
            }

            return null;
        }

        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <param name="p"></param>
        /// <param name="enumStr"></param>
        /// <returns></returns>
        public static T GetEnum<T>(string enumStr)
        {
            var e = GetEnum(typeof(T), enumStr);
            if (e != null)
                return (T)GetEnum(typeof(T), enumStr);
            return default(T);
        }

        /// <summary>
        /// 根据注解获取枚举
        /// </summary>
        /// <returns></returns>
        public static T GetEnumByDes<T>(string description)
        {
            var enums = GetList<T>();
            foreach (var e in enums)
            {
                if (GetEnumDescription(e as Enum) == description)
                    return e;
            }

            return default(T);
        }

        /// <summary>
        /// 获取枚举所有项列表
        /// </summary>
        /// <returns></returns>
        public static List<T> GetList<T>()
        {
            List<T> list = new List<T>();

            foreach (string str in Enum.GetNames(typeof(T)))
            {
                list.Add(GetEnum<T>(str));
            }

            return list;
        }
    }
}
