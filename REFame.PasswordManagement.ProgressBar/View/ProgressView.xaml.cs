using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            { 
                Resources.MergedDictionaries.Add(dictionary);
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
