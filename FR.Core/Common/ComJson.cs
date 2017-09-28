using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FR.Core
{
    public static class ComJson
    {
        /// <summary>
        /// 将对象转为JSON字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object o, bool isCompress = true)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();

            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            if (isCompress)
                return JsonConvert.SerializeObject(o, Formatting.None, timeFormat);
            else
                return JsonConvert.SerializeObject(o, Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 将JSON转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string json)
        {
            if (json == null)
                return System.Activator.CreateInstance<T>();

            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 把字符串转成JSON数据表
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Newtonsoft.Json.Linq.JObject ToJsonData(this string json)
        {
            Newtonsoft.Json.Linq.JObject result = Newtonsoft.Json.Linq.JObject.Parse(json);

            return result;
        }

        /// <summary>
        /// 去除指定JSON中的Key对应的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJsonValue(this string json, string key)
        {
            Newtonsoft.Json.Linq.JObject result = Newtonsoft.Json.Linq.JObject.Parse(json);

            return result[key].ToString();
        }

    }
}
