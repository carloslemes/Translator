using System;

namespace TransactionsLog.Command
{
    interface ICommand 
    {
        bool Match(string[] args);

        ICommand Analysis(String configurationFiles);

        void Run(Action<String> outPut);
    }
}