using System.Globalization;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.App.CoreComponents
{
    public class CultureBuilder : ICultureBuilder
    {
        public CultureInfo Get(ICore core)
        {
            string language = core.GetRegisteredType<IConfigurationFactory<ThemeData>>()
                .SetPath()
                .Create()
                .LoadAsync()
                .Result.Language;

            return new CultureInfo(language ?? "en-001");
        }
    }
}