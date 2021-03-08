using System;
using System.IO;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.File;

namespace REFame.PasswordManagement.Configuration
{
    public class ConfigurationFactory<T> : IConfigurationFactory<T> where T : new()
    {
        private Configuration<T> configuration;
        public ConfigurationFactory(FolderProvider folderProvider)
        {
            configuration = new Configuration<T>();
            defaultPath = Path.Combine(folderProvider.AppDataFolder, $"{typeof(T).Name}.json");
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

        private readonly string defaultPath;
    }
}