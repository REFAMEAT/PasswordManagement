﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.DB;
using REFame.PasswordManagement.DB.Entities.Base;
using REFame.PasswordManagement.File;
using SqlConnectionStringBuilder = Microsoft.Data.SqlClient.SqlConnectionStringBuilder;

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

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(stringBuilder.ToString());
            BuildDatabase(optionsBuilder.Options);
        }

        private void SqliteButton_Click(object sender, RoutedEventArgs e)
        {
            SqLiteConnectionStringBuilder stringBuilder 
                = new SqLiteConnectionStringBuilder(new FolderProvider());

            BuildDatabase(new DbContextOptionsBuilder()
                .UseSqlite(stringBuilder.Create())
                .Options);
        }

        private void BuildDatabase(DbContextOptions options)
        {
            try
            {
               new Database().Build<GenerateTable>(true, options, Assembly.GetAssembly(typeof(GenerateTable)));
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