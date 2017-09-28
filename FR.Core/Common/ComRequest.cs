using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FR.Core.Common
{
    public static class ComRequest
    {
        /// <summary>
        /// Get方式请求接口
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string GetApi(string Url)
        {
            if (Url.Contains("?"))
                Url += "&";
            else
                Url += "?";

            Url += string.Format("Key={0}&CPKey={1}", Base.Key, Base.CPKey);

            return GetWeb(Url, Encoding.UTF8);
        }

        /// <summary>
        /// Post方式请求接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Url"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static string PostApi<T>(string Url, T Data)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("Key", Base.Key);

            parameters.Add("CPKey", Base.CPKey);

            parameters.Add("Data", Data.JsonSerialize());

            return PostWeb(Url, parameters);
        }

        /// <summary>
        /// Http Get 同步方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetWeb(string url, Encoding encoding = null)
        {
            HttpClient httpClient = new HttpClient();
            var t = httpClient.GetByteArrayAsync(url);
            t.Wait();
            var ret = encoding.GetString(t.Result);
            return ret;
        }

        public static string PostWeb(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new StreamContent(ms);


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
            hc.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }

        /// <summary>
        /// 组装QueryString的方法
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();

            var i = 0;

            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// 填充表单信息的Stream
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="stream"></param>
        public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);

            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);

            stream.Write(formDataBytes, 0, formDataBytes.Length);

            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }
    }
}
