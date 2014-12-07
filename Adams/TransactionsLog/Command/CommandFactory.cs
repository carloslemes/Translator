using System;
using System.Collections.Generic;
using System.Linq;

namespace TransactionsLog.Command
{
    internal static class CommandFactory
    {
        private static readonly IEnumerable<ICommand> Commands = new List<ICommand>
                                                 {
                                                     new Assignment(),
                                                     new HowMany(),
                                                     new HowMuch(),
                                                     new Help(),
                                                     new LoadFile(),
                                                     new EasterEgg()
                                                 };
        
        
        public static ICommand Parse(string[] args)
        {
            foreach (var command in Commands.Where(command => command.Match(args)))
            {
                return command;
            }

            throw new ArgumentException("I'm sorry, but I'm afraid I can't do that. I can't understand the supply argument.");
        }
    }
}