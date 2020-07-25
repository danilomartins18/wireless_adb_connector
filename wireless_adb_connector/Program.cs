using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace wireless_adb_connector
{
    class Program
    {
        const string IPDEVICE = "192.168.0.100";

        private static Process process = null;

        private static Process Process
        {
            get
            {
                if (process == null)
                {
                    process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = false;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }

                return process;
            }
        }

        static void Main(string[] args)
        {
            // INFO : Nox app player installed in drive D
            var lines = new List<string>()
            {
                @"adb tcpip 5555",
                @$"adb connect {IPDEVICE}:5555",
            };

            // Run and wait for exit
            ProcessLines(lines);
            Console.WriteLine("\r\nPress a key");
            Console.ReadKey();
        }

        private static void ProcessLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Process.StandardInput.WriteLine(line);
                Process.StandardInput.Flush();
            }

            Process.StandardInput.Close();
            Process.WaitForExit();

            var outputs = Process.StandardOutput.ReadToEnd().Split('\n');
            Console.WriteLine(outputs[outputs.Length - 3]);
        }
    }
}
