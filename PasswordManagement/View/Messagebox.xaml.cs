using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManagement.View
{
    /// <summary>
    /// Interaction logic for Messagebox.xaml
    /// </summary>
    public partial class Messagebox : Window
    {
        public Messagebox()
        {
            InitializeComponent();
        }

        public static void Error(string message)
        {
            Messagebox mbox = new Messagebox();
            mbox.textBlock.Text = $"An application Error occured:{Environment.NewLine}{message}";

            Button button = new Button();
            button.Content = "OK";
            button.Margin = new Thickness(2);
            button.Click += (sender, args) => mbox.Close();
            DockPanel.SetDock(button, Dock.Right);
            mbox.dockPanel.Children.Add(button);

            mbox.ShowDialog();
        }

        public static bool ShowYesNo(string message, string caption)
        {
            bool isYes = false;

            Messagebox mbox = new Messagebox();
            mbox.Title = caption;
            mbox.textBlock.Text = message;

            Button buttonNo = new Button();
            Button buttonYes = new Button();

            buttonNo.Content = "No";
            buttonYes.Content = "Yes";

            buttonNo.Click += (sender, args) => mbox.Close();
            buttonYes.Click += (sender, args) =>
            {
                isYes = true;
                mbox.Close();
            };
            
            buttonNo.Width = 140;
            buttonYes.Width = 140;

            DockPanel.SetDock(buttonNo, Dock.Left);
            DockPanel.SetDock(buttonYes, Dock.Right);

            mbox.dockPanel.Children.Add(buttonNo);
            mbox.dockPanel.Children.Add(buttonYes);

            buttonNo.Style = mbox.FindResource("MaterialDesignFlatButton") as Style;

            mbox.ShowDialog();

            return isYes;
        }
    }
}
