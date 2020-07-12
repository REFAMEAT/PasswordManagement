using System.Collections.Generic;
using System.Security.AccessControl;
using System.Windows.Documents;
using System.Windows.Input;
using PasswordManagement.Backend.BinarySerializer;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private BinaryHelper binaryHelper = new BinaryHelper();
        private BinaryData binaryData;

        private List<PasswordData> items;
        private ICommand buttonCommandOpenSettings;

        public MainViewModel()
        {
            binaryData = binaryHelper.GetData();

            items = binaryData.Passwords;
        }

        public List<PasswordData> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public ICommand ButtonCommandOpenSettings => buttonCommandOpenSettings ??= new Command(DoOpenSettings);

        private void DoOpenSettings(object obj)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }
}