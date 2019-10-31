using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Common
{
    public class ClearStringHtmlHelper
    {
        /// <summary>
        /// 清除html
        /// </summary>
        /// <param name="sourceTxt"></param>
        /// <returns></returns>
        public static string ClearHtml(string sourceTxt)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(sourceTxt, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");
            return strText;
        }
    }
}
