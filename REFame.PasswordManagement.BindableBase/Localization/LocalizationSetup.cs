using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Model.Enums;

namespace REFame.PasswordManagement.WpfBase.Localization
{
    public class LocalizationModule : IModule
    {
        public async Task Initialize(ICore core)
        {
            Localizations.Current.RegisterLanguage(Language.English, LocalizationResources.EN);
            Localizations.Current.RegisterLanguage(Language.German, LocalizationResources.DE);
        }
    }
}