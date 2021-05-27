using System.Windows;
using System.Windows.Input;
using REFame.PasswordManagement.ProgressBar.ViewModel;

namespace REFame.PasswordManagement.ProgressBar.View
{
    /// <summary>
    /// Interaction logic for Progress.xaml
    /// </summary>
    public partial class ProgressView : Window
    {
        public ProgressView()
        {
            InitializeComponent();

            Resources.MergedDictionaries.Clear();

            var list = Application.Current?.Resources?.MergedDictionaries;

            if (list != null)
            {
                foreach (ResourceDictionary dictionary in list)
                {
                    Resources.MergedDictionaries.Add(dictionary);
                } 
            }

            ViewModel = new ProgressViewModel();
        }

        public ProgressViewModel ViewModel
        {
            get => DataContext as ProgressViewModel; 
            private set => DataContext = value;
        }

        private void ProgressView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
