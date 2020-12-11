﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REFame.PasswordManagement.Logging;

namespace REFame.PasswordManagement.File.Config
{
    public static class JsonPathInfo
    {
        public static bool InTest { get; set; } = Debugger.IsAttached;
    }

    /// <summary>
    ///     Serialize and Deserialize JSON files
    /// </summary>
    public static class JsonHelper<T> where T : class, new()
    {
        public static string GetPath()
        {
            string savePath = Path.Combine
            (
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "REFAME",
                "PasswordManagement"
            );
            if (JsonPathInfo.InTest)
            {
                savePath = Path.Combine(savePath, "_dev");
                Directory.CreateDirectory(savePath);
            }

            return Path.Combine(savePath, $"{typeof(T).Name}.json");
        }

        /// <summary>
        ///     Read a generic type from a JSON file
        /// </summary>
        /// <returns>The deserialized object</returns>
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
        ///     Read a generic type from a JSON file
        /// </summary>
        /// <returns>The deserialized object</returns>
        public static async Task<T> GetDataAsync(T defaultValue = null)
        {
            string content;

            try
            {
                content = await System.IO.File.ReadAllTextAsync(GetPath());
            }
            catch (FileNotFoundException ex)
            {
                if (defaultValue == null)
                {
                    Logger.Current.Get().Error(ex);
                    throw;
                }

                await WriteDataAsync(defaultValue);
                return await GetDataAsync();
            }

            content = content.Replace("\r\n", null);
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        /// <summary>
        ///     Write a generic type to a JSON file
        /// </summary>
        /// <param name="value">The value to serialize</param>
        public static void WriteData(T value)
        {
            var serializer = new JsonSerializer();

            using var sw = new StreamWriter(GetPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            JsonConvert.SerializeObject(value);

            serializer.Serialize(jsonWriter, value);
        }

        /// <summary>
        ///     Write a generic type to a JSON file
        /// </summary>
        /// <param name="value">The value to serialize</param>
        public static async Task WriteDataAsync(T value)
        {
            var serializer = new JsonSerializer();

            await using var sw = new StreamWriter(GetPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            JsonConvert.SerializeObject(value);

            serializer.Serialize(jsonWriter, value);
        }
    }
}