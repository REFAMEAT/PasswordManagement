using System;
using System.IO;
using Newtonsoft.Json;
using PasswordManagement.Logging;

namespace PasswordManagement.File.Config
{
    /// <summary>
    ///     Serialize and Deserialize JSON files
    /// </summary>
    public class JsonHelper<T> where T : class
    {
        /// <summary>
        ///     Path to the JSON file
        /// </summary>
        private const string jsonConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\{type}.json";

        public static string GetPath()
        {
            return jsonConfigPath.Replace("{user}", Environment.UserName).Replace("{type}", typeof(T).Name);
        }

        /// <summary>
        ///     Read a <see cref="ThemeData" /> from the JSON file
        /// </summary>
        /// <returns></returns>
        public static T GetData(T defaultValue = null)
        {
            string content;

            try
            {
                content = System.IO.File.ReadAllText(GetPath());
            }
            catch (FileNotFoundException ex)
            {
                if (defaultValue == null)
                {
                    Logger.Current.Get().Error(ex);
                    throw;
                }

                WriteData(defaultValue);
                return GetData();
            }

            content = content.Replace("\r\n", null);

            var data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        /// <summary>
        ///     Write a <see cref="ThemeData" /> to a JSON file
        /// </summary>
        /// <param name="value"></param>
        public static void WriteData(T value)
        {
            var serializer = new JsonSerializer();

            using var sw = new StreamWriter(GetPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            string x = JsonConvert.SerializeObject(value);

            serializer.Serialize(jsonWriter, value);
        }
    }
}