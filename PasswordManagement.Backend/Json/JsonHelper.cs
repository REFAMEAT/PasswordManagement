using System;
using System.IO;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using PasswordManagement.Backend.Theme;
using PasswordManagement.Backend.Xml;

namespace PasswordManagement.Backend.Json
{
    /// <summary>
    /// Serialize and Deserialize JSON files
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// Path to the JSON file
        /// </summary>
        public const string jsonConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\config.json";

        private static string GetPath()
        {
            return jsonConfigPath.Replace("{user}", Environment.UserName);
        }

        private static async Task<string> GetPathAsync()
        {
            return await Task.Run(() => jsonConfigPath.Replace("{user}", Environment.UserName));
        }

        /// <summary>
        /// Read a <see cref="ThemeData"/> from the JSON file
        /// </summary>
        /// <returns></returns>
        public static ThemeData GetData()
        {
            string content;

            try
            {
                content = File.ReadAllText(GetPath());
            }
            catch (FileNotFoundException)
            {
                WriteData(new ThemeData
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

        /// <summary>
        /// Write a <see cref="ThemeData"/> to a JSON file
        /// </summary>
        /// <param name="value"></param>
        public static void WriteData(ThemeData value)
        {
            JsonSerializer serializer = new JsonSerializer();

            using StreamWriter sw = new StreamWriter(GetPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            serializer.Serialize(jsonWriter, value);
        }

        public static async Task WriteDataAsync(ThemeData value)
        {
            JsonSerializer serializer = new JsonSerializer();

            await using StreamWriter sw = new StreamWriter(await GetPathAsync());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            await Task.Run(() => serializer.Serialize(jsonWriter, value));
        }
    }
}