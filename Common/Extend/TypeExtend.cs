using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Common.Extend
{
    public static class TypeExtend
    {
        /// <summary>
        /// 判断是否是同一类型
        /// </summary>
        public static bool GenericEq(this Type type, Type toCompare)
        {
            return type.Namespace == toCompare.Namespace && type.Name == toCompare.Name;
        }
    }
}
