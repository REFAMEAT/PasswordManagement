using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManagement.View
{
    /// <summary>
    ///     Interaction logic for Messagebox.xaml
    /// </summary>
    public partial class Messagebox : Window
    {
        internal Messagebox()
        {
            InitializeComponent();
        }

        internal static void Error(string message)
        {
            var mbox = new Messagebox
            {
                textBlock = {Text = $"An application Error occured:{Environment.NewLine}{message}"}
            };

            var button = new Button
            {
                Content = "OK",
                Margin = new Thickness(2)
            };

            button.Click += (sender, args) => mbox.Close();
            DockPanel.SetDock(button, Dock.Right);
            mbox.dockPanel.Children.Add(button);

            mbox.ShowDialog();
        }

        internal static bool ShowYesNo(string message, string caption)
        {
            var isYes = false;

            var mbox = new Messagebox
            {
                Title = caption,
                textBlock = {Text = message}
            };

            var buttonNo = new Button
            {
                Content = mbox.FindResource("No"),
                Width = 140,
                Style = mbox.FindResource("MaterialDesignFlatButton") as Style
            };

            var buttonYes = new Button
            {
                Content = "Yes",
                Width = 140
            };


            buttonNo.Click += (sender, args) => mbox.Close();
            buttonYes.Click += (sender, args) =>
            {
                isYes = true;
                mbox.Close();
            };

            DockPanel.SetDock(buttonNo, Dock.Left);
            DockPanel.SetDock(buttonYes, Dock.Right);

            mbox.dockPanel.Children.Add(buttonNo);
            mbox.dockPanel.Children.Add(buttonYes);

            mbox.ShowDialog();

            return isYes;
        }
    }
}