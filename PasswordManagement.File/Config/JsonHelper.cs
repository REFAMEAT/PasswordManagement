using System;
using System.IO;
using Newtonsoft.Json;
using PasswordManagement.Logging;

namespace PasswordManagement.File.Config
{
    /// <summary>
    /// Serialize and Deserialize JSON files
    /// </summary>
    internal class JsonHelper<T> where T : class
    {
        /// <summary>
        /// Path to the JSON file
        /// </summary>
        internal const string jsonConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\{type}.json";

        private static string GetPath()
        {
            return jsonConfigPath.Replace("{user}", Environment.UserName).Replace("{type}", typeof(T).Name);
        }

        /// <summary>
        /// Read a <see cref="ThemeData"/> from the JSON file
        /// </summary>
        /// <returns></returns>
        internal static T GetData(T defaultValue = null)
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
                    Logger.Current.Error(ex);
                    throw;
                }
                WriteData(defaultValue); 
                return GetData(null);
            }

            content = content.Replace("\r\n", null);

            T data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        /// <summary>
        /// Write a <see cref="ThemeData"/> to a JSON file
        /// </summary>
        /// <param name="value"></param>
        internal static void WriteData(T value)
        {
            JsonSerializer serializer = new JsonSerializer();

            using StreamWriter sw = new StreamWriter(GetPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            var x = JsonConvert.SerializeObject(value);

            serializer.Serialize(jsonWriter, value);
        }
    }
}