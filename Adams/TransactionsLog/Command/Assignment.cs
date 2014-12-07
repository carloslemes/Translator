using System;
using System.IO;
using System.Linq;

namespace TransactionsLog.Command
{
    class Assignment : ICommand
    {
        private string[] _args;
        private string _configurationFiles;

        private string _unit;
        private string _value;

        public Assignment()
        {
            
        }

        public Assignment(string[] args)
        {
            _args = args;
        }

        public bool Match(string[] args)
        {
            if (args[args.Length - 1].EndsWith("?"))
                return false;
            
            var operatorToken = Operator.Is.ToString().ToLower();
            
            if (!args.Any(t => t.Equals(operatorToken)))
                return false;

            if (args[args.Length - 1].Equals(operatorToken))
                return false;
            
            _args = args;
            return true;
        }

        public ICommand Analysis(string configurationFiles)
        {
            if (_args == null || _args.Length == 0)
                throw new InvalidOperationException("Command needs a list of arguments.");

            var tokenPosition = Array.LastIndexOf(_args, Operator.Is.ToString().ToLower());

            _unit = _args.Take(tokenPosition).Aggregate((first, second) => String.Format("{0} {1}", first, second));
            ParseUnit(_unit);

            _value = _args.ElementAt(tokenPosition + 1);
            ParseValue(_value);

            _configurationFiles = configurationFiles;
            return this;
        }
        
        private static void ParseUnit(string unit)
        {
            if (unit == null) 
                throw new ArgumentNullException("unit");

            if (unit.ToLower().Contains(Operator.How.ToString().ToLower()))
                throw new ArgumentException("The unit cannot contains the reserved keyword How");

            if (unit.ToLower().Contains(Operator.Many.ToString().ToLower()))
                throw new ArgumentException("The unit cannot contains the reserved keyword Many");

            if (unit.ToLower().Contains(Operator.Much.ToString().ToLower()))
                throw new ArgumentException("The unit cannot contains the reserved keyword Much");
        }

        private static void ParseValue(string value)
        {
            int intValue;
            Roman romanValue;
            
            if(!Int32.TryParse(value, out intValue))
                if(!Roman.TryParse(value, out romanValue))
                    throw new ArgumentException(string.Format("The value \"{0}\" is not valid. It's neither an integer or roman. ", value));

        }

        public void Run(Action<string> outPut)
        {   
            using (var datWriter = new StreamWriter(_configurationFiles, true))
            {
                datWriter.WriteLine("{0};{1}", _unit, _value);
            }
        }
    }
}