using System;
using System.Collections.Generic;
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

            BinaryData data = (BinaryData) formatter.Deserialize(s);

            if (data.Passwords == null)
            {
                data.Passwords = new List<PasswordData>();
            }

            return data;
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