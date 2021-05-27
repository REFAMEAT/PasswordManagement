using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.AppCore.Contracts.Components
{
    public interface IUIBuilder
    {
        ThemeData BuildTheme(ICore core);
    }
}