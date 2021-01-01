using System;
using System.Reflection;
using System.Windows;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IInitializable
    {
        private readonly IDataManager<PasswordData> dataManager;

        public MainWindow(IDataManager<PasswordData> dataManager = null)
        {
            this.dataManager = dataManager;
            InitializeComponent();

            Title = string.Format(Loc.MainWindow_ctor_Title, typeof(MainWindow).Assembly.GetName().Version);

        }

        public void Initialize()
        {
            DataContext = new MainViewModel(dataManager);
        }
    }
}