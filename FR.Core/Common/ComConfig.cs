using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

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
    }
}
