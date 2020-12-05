using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.UI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public List<SettingMediator> Mediators { get; } = new List<SettingMediator>();

        public Settings()
        {
            InitializeComponent();
        }

        public void AddSetting<TFactory, TTabItem>()
            where TFactory : ISettingFactory, new()
            where TTabItem : TabItem, new()
        {
            var factory = new TFactory();
            string header = factory.GetHeader();
            BindableBase viewModel = factory.GetViewModel();
            SettingMediator mediator = factory.GetMediator();

            var tabItem = new TTabItem
            {
                DataContext = viewModel,
                Header = header,
                Style = (Style)Resources["StyleTabControl"]
            };

            TabControl.Items.Add(tabItem);
            Mediators.Add(mediator);
        }

        public void SetViewModel(IBindableFactory vmFactory)
        {
            DataContext = vmFactory.GetViewModel();
        }
    }
}
