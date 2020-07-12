using System;
using System.IO;
using System.Xml.Serialization;

namespace PasswordManagement.Backend.Xml
{
    public class XmlHelper
    {
        public const string xmlConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\config.xml";
        public XmlSerializer serializer;

        public XmlHelper()
        {
            serializer = new XmlSerializer(typeof(XmlData));
        }

        public XmlData GetData()
        {
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName), FileMode.OpenOrCreate);
            try
            {
                return (XmlData)serializer.Deserialize(s);
            }
            catch (Exception)
            {
                serializer.Serialize(s, new XmlData());
                return new XmlData();
            }
        }

        public void Write(XmlData value)
        {
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName), FileMode.Truncate);

            try
            {
                serializer.Serialize(s, value);
            }
            catch (Exception)
            {
                // TODO: implement logging
            }

        }
    }
}