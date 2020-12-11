using System;
using REFame.PasswordManagement.File.Contracts.Config;

namespace REFame.PasswordManagement.File.Config.Factory
{
    public class ConfigurationFactory<T> : IConfigurationFactory<T> where T : new()
    {
        private Configuration<T> configuration;
        public ConfigurationFactory()
        {
            configuration = new Configuration<T>();
        }
        public string CurrentPath { get; private set; }

        public IConfiguration<T> Create()
        {
            return configuration;
        }

        public IConfigurationFactory<T> SetPath(string path = null)
        {
            var newPath = string.IsNullOrWhiteSpace(path)
                ? defaultPath.Replace("{user}", Environment.UserName)
                : path.Replace("{user}", Environment.UserName);

            configuration.Path = CurrentPath = newPath;
            return this;
        }

        private readonly string defaultPath = System.IO.Path.Combine
        (
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "REFAME",
            "PasswordManagement",
            $"{typeof(T).Name}.json"
        );
    }
}