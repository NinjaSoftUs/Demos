using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using log4net;
using NinjaSoft.CommonInfrastructure.Extenstions;
using NinjaSoft.CommonInfrastructure.Models;
using NinjaSoft.CommonInfrastructure.Utils;

//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace NinjaSoft.DirectoryStats
{


     class Program
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program).FullName);
        private static Stopwatch _stopWatch;

        private static void Main(string[] args)
        {
            SetConsoleMessageType(MessageType.Defalut);
            _stopWatch = new Stopwatch();

            try
            {
                if (args.Length < 1)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine()
                        .AppendLine($"Usage: [dir1] [dir2] [dir3]")
                        .AppendLine($"For Example: ")
                        .Append("DirectoryStats C:\\temp \"C:\\Program Files (x86)\" C:\\Users")
                        .AppendLine($"")
                        .AppendLine($"Note:You can enter up to three different paths")
                        .AppendLine($"and if your path contains spaces you have surround the argument with \" \"");

                    Console.WriteLine(sb.ToString());
                    return;
                }
                if (args.Length > 3)
                {
                    throw new ArgumentException("DirectoryStats only accepts a maximum of three directories");
                }

                var directoryInfos = new List<DirectoryInfo>();
                foreach (var arg in args)
                {
                    var dirInfo = new DirectoryInfo(arg);
                    if (!dirInfo.Exists)
                    {
                        throw new DirectoryNotFoundException($"The directory \"{arg}\" dose not Exists");
                    }

                    directoryInfos.Add(dirInfo);
                }

                //all args are parsed now lets run the app
                using (var helper = new DirStatsHelper())
                {
                    _stopWatch.Start();
                    var t = helper.GetDirStatsAsync(directoryInfos.ToArray());
                    _stopWatch.Stop();
                    DispalyResults(t.Result);
                }
            }
            catch (ArgumentException e)
            {
                DisplayError(e);
                _log.Error(e.Message);
                _log.Debug(e.StackTrace);
                
            }
            catch (DirectoryNotFoundException e)
            {
                DisplayError(e);
                _log.Debug(e.StackTrace);
            }
            catch (Exception e)
            {
                DisplayError(e);
                _log.Debug(e.StackTrace);
            }
        }

        private static void DispalyResults(DirStatsSummery result)
        {
            var exeTime = _stopWatch.Elapsed.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture);

            Console.WriteLine();
            if (result.HasErrors)
            {
                SetConsoleMessageType(MessageType.Warning);

                Console.WriteLine("Job Finished With Errors (see error log for more information)");
            }
            else
            {
                SetConsoleMessageType(MessageType.Good);
                Console.WriteLine("Job Finished Successfully");
            }

            SetConsoleMessageType(MessageType.Defalut);
            Console.WriteLine($"Execution Time: {exeTime}");
            Console.WriteLine();
            Console.WriteLine(result.ToOutputString());
        }

        private static void DisplayError(Exception e)
        {
            Console.WriteLine();
            SetConsoleMessageType(MessageType.Error);
            Console.Write("Error:");
            SetConsoleMessageType(MessageType.Warning);
            Console.WriteLine(e.Message);
            SetConsoleMessageType(MessageType.Defalut);
        }

        private static void SetConsoleMessageType(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Error:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case MessageType.Warning:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case MessageType.Good:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }

    public enum MessageType
    {
        Defalut,
        Warning,
        Error,
        Good
    }
}