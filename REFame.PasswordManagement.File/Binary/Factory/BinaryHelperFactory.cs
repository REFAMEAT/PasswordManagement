using System;
using System.IO;
using REFame.PasswordManagement.File.Contracts.Binary;

namespace REFame.PasswordManagement.File.Binary.Factory
{
    public class BinaryHelperFactory : IBinaryHelperFactory
    {
        private readonly string xmlConfigPathDefault;
        private readonly IBinaryHelper binaryHelper;

        public BinaryHelperFactory()
        {
            xmlConfigPathDefault = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "REFame",
                "PasswordManagement",
                "data.bin");
            binaryHelper = new BinaryHelper();
        }

        public IBinaryHelperFactory SetPath(string path = null)
        {
            var configPath = string.IsNullOrWhiteSpace(path)
                ? xmlConfigPathDefault.Replace("{user}", Environment.UserName)
                : path.Replace("{user}", Environment.UserName);

            binaryHelper.OverwriteDefaultPath(configPath);
            CurrentPath = configPath;
            return this;
        }

        public IBinaryHelper Create()
        {
            return binaryHelper;
        }

        public string CurrentPath { get; private set; }
    }
}