using System.Collections.Generic;
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
        private ICommand buttonCommandAddItem;

        public MainViewModel()
        {
            binaryData = binaryHelper.GetData();

            Items = binaryData.Passwords;
        }

        public List<PasswordData> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public ICommand ButtonCommandOpenSettings => buttonCommandOpenSettings ??= new Command(DoOpenSettings);
        public ICommand ButtonCommandAddItem => buttonCommandAddItem ??= new Command(DoAddItem);

        private void DoOpenSettings(object obj)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void DoAddItem(object obj)
        {
            Add add = new Add();
            add.Show();
        }
    }
}