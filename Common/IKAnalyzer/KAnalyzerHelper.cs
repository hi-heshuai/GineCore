using IKAnalyzerNet;
using Lucene.Net.Analysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Common
{
    /// <summary>
    /// 分词
    /// </summary>
    public class KAnalyzerHelper
    {
        public static List<string> Work(string str)
        {
            List<string> result = new List<string>();

            IKAnalyzer ika = new IKAnalyzer();
            System.IO.TextReader r = new System.IO.StringReader(str);
            TokenStream ts = ika.TokenStream("TestField", r);
            for (Token t = ts.Next(); t != null; t = ts.Next())
            {
                result.Add(t.TermText());
            }

            return result;
        }
    }
}
