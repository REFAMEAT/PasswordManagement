using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PasswordManagement.Backend.Xml
{
    [Obsolete("Use Json Serializer!!!")]
    public class XmlHelper
    {
        public const string xmlConfigPath = @"C:\Users\{user}\AppData\Roaming\PWManagement\config.xml";
        public XmlSerializer serializer;

        public XmlHelper()
        {
            serializer = new XmlSerializer(typeof(ThemeData));
        }

        public ThemeData GetData()
        {
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName), FileMode.Truncate);
            try
            {
                return (ThemeData)serializer.Deserialize(s);
            }
            catch (Exception)
            {
                serializer.Serialize(s, new ThemeData());
                return new ThemeData();
            }
        }

        public void Write(ThemeData value)
        {
            using Stream s = new FileStream(xmlConfigPath.Replace("{user}", Environment.UserName), FileMode.Truncate);

            FileInfo fileInfo = new FileInfo(xmlConfigPath.Replace("{user}", Environment.UserName));
            if (fileInfo.Directory != null 
                && !Directory.Exists(fileInfo.Directory.FullName))
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

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