using System;
using System.Collections.Generic;
using System.Text;

namespace GineCore.Common
{
    /// <summary>
    /// 系统错误日志文件
    /// </summary>
    public class LogFileHelper
    {
        //private string _fileName;
        private static Dictionary<long, long> lockDic = new Dictionary<long, long>();
       
        /// <summary>  
        /// 创建文件  
        /// </summary>  
        /// <param name="fileName"></param>  
        private static void Create(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(fileName))
                {
                    fs.Close();
                }
            }
        }

        private static string GetFileName()
        {
            string fileName = "log/" + DateTime.Now.ToString("yyyyMMddhhmmss") + 
                "_" + Guid.NewGuid().ToString().Substring(0, 5) + ".txt";
            return fileName;
        }

        /// <summary>  
        /// 写入文本  
        /// </summary>  
        /// <param name="content">文本内容</param>  
        private static void Write(string content, string newLine)
        {

            var fileName = GetFileName();
            Create(fileName);

            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("FileName不能为空！");
            }
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite,
                    System.IO.FileShare.ReadWrite, 8, System.IO.FileOptions.Asynchronous))
            { 
                Byte[] dataArray = System.Text.Encoding.Default.GetBytes(content + newLine);
                bool flag = true;
                long slen = dataArray.Length;
                long len = 0;
                while (flag)
                {
                    try
                    {
                        if (len >= fs.Length)
                        {
                            fs.Lock(len, slen);
                            lockDic[len] = slen;
                            flag = false;
                        }
                        else
                        {
                            len = fs.Length;
                        }
                    }
                    catch (Exception ex)
                    {
                        while (!lockDic.ContainsKey(len))
                        {
                            len += lockDic[len];
                        }
                    }
                }
                fs.Seek(len, System.IO.SeekOrigin.Begin);
                fs.Write(dataArray, 0, dataArray.Length);
                fs.Close();
            }
        }

        /// <summary>  
        /// 写入文件内容  
        /// </summary>  
        /// <param name="content"></param>  
        public static void WriteLine(string content)
        {
            Write(content, System.Environment.NewLine);
        }
        /// <summary>  
        /// 写入文件  
        /// </summary>  
        /// <param name="content"></param>  
        public static void Write(string content)
        {
            Write(content, "");
        }
    }
}
