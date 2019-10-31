using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Service.MenusService
{
    public class MenusTypeEnumHelper
    {
        public enum Type
        {
            [Description("添加")]
            Add,

            [Description("修改")]
            Update,

            [Description("删除")]
            Delete,

            [Description("查询")]
            Search,

            [Description("导出excel")]
            ExpertExcel
        }

        /// <summary>
        /// 获取枚举信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDictionary()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var value in Enum.GetValues(typeof(Type)))
            {
                object[] objAttrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objAttrs != null && objAttrs.Length > 0)
                {
                    DescriptionAttribute descAttr = objAttrs[0] as DescriptionAttribute;
                    keyValuePairs.Add(value.ToString(), descAttr.Description);
                }
            }

            return keyValuePairs;
        }
    }
}
