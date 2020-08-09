using System;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace PasswordManagement.SetupUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker;
        public static Dispatcher currentDispatcher;

        public MainWindow()
        {
            InitializeComponent();
            currentDispatcher = Dispatcher.CurrentDispatcher;
            worker = new BackgroundWorker();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += WorkerOnDoWork;
            worker.RunWorkerAsync();

            buttonStart.IsEnabled = false;
            worker.RunWorkerCompleted += (sendere, ev) => Dispatcher.Invoke(() => buttonStart.IsEnabled = true);
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            PasswordManagementSetup setup = new PasswordManagementSetup(Globals.TotalDownloadPathFtp, Globals.ManagerPath);

            if (!NetCoreDownloader.CoreVersionInstalled() || true)
            {
                SetValueThreadSafe(NetVersionCheck, 100);
                NetCoreDownloader.DownloadCore((senderVersionDownload, argsVersionDownload) => SetValueThreadSafe(NetVersionDownload, argsVersionDownload.ProgressPercentage));
            }
            else
            {
                NoActionNeeded(NetVersionCheck);
                NoActionNeeded(NetVersionDownload);
            }

            if (setup.NeedNewProductVersion())
            {
                setup.DownloadPasswordManager(((senderPwDownload, argsPwDownload) => SetValueThreadSafe(PasswordManagementDownload, argsPwDownload.ProgressPercentage)));
            }
            else
            {
                NoActionNeeded(PasswordManagementDownload);
                NoActionNeeded(PasswordManagementCheck);
            }

            Shortcut.Create(Path.Combine(Globals.ManagerPath, "PasswordManagement.exe"));
            NoActionNeeded(Other);
        }

        private void NoActionNeeded(ProgressBarDone progressBar)
        {
            Dispatcher.Invoke(() =>
            {
                progressBar.SetIcon(PackIconKind.HandOkay);
                progressBar.Value = 100;
            });
        }

        private void SetValueThreadSafe(ProgressBarDone progressBar, double value) =>
            Dispatcher.Invoke(() => progressBar.Value = value);

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            Close();
        }
    }
}
