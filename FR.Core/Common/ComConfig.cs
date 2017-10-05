using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace FR.Core
{
    public class ComConfig
    {
        public static IConfiguration AppSettings { get; set; }
        static ComConfig()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            AppSettings = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

        /// <summary>
        /// 读取配置文件 appSettings
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="configKey">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T GetAppConfig<T>(string configKey, T defaultValue)
        {
            T obj = default(T);
            try
            {
                obj = (T)Convert.ChangeType(ComConfig.AppSettings[configKey], typeof(T));
                if (obj == null)
                    obj = defaultValue;
            }
            catch
            {
                obj = defaultValue;
            }
            return obj;
        }

    }
}
