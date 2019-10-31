using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GineCore.Common
{
    public static class StringExtend
    {
        public static List<int> ToIntList(this string str, char splitChar = ',')
        {
            List<int> list = str.Split(splitChar).Select(u => int.Parse(u)).ToList();
            return list;
        }
    }
}
