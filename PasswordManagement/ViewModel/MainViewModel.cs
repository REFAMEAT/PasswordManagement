using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using PasswordManagement.Backend.Binary;
using PasswordManagement.Backend.Data;
using PasswordManagement.Model;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private readonly IDataManager<PasswordData> dataManager;
        private ICommand buttonCommandAddItem;
        private ICommand buttonCommandDeleteItem;
        private ICommand buttonCommandOpenSettings;

        private ObservableCollection<PasswordDataDisplay> items;
        private PasswordDataDisplay selectedItem;

        public MainViewModel()
        {
            dataManager = new FileDataManager();

            Items = ToDisplayData(dataManager.LoadData());
        }

        public ICommand ButtonCommandOpenSettings => buttonCommandOpenSettings ??= new Command(DoOpenSettings);
        public ICommand ButtonCommandAddItem => buttonCommandAddItem ??= new Command(DoAddItem);
        public ICommand ButtonCommandDeleteItem => buttonCommandDeleteItem ??= new Command(DoDeleteItem);

        public ObservableCollection<PasswordDataDisplay> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public PasswordDataDisplay SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        private void DoDeleteItem(object obj)
        {
            if (SelectedItem == null)
            {
                return;
            }

            PasswordData itemToDelete = dataManager.LoadData()
                .Find(x => x.Password == SelectedItem.Password
                           && x.Description == SelectedItem.Description
                           && x.Comments == SelectedItem.Comments);

            bool deleted = dataManager.Remove(itemToDelete);
            if (deleted)
            {
                Items.Remove(SelectedItem);
            }
        }

        private void DoOpenSettings(object obj)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void DoAddItem(object obj)
        {
            AddPassword addPassword = new AddPassword();
            addPassword.Show();
            addPassword.Closed += (sender, e) =>
            {
                if (addPassword.Canceled)
                {
                    return;
                }

                PasswordData newItem = ((AddPasswordViewModel)addPassword.DataContext).NewItem;

                dataManager.AddData(newItem);
                Items.Add(new PasswordDataDisplay(newItem));
            };
        }

        private ObservableCollection<PasswordDataDisplay> ToDisplayData(List<PasswordData> data)
        {
            return new ObservableCollection<PasswordDataDisplay>(
                data.ConvertAll(x => new PasswordDataDisplay(x)));
        }
    }
}