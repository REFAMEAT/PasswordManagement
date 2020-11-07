using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.File.Binary
{
    /// <summary>
    ///     Helper class for Binaries
    /// </summary>
    public class BinaryHelper
    {
        /// <summary>
        ///     The path to the bin-file
        /// </summary>
        private const string xmlConfigPathDefault = @"C:\Users\{user}\AppData\Roaming\PWManagement\data.bin";

        private readonly string xmlConfigPath;

        public BinaryHelper(string path = null)
        {
            if (path != null)
            {
                if (!System.IO.File.Exists(path))
                {
                    throw new FileNotFoundException("cannot find file", path);
                }

                xmlConfigPath = path;
            }
            else
            {
                xmlConfigPath = xmlConfigPathDefault;
            }
        }

        /// <summary>
        ///     Read a <see cref="BinaryData" /> from the .bin file
        /// </summary>
        /// <returns></returns>
        public BinaryData GetData()
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName),
                FileMode.OpenOrCreate);

            var data = (BinaryData) formatter.Deserialize(s);

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
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName),
                FileMode.OpenOrCreate);
            formatter.Serialize(s, content);
        }
    }
}