using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkModManager
{
    public class Configuration
    {
        public static string steamLibraryPath;
        public static Dictionary<string, string>? GetConfig()
        {
            
            string file = "config.json";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            string jsontext = File.ReadAllText(path);
            Dictionary<string, string>? config = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsontext);
            return config;
        }

        public static void Init()
        {
            var init = GetConfig();
            if (init != null) steamLibraryPath = init["SteamLibraryPath"];
        }
    }
}
