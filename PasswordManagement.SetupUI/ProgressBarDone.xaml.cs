using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace PasswordManagement.SetupUI
{
    /// <summary>
    /// Interaction logic for ProgressBarDone.xaml
    /// </summary>
    public partial class ProgressBarDone : UserControl
    {
        private ProgressBar progressBar;
        private PackIcon icon;

        public ProgressBarDone()
        {
            InitializeComponent();

            progressBar = new ProgressBar();
            progressBar.Value = 1;
            progressBar.ValueChanged += ProgressBar_OnValueChanged;
            progressBar.Style = FindResource("MaterialDesignCircularProgressBar") as Style;

            icon = new PackIcon();
            icon.Kind = PackIconKind.Done;
            icon.Foreground = Brushes.Green;

            Grid.Children.Add(progressBar);
        }

        private void ProgressBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue == 100)
            {
                Grid.Children.Remove(progressBar);
                Grid.Children.Add(icon);
            }
            else if(Grid.Children.Contains(icon))
            {
                Grid.Children.Remove(icon);
                Grid.Children.Add(progressBar);
            }
        }

        public double Value { get => progressBar.Value; set => progressBar.Value = value; }

        public void SetIcon(PackIconKind icon)
        {
            this.icon.Kind = icon;
        }
    }
}
