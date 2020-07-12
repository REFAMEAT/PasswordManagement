using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using PasswordManagement.Backend.Xml;

namespace PasswordManagement.Backend.BinarySerializer
{
    public class BinaryHelper
    {
        public const string xmlConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\data.bin";
        public XmlSerializer serializer;

       
        public BinaryData GetData()
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName), FileMode.OpenOrCreate);
            return (BinaryData)formatter.Deserialize(s);
        }

        public void Write(BinaryData content)
        {
            IFormatter formatter = new BinaryFormatter();
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName), FileMode.OpenOrCreate);
            formatter.Serialize(s, content);
        }
    }
}