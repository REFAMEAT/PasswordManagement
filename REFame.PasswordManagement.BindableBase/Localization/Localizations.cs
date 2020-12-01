using System;
using System.CodeDom;
using System.Collections.Generic;
using REFame.PasswordManagement.Model.Enums;

namespace REFame.PasswordManagement.WpfBase.Localization
{
    public class Localizations
    {
        private static Localizations current;
        private Dictionary<Language, Uri> registeredLanguages;

        private Localizations()
        {
            registeredLanguages = new Dictionary<Language, Uri>();
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
    }
}