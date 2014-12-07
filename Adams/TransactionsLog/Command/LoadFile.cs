using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TransactionsLog.Command
{
    class LoadFile : ICommand
    {
        private string[] _args;

        public LoadFile()
        {
            
        }

        public LoadFile(string[] args)
        {
            _args = args;
        }

        public bool Match(string[] args)
        {
            var firstArgument = args.First();

            _args = args;

            return (firstArgument.Equals("-f") || firstArgument.Equals("--file"));
        }

        public ICommand Analysis(string configurationFiles)
        {
            if (_args == null || _args.Length == 0)
                throw new InvalidOperationException("Command needs a list of arguments.");

            if (_args.Length == 1)
                throw new InvalidOperationException("Command needs the location of file with notes.");

            if (_args.Length > 2)
                throw new InvalidOperationException("Command accepts just one parameter, please insert only de notes directory.");

            if (!File.Exists(_args[1]))
                throw new ArgumentException(string.Format("The file {0} cannot be found, please confirm the directory.", _args[1]));

            return this;
        }

        public void Run(Action<string> outPut)
        {
            var notes = File.ReadLines(_args[1]);

            foreach (var note in notes)
            {
                Marvin.Run(note.Split(' '));
            }
        }

        public static Dictionary<string, string> LoadDatFile(string dataFileDirectory)
        {
            var dataFile = File.ReadLines(dataFileDirectory);

            var dictionary = new Dictionary<string, string>();
            foreach (var strings in dataFile.Select(data => data.Split(';')))
            {
                if (dictionary.ContainsKey(strings[0]))
                    dictionary[strings[0]] = strings[1];
                else
                    dictionary.Add(strings[0], strings[1]);
            }
            return dictionary;
        }
    }
}