using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.App.CoreComponents
{
    public class UIBuilder : IUIBuilder
    {
        public ThemeData BuildTheme(ICore core)
        {
            ThemeData data = core.GetRegisteredType<ISettingService<ThemeData>>().Load().Result;

            return data;
        }
    }
}