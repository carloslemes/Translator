using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsLog.Command
{
    class HowMany : ICommand
    {
        private string[] _args;
        private string _unit;
        private Dictionary<string, string> _dataTable;

        public HowMany()
        {
            
        }

        public HowMany(string[] args)
        {
            _args = args;
        }

        public bool Match(string[] args)
        {
            if (args.Length < 5)
                return false;
            
            if (!args[0].ToLower().Equals(Operator.How.ToString().ToLower()))
                return false;
                
            if (!args[1].ToLower().Equals(Operator.Many.ToString().ToLower()))
                return false;

            if (!Enum.IsDefined(typeof (Unit), args[2]))
                return false;

            if (!args[args.Length - 1].EndsWith("?"))
                return false;

            _args = args;
            return true;
        }

        public ICommand Analysis(string configurationFiles)
        {
            if (_args == null || _args.Length == 0)
                throw new InvalidOperationException("Command needs a list of arguments.");

            _dataTable = LoadFile.LoadDatFile(configurationFiles);

            if (!_dataTable.Any())
                throw new InvalidOperationException("There is no transaction log to compute that query.");

            var tokenPosition = Array.LastIndexOf(_args, Operator.Is.ToString().ToLower());

            if (tokenPosition == 0)
                throw new ArgumentException("I have no idea what you are talking about");

            _unit = _args.Take(tokenPosition).ToArray().Last();

            if (!Enum.IsDefined(typeof(Unit), _unit))
                throw new ArgumentException("I have no idea what you are talking about");

            var newArgs = new List<string>();

            _args[_args.Length - 1] = _args[_args.Length - 1].Replace("?", string.Empty);
            for (var index = tokenPosition + 1; index < _args.Length; index++)
            {
                if (string.IsNullOrEmpty(_args[index]))
                    continue;

                if (!_dataTable.Keys.Any(k => k.Contains(_args[index])))
                    throw new ArgumentException("I have no idea what you are talking about");

                newArgs.Add(_args[index]);
            }
            
            _args = newArgs.ToArray();
            return this;
        }
        
        public void Run(Action<string> outPut)
        {
            var arguments = _args.Take(_args.Length).Aggregate((first, second) => String.Format("{0} {1}", first, second));

            var unit = _args.Last();

            var storageData = _dataTable.First(f => f.Key.Contains(unit));

            var storage = storageData.Key.Split(' ').ToList();
                storage.Remove(unit);
            
            var query = _args.ToList();
                query.Remove(unit);

            var romanStorage = storage.ToRoman(_dataTable);

            var romanQuery = storage.ToRoman(_dataTable);

            var ratio = romanQuery.ToDouble() / romanStorage.ToDouble();

            var queryAnswer = ratio*int.Parse(storageData.Value);

            outPut(arguments + " is " + queryAnswer + " " + _unit);
        }
    }
}