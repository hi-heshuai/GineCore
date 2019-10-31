using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace GineCore.Common.Orc
{
    /// <summary>
    /// orc 图片识别帮助类
    /// </summary>
    public class OrcHelper
    {
        private static readonly string baseUrl = "http://192.168.30.135:9101";

        /// <summary>
        /// 请求一次OCR识别任务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        public static OCRResultModel OCRFileProcess(string picUrl)
        {
            HttpClient client = new HttpClient();
            try
            {
                string reqUrl = baseUrl + "/ocr/toOCR";
                String ocrFile = picUrl;
                var picBites = File.ReadAllBytes(ocrFile);
                MultipartFormDataContent content = new MultipartFormDataContent();

                // 设置请求参数
                content.Add(new StringContent("png"), "ext");
                content.Add(new StringContent("DIR"), "inputType");
                content.Add(new StringContent("TXT"), "outputType");
                content.Add(new ByteArrayContent(picBites), "attachment");

                //批量添加
                //foreach (var file in ocrFiles)
                //{
                //content.Add(new ByteArrayContent(File.ReadAllBytes(file)), "attachment");
                //}

                HttpResponseMessage result = client.PostAsync(reqUrl, content).Result;
                if (result.IsSuccessStatusCode)
                {
                    string rslt = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<OCRResultModel>(rslt);
                }
                else
                {
                    throw new Exception("请求OCR失败");
                }
            }
            catch (Exception e)
            {
                throw new Exception("接口错误");
            }
            finally
            {
                if (client != null)
                    client.Dispose();
            }
        }

        /// <summary>
        /// OCR状态查看接口
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static OCRStatusModel CheckOCRStatus(string processId)
        {
            HttpClient client = new HttpClient();
            try
            {
                string reqUrl = string.Format("{0}/ocr/checkOCRStatus?processId={1}", baseUrl, processId);

                HttpResponseMessage result = client.GetAsync(reqUrl).Result;
                if (result.IsSuccessStatusCode)
                {
                    string rslt = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<OCRStatusModel>(rslt);
                }
                else
                {
                    throw new Exception("请求OCR失败");
                }
            }
            catch (Exception e)
            {
                return new OCRStatusModel();
            }
            finally
            {
                if (client != null)
                    client.Dispose();
            }
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static string GetOCRResult(string processId)
        {
            HttpClient client = new HttpClient();
            try
            {
                string reqUrl = string.Format("{0}/ocr/retriveResultForText?processId={1}", baseUrl, processId);

                HttpResponseMessage result = client.GetAsync(reqUrl).Result;
                if (result.IsSuccessStatusCode)
                {
                    string resultStr = result.Content.ReadAsStringAsync().Result;
                    return resultStr;
                }
                else
                {
                    throw new Exception("请求OCR失败");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (client != null)
                    client.Dispose();
            }
        }
    }
}
