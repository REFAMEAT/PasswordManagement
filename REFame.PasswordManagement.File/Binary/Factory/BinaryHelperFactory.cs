using System;
using REFame.PasswordManagement.File.Contracts.Binary;

namespace REFame.PasswordManagement.File.Binary.Factory
{
    public class BinaryHelperFactory : IBinaryHelperFactory
    {
        private const string xmlConfigPathDefault = @"C:\Users\{user}\AppData\Roaming\PWManagement\data.bin";
        private readonly IBinaryHelper binaryHelper;

        public BinaryHelperFactory()
        {
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