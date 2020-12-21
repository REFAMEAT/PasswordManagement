using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.File.Binary
{
    /// <summary>
    ///     Helper class for Binaries
    /// </summary>
    public class BinaryHelper : IBinaryHelper
    {
        private string xmlConfigPath;

        public BinaryHelper()
        {
            xmlConfigPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "REFame",
                "PasswordManagement",
                "data.bin");
        }

        /// <summary>
        ///     Read a <see cref="BinaryData" /> from the .bin file
        /// </summary>
        /// <returns></returns>
        public BinaryData GetData()
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath, FileMode.OpenOrCreate);

            var data = (BinaryData)formatter.Deserialize(s);

            data.Passwords ??= new List<PasswordData>();

            return data;
        }

        /// <summary>
        ///     Write a <see cref="BinaryData" /> to the .bin File
        /// </summary>
        /// <param name="content"></param>
        public void Write(BinaryData content)
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath, FileMode.OpenOrCreate);
            formatter.Serialize(s, content);
        }

        /// <summary>
        ///     Read a <see cref="BinaryData" /> from the .bin file
        /// </summary>
        /// <returns></returns>
        public async Task<BinaryData> GetDataAsync()
        {
            IFormatter formatter = new BinaryFormatter();
            await using Stream s = new FileStream(xmlConfigPath, FileMode.OpenOrCreate);

            var data = (BinaryData)formatter.Deserialize(s);

            data.Passwords ??= new List<PasswordData>();

            return data;
        }

        public async Task WriteAsync(BinaryData content)
        {
            IFormatter formatter = new BinaryFormatter();
            await using Stream s = new FileStream(xmlConfigPath, FileMode.OpenOrCreate);

            formatter.Serialize(s, content);
        }

        public void OverwriteDefaultPath(string newPath)
        {
            if (!System.IO.File.Exists(newPath))
            {
                FileInfo fileInfo = new FileInfo(newPath);

                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
                
                System.IO.File.Create(newPath);
                //throw new FileNotFoundException("cannot find file", newPath);
            }

            xmlConfigPath = newPath;
        }
    }
}