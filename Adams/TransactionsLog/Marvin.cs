using System;
using System.IO;
using TransactionsLog.Command;

namespace TransactionsLog
{
    static class Marvin
    {
        private static readonly string LogDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Marvin", "MarvinTransaction.log");
        private static readonly string DatDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Marvin", "MarvinTransaction.dat");

        public static void Run(string[] args)
        {
            if (args.Length == 0)
            {
                ShowDescription();
                return;
            }

            try
            {
                LoadFile(LogDirectory);

                LoadFile(DatDirectory);
                
                CommandFactory
                    .Parse(args)
                    .Analysis(DatDirectory)
                    .Run(Console.WriteLine);

                using (var logWriter = new StreamWriter(LogDirectory, true))
                {
                    var dateTime = DateTime.Now;
                    logWriter.WriteLine("[{0}] {1}", dateTime.ToString("s"), StringIEnumerableExtension.ToString(args));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void LoadFile(string fileDirectory)
        {
            try
            {
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileDirectory));
                }

                if (File.Exists(fileDirectory))
                    return;

                var file = File.Create(fileDirectory);
                file.Dispose();
            }
            catch (Exception exception)
            {
                throw new ApplicationException(string.Format("Sorry, but I can't create my log file at {0} because {1}", fileDirectory, exception.Message), exception);
            }
        }

        private static void ShowDescription()
        {
            Console.WriteLine("dont' panic!");
            Console.WriteLine("Marvin is a trial application to help you log your intergalatic transactions.");
            Console.WriteLine("Marvin keeps yours transactions log at " + LogDirectory);
            Console.WriteLine("Use -h --help to get help.");
        }
    }
}