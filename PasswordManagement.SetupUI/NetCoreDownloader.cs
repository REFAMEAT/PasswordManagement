using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Threading;
using MaterialDesignThemes.Wpf.Transitions;

namespace PasswordManagement.SetupUI
{
    class NetCoreDownloader
    {
        internal static bool CoreVersionInstalled()
        {
            // looking for .NET Core Version
            Collection<PSObject> x = PowerShell.Create()
                .AddCommand("dotnet")
                .AddArgument("--version")
                .Invoke();

            // if version is not .net 3.1 download new one
            return x[0].ToString() == Globals.NetCoreVersion;
        }

        internal static void DownloadCore(DownloadProgressChangedEventHandler progressChanged)
        {
           WebClient request = new WebClient
            {
                Credentials = new NetworkCredential("pwtester", "pwtester")
            };
            request.DownloadProgressChanged += progressChanged;
            request.DownloadDataCompleted += RequestOnDownloadDataCompleted;
            request.DownloadDataAsync(new Uri(Globals.NetCoreRuntimeDownloadPath));

            while (request.IsBusy)
            {
                Thread.Sleep(100);
            }
        }

        private static void RequestOnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            byte[] fileData = e.Result;

            FileStream fs = File.Create($"{Globals.ManagerPath}\\DotNetCoreSetup.exe");

            fs.Write(fileData, 0, fileData.Length);
            fs.Close();

            Process.Start(fs.Name);
        }
    }
}