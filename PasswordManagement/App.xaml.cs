using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PasswordManagement.Backend.Xml;

namespace PasswordManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            XmlData data = new XmlHelper().GetData();
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (data.Language)
            {
                case Language.English:
                    dictionary.Source = new Uri("..\\Resources\\StringResources.EN.xaml", UriKind.Relative); 
                    break;
                case Language.German:
                    dictionary.Source = new Uri("..\\Resources\\StringResources.DE.xaml", UriKind.Relative); 
                    break;
            }

            Resources.MergedDictionaries.Add(dictionary);
        }
    }
}
