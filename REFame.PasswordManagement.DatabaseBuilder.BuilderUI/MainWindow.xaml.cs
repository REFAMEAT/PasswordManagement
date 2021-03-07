using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Microsoft.Data.SqlClient;
using REFame.PasswordManagement.DB.Entities.Base;

namespace REFame.PasswordManagement.DatabaseBuilder.BuilderUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            SqlConnectionStringBuilder stringBuilder;
            if (UserName.Text != string.Empty)
            {
                stringBuilder = new SqlConnectionStringBuilder
                {
                    InitialCatalog = DatabaseName.Text,
                    DataSource = ServerName.Text,
                    UserID = UserName.Text,
                    Password = PasswordBox.Password
                };
            }
            else
            {
                stringBuilder = new SqlConnectionStringBuilder
                {
                    InitialCatalog = DatabaseName.Text,
                    DataSource = ServerName.Text,
                    IntegratedSecurity = true
                };
            }

            try
            {
                new Database().Build<GenerateTable>(true, stringBuilder, Assembly.GetAssembly(typeof(GenerateTable)));
                MessageBox.Show(this, "Database created", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating the database: \n\r" + ex.Message, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}