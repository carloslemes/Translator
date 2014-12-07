using System;
using System.Linq;

namespace TransactionsLog.Command
{
    class EasterEgg :ICommand
    {
        public bool Match(string[] args)
        {
            var query = args.Aggregate((f, s) => String.Format("{0} {1}", f, s));

            return query.Equals("the answer to life the universe and everything");
        }

        public ICommand Analysis(string configurationFiles)
        {
            return this;
        }

        public void Run(Action<string> outPut)
        {
            outPut("42");
        }
    }
}
