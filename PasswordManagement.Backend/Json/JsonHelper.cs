using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using PasswordManagement.Backend.Xml;
using System;
using System.IO;
using PasswordManagement.Backend.Theme;

namespace PasswordManagement.Backend.Json
{
    public class JsonHelper
    {
        public const string jsonConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\config.json";

        private static string GetPath() => jsonConfigPath.Replace("{user}", Environment.UserName);

        public static ThemeData GetData()
        {
            string content;

            try
            {
               content = File.ReadAllText(GetPath());
            }
            catch (FileNotFoundException)
            {
                WriteData(new ThemeData()
                {
                    Language = Language.English, 
                    PrimaryColor = "Blue", 
                    Theme = BaseTheme.Light
                });
                return GetData();
            }

            content = content.Replace("\r\n", null);

            ThemeData data = JsonConvert.DeserializeObject<ThemeData>(content);

            return data;
        }

        public static void WriteData(ThemeData value)
        {
            JsonSerializer serializer = new JsonSerializer();

            using StreamWriter sw = new StreamWriter(GetPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            serializer.Serialize(jsonWriter, value);
        }
    }
}