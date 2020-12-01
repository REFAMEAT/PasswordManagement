using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Enums;

namespace REFame.PasswordManagement.WpfBase.Localization
{
    public static class LocalizationSetup
    {
        public static AppCore SetupLocalization(this AppCore core)
        {
            Localizations.Current.RegisterLanguage(Language.English, LocalizationResources.EN);
            Localizations.Current.RegisterLanguage(Language.German, LocalizationResources.DE);

            return core;
        }
    }
}