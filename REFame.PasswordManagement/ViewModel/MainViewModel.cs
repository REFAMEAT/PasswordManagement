using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.App.Model;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Settings.Call;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IDataManager<PasswordData> dataManager;
        private ICommand buttonCommandAddItem;
        private ICommand buttonCommandDeleteItem;
        private ICommand buttonCommandOpenSettings;

        private ObservableCollection<PasswordDataDisplay> items;
        private PasswordDataDisplay selectedItem;

        public MainViewModel(IDataManager<PasswordData> dataManager)
        {
            this.dataManager = dataManager;
            Items = ToDisplayData(dataManager.LoadData());
        }

        public MainViewModel()
        {
            bool useDatabase = Globals.UseDatabase;
            dataManager = PWCore.CurrentCore.GetRegisteredType<IDataManager<PasswordData>>();
            Items = ToDisplayData(dataManager.LoadData());
        }

        public ICommand ButtonCommandOpenSettings => buttonCommandOpenSettings ??= new AsyncCommand(DoOpenSettings);
        public ICommand ButtonCommandAddItem => buttonCommandAddItem ??= new AsyncCommand(DoAddItem);
        public ICommand ButtonCommandDeleteItem => buttonCommandDeleteItem ??= new AsyncCommand(DoDeleteItem);

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

        private async Task DoDeleteItem(object obj)
        {
            if (SelectedItem == null)
            {
                return;
            }

            PasswordData itemToDelete = (await dataManager.LoadDataAsync())
                .Find(x => x.Password == SelectedItem.Password
                           && x.Description == SelectedItem.Description
                           && x.Comments == SelectedItem.Comments);

            bool deleted = await dataManager.RemoveAsync(itemToDelete);
            if (deleted)
            {
                Items.Remove(SelectedItem);
            }
        }

        private async Task DoOpenSettings(object obj)
        {
            SettingCall.Open();
        }

        private async Task DoAddItem(object obj)
        {
            var addPassword = new AddPassword();
            addPassword.Show();
            addPassword.Closed += async (sender, e) =>
            {
                if (addPassword.Canceled)
                {
                    return;
                }

                PasswordData newItem = ((AddPasswordViewModel) addPassword.DataContext).NewItem;
                newItem.Identifier = Guid.NewGuid().ToString();
                await dataManager.AddDataAsync(newItem);
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