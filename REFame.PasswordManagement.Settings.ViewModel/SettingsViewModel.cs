using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.Settings.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        public List<SettingMediator> SettingMediators { get; set; } = new List<SettingMediator>();

        public SettingsViewModel()
        {
            SettingMediators = new List<SettingMediator>();
            SaveCommand = new AsyncCommand(Save);
        }

        public ICommand SaveCommand { get; }

        private async Task Save()
        {
            SettingMediators.ForEach(async x => x.RequestSave(this));
        }
    }
}