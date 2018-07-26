using Newtonsoft.Json;
using System.IO;

namespace BitSms.EmailSender
{
    public class ConfigLoader : IConfigLoader
    {
        public Config GetConfig(string path)
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }
    }
}