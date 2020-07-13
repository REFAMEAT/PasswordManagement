using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using PasswordManagement.Annotations;
using PasswordManagement.Backend.Binary;
using PasswordManagement.Backend.Security;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private readonly BinaryData binaryData;
        private readonly BinaryHelper binaryHelper = new BinaryHelper();
        private ICommand buttonCommandAddItem;
        private ICommand buttonCommandDeleteItem;
        private ICommand buttonCommandOpenSettings;

        private ObservableCollection<PasswordDataDisplay> items;
        private PasswordDataDisplay selectedItem;

        public MainViewModel()
        {
            binaryData = binaryHelper.GetData();

            Items = new ObservableCollection<PasswordDataDisplay>(
                binaryData.Passwords.ConvertAll(x => new PasswordDataDisplay(x)));
        }

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

        public ICommand ButtonCommandOpenSettings => buttonCommandOpenSettings ??= new Command(DoOpenSettings);
        public ICommand ButtonCommandAddItem => buttonCommandAddItem ??= new Command(DoAddItem);
        public ICommand ButtonCommandDeleteItem => buttonCommandDeleteItem ??= new Command(DoDeleteItem);

        private void DoDeleteItem(object obj)
        {
            if (SelectedItem == null) return;

            PasswordData itemToDelete = binaryData.Passwords
                .Find(x => x.Password == SelectedItem.Password
                           && x.Description == SelectedItem.Description
                           && x.Comments == SelectedItem.Comments);

            binaryData.Passwords.Remove(itemToDelete);
            Items.Remove(SelectedItem);
            binaryHelper.Write(binaryData);
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
                if (!addPassword.DialogResult != true)
                {
                    return;
                }

                PasswordData newItem = ((AddPasswordViewModel) addPassword.DataContext).NewItem;

                Items.Add(new PasswordDataDisplay(newItem));
                binaryData.Passwords.Add(newItem);
                binaryHelper.Write(binaryData);
            };
        }
    }

    public class PasswordDataDisplay : PasswordData, INotifyPropertyChanged
    {
        private bool display;
        private readonly Timer displayTimer;
        private string passwordDisplay;

        public PasswordDataDisplay(PasswordData baseData)
        {
            Password = baseData.Password;
            Comments = baseData.Comments;
            Description = baseData.Description;
            displayTimer = new Timer(2000);
            displayTimer.AutoReset = false;
            displayTimer.Elapsed += (sender, args) => Display = false;
            Display = false;
        }

        public string PasswordDisplay
        {
            get => passwordDisplay;
            set => SetProperty(ref passwordDisplay, value);
        }

        public bool Display
        {
            get => display;
            set
            {
                displayTimer.Enabled = value;
                if (value)
                {
                    PasswordDisplay = Encryption.DecryptString(Password, App.LogedIn);
                }
                else
                {
                    PasswordDisplay = string.Empty;
                    for (int i = Encryption.DecryptString(Password, App.LogedIn).Length - 1; i >= 0; i--)
                        PasswordDisplay += '•';
                }

                SetProperty(ref display, value);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion
    }
}