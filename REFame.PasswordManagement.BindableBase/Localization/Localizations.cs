using System;
using System.Collections.Generic;
using System.Linq;
using REFame.PasswordManagement.Model.Enums;

namespace REFame.PasswordManagement.WpfBase.Localization
{
    /// <summary>
    /// Provide information about supported languages or register supported languages
    /// </summary>
    [Obsolete]
    public class Localizations
    {
        private static Localizations current;
        private Dictionary<Language, Uri> registeredLanguages;

        private Localizations()
        {
            registeredLanguages = new Dictionary<Language, Uri>();
            registeredLanguages.Add(Language.English, new Uri("pack://application:,,,/REFame.PasswordManagement.WpfBase;component/Localization/StringResources.EN.xaml"));
        }

        public static Localizations Current => current ??= new Localizations();

        public void RegisterLanguage(Language language, string locFilePath)
        {
            if (registeredLanguages.TryGetValue(language, out _))
            {
                throw new InvalidOperationException("Language is already registered");
            }

            registeredLanguages.Add(language, new Uri(locFilePath));
        }

        public Uri GetRegisteredLanguageUri(Language language)
        {
            return registeredLanguages[language];
        }

        public List<Language> GetAllLanguages()
        {
            return registeredLanguages.Keys.ToList();
        }
    }
}