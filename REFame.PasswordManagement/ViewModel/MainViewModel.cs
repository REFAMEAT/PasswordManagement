using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.App.Model;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Data.Contracts;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.UserManagement.Contracts;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly IDataManager<PasswordData> dataManager;

        private ObservableCollection<PasswordDataDisplay> items;
        private PasswordDataDisplay selectedItem;

        public MainViewModel(IDataManager<PasswordData> dataManager)
        {
            this.dataManager = dataManager;
            Items = ToDisplayData(dataManager.LoadData());

            ButtonCommandOpenSettings = new AsyncCommand(DoOpenSettings);
            ButtonCommandAddItem = new AsyncCommand(DoAddItem);
            ButtonCommandDeleteItem = new AsyncCommand(DoDeleteItem);
            CommandOpenUserManagement = new AsyncCommand(OpenUserManagement);
        }

        public ICommand ButtonCommandOpenSettings { get; set; }
        public ICommand ButtonCommandAddItem { get; set; }
        public ICommand ButtonCommandDeleteItem { get; set; }
        public ICommand CommandOpenUserManagement { get; set; }

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

        private Task DoOpenSettings(object obj)
        {
            PWCore.CurrentCore
                .GetRegisteredType<ISetting>()
                .Open();

            return Task.CompletedTask;
        }

        private Task DoAddItem(object obj)
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

            return Task.CompletedTask;
        }

        private async Task OpenUserManagement()
        {
            await PWCore.CurrentCore
                .GetRegisteredType<IUserMgmt>()
                .Open();
        }

        private ObservableCollection<PasswordDataDisplay> ToDisplayData(List<PasswordData> data)
        {
            return new ObservableCollection<PasswordDataDisplay>(
                data.ConvertAll(x => new PasswordDataDisplay(x)));
        }
    }
}