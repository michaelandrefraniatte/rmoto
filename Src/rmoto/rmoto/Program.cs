using System;
using System.Diagnostics;
using System.IO;

namespace rmoto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rootPath = System.Windows.Forms.Application.StartupPath;
            Console.WriteLine(rootPath);
            UnblockFiles(rootPath);
            Console.WriteLine("done");
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                UnblockFiles(dir);
                Console.WriteLine("done");
            }
        }
        public static void UnblockFiles(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                return;
            }
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = $"Get-ChildItem -Path '{folderName}' -Recurse | Unblock-File",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
    }
}