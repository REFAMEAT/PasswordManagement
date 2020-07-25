using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PasswordManagement.Model;

namespace PasswordManagement.File.Binary
{
    /// <summary>
    /// Helper class for Binaries
    /// </summary>
    public class BinaryHelper
    {
        /// <summary>
        /// The path to the bin-file
        /// </summary>
        public const string xmlConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\data.bin";

        /// <summary>
        /// Read a <see cref="BinaryData"/> from the .bin file
        /// </summary>
        /// <returns></returns>
        internal BinaryData GetData()
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName),
                FileMode.OpenOrCreate);
            return (BinaryData)formatter.Deserialize(s);
        }

        /// <summary>
        /// Write a <see cref="BinaryData"/> to the .bin File
        /// </summary>
        /// <param name="content"></param>
        internal void Write(BinaryData content)
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName),
                FileMode.OpenOrCreate);
            formatter.Serialize(s, content);
        }
    }
}