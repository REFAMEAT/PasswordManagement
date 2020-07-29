using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Documents;
using PasswordManager.Setup;
using File = System.IO.File;

namespace PasswordManagement.SetupUI
{
    internal class PasswordManagementSetup
    {
        private readonly string remote;
        private readonly string local;
        public double FileSize { get; set; }
        public List<string> Files { get; set; }

        public PasswordManagementSetup(string remote, string local)
        {
            this.remote = remote;
            this.local = local;

            Files = GetFolder();
            FileSize = GetFolderSize(Files);
        }

        internal void DownloadPasswordManager(DownloadProgressChangedEventHandler changed)
        {
            if (!Directory.Exists(local))
            {
                Directory.CreateDirectory(local);
            }

            if (Directory.GetFiles(local).Any())
            {
                foreach (string enumerateFile in Directory.EnumerateFiles(local))
                {
                    File.Delete(enumerateFile);
                }
            }

            using (WebClient client = new WebClient())
            {
                client.DownloadDataCompleted += ClientOnDownloadDataCompleted;
                client.DownloadProgressChanged += changed;

                foreach (string file in Files)
                {
                    this.file = file;
                    string filePath = Globals.TotalDownloadPath + "\\" + file;

                    Console.WriteLine("Downloading " + file);
                    client.Credentials = new NetworkCredential("pwtester", "pwtester");

                    Task task = client.DownloadDataTaskAsync(filePath);
                    Task.WaitAll(task);
                }
            }
        }

        private bool fileCreating;
        private string file;

        private void ClientOnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            fileCreating = true;
            string localFilePath = Globals.ManagerPath + "/" + file;
            byte[] fileData = e.Result;
            
            FileStream fileStream = File.Create(localFilePath);
            fileStream.Write(fileData, 0, fileData.Length);
            fileCreating = false;
        }

        private List<string> GetFolder()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remote);
            request.Credentials = new NetworkCredential("pwtester", "pwtester");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            List<string> files = new List<string>();

            do
            {
                string readLine = reader.ReadLine();
                if (readLine == null)
                {
                    break;
                }
                else
                {
                    files.Add(readLine.Replace(Globals.ProductVersion + "/", null));
                }
            } while (true);

            return files;
        }

        private double GetFolderSize(List<string> files)
        {
            double size = 0;

            foreach (string file in files)
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{remote}/{file}");
                request.Credentials = new NetworkCredential("pwtester", "pwtester");
                request.Method = WebRequestMethods.Ftp.GetFileSize;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                size += response.ContentLength;
            }

            //FtpWebRequest request = (FtpWebRequest) WebRequest.Create(Globals.TotalDownloadPathFtp);
            //request.Credentials = new NetworkCredential("pwtester", "pwtester");
            //request.Method = WebRequestMethods.Ftp.GetFileSize;

            //FtpWebResponse response = (FtpWebResponse) request.GetResponse();
            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);

            return size;
        }

        public bool NeedNewProductVersion()
        {
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(local, "PasswordManagement.exe"));

            if (File.Exists(Path.Combine(local, "PasswordManagement.exe")))
            {
                return fileVersionInfo.ProductVersion != Globals.ProductVersion; 
            }

            return true;
        }
    }
}
