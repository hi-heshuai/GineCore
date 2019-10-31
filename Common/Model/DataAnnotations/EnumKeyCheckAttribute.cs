using GineCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common
{
    /// <summary>
    /// 枚举键检测
    /// </summary>
    public class EnumKeyCheckAttribute : ValidationAttribute
    {

        public Type EnumType { get; set; }

        public override bool IsValid(object value)
        {
            if (EnumType == null)
            {
                throw new Exception(string.Format("枚举类型参数“EnumType”不能为空"));
            }

            if (!typeof(System.Enum).IsAssignableFrom(EnumType))
            {
                throw new Exception(string.Format("枚举类型参数“{0}”不是有效的枚举类型", EnumType.FullName));
            }

            var enumKey = EnumHelper.IsEnum(EnumType, value as string);

            if (!enumKey)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    base.ErrorMessage = string.Format("枚举值“{0}”不存在于枚举类型“{1}”中", value, EnumType.FullName);
                }
                return false;
            }

            return true ;
        }
    }
}
