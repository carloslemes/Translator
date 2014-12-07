using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsLog.Command
{
    class HowMuch : ICommand
    {
        private string[] _args;
        private Dictionary<string, string> _dataTable;

        public HowMuch()
        {
            
        }

        public HowMuch(string[] args)
        {
            _args = args;
        }

        public bool Match(string[] args)
        {
            if(args.Length < 4)
                return false;

            if (!args[0].ToLower().Equals(Operator.How.ToString().ToLower()))
                return false;

            if (!args[1].ToLower().Equals(Operator.Much.ToString().ToLower()))
                return false;

            if (!args[args.Length - 1].EndsWith("?"))
                return false;

            _args = args;
            return true;
        }

        public ICommand Analysis(string configurationFiles)
        {
            var newArgs = new List<string>();

            if (_args == null || _args.Length == 0)
                throw new InvalidOperationException("Command needs a list of arguments.");

            _dataTable = LoadFile.LoadDatFile(configurationFiles);

            if(!_dataTable.Any())
                throw new InvalidOperationException("There is no transaction log to compute that query.");

            _args[_args.Length - 1] = _args[_args.Length - 1].Replace("?", string.Empty);
            for (var index = 3; index < _args.Length; index++)
            {
                if(string.IsNullOrEmpty(_args[index])) 
                    continue;
                
                if(!_dataTable.Keys.Contains(_args[index]))
                    throw new ArgumentException("I have no idea what you are talking about");

                newArgs.Add(_args[index]);
            }

            _args = newArgs.ToArray();
            return this;
        }

        public void Run(Action<string> outPut)
        {
            var arguments =_args.Take(_args.Length).Aggregate((first, second) => String.Format("{0} {1}", first, second));
            
            var roman = _args.ToRoman(_dataTable);
            
            outPut(arguments + " is " + roman.ToInt32());
        }
    }
}