using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.AppCore.Tests.Mock
{
    public class MockUIBuilder : IUIBuilder
    {
        public ThemeData BuildTheme(ICore core)
        {
            return new ThemeData();
        }
    }
}