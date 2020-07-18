using System;
using System.IO;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Core;
using Newtonsoft.Json;

namespace PasswordManagement.Logging
{
    public class Logger : IDisposable
    {
        private static Logger current;
        public static Logger Current => current ??= new Logger();

        private const string configPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\Logs.json";

        private Logger()
        {

        }

        public void Debug(string info, [CallerMemberName] string caller = null)
        {
            Log log = new Log
            {
                ErrorMessage = info,
                Level = LogLevel.Debug,
                Caller = caller,
                TimeStamp = DateTime.Now.ToShortTimeString(),
            };

            WriteInternal(log);
        }

        public void Info(string info, [CallerMemberName] string caller = null)
        {
            Log log = new Log
            {
                ErrorMessage = info,
                Level = LogLevel.Info,
                Caller = caller,
                TimeStamp = DateTime.Now.ToShortTimeString(),
            };

            WriteInternal(log);
        }

        public void Error(Exception ex, [CallerMemberName] string caller = null)
        {
            Log log = new Log()
            {
                ErrorMessage = ex.Message,
                Level = LogLevel.Error,
                Caller = caller,
                TimeStamp = DateTime.Now.ToShortTimeString(),
            };

            WriteInternal(log);
        }

        private void WriteInternal(Log log)
        {
            Logs logs = GetInternal();
            logs.SavedLogs.Add(log);

            JsonSerializer serializer = new JsonSerializer();
            using StreamWriter sw = new StreamWriter(GetLogPath());
            using JsonWriter jsonWriter = new JsonTextWriter(sw);

            serializer.Serialize(jsonWriter, logs);
        }

        private Logs GetInternal()
        {
            string content;

            try
            {
                content = File.ReadAllText(GetLogPath());
            }
            catch (FileNotFoundException)
            {
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter sw = new StreamWriter(GetLogPath()))
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jsonWriter, new Logs
                    {
                        MachineName = Environment.MachineName,
                        UserName = Environment.UserName,
                        WindowsVersion = Environment.OSVersion.VersionString
                    });
                }
                
                content = File.ReadAllText(GetLogPath());
            };

            content = content.Replace("\r\n", null);

            Logs data = JsonConvert.DeserializeObject<Logs>(content);

            return data;
        }

        private string GetLogPath(string user = "")
        {
            return configPath.Replace("{user}", user == string.Empty ? Environment.UserName : user);
        }

        public void Dispose()
        {

        }
    }
}