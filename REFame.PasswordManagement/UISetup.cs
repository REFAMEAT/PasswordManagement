using System.Threading.Tasks;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.App
{
    class UISetup : IModule
    {
        public async Task Initialize(ICore core)
        {
            // Register method when theme-change is requested
            ThemeMediator.ChangeThemeRequested += 
                async (sender, args) => await UiHelper.AdjustApplicationStyle();

            // Adjust current App-Style
            await UiHelper.AdjustApplicationStyle();
        }
    }
}