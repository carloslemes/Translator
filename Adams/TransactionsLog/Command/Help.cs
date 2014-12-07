using System;
using System.Linq;

namespace TransactionsLog.Command
{
    class Help : ICommand
    {
        public bool Match(string[] args)
        {
            var firstArgument = args.First();

            return (firstArgument.Equals("-h") || firstArgument.Equals("--help"));
        }

        public ICommand Analysis(string configurationFiles)
        {
            return this;
        }

        public void Run(Action<string> outPut)
        {
            ShowHelp(outPut);
        }

        void ShowHelp(Action<string> outAction)
        {
            outAction("With Marvin's intergalactic transactions log you don't panic.");
            outAction("Just enter your notes like this: ");
            outAction("     glob is I");
            outAction("     prok is V");
            outAction("     pish is X");
            outAction("     tegj is L");
            outAction("     glob glob Silver is 34 Credits");
            outAction("     glob prok Gold is 57800 Credits");
            outAction("     pish pish Iron is 3910 Credits");
            outAction("And when you need to know your status just ask me that:");
            outAction("     how much is pish tegj glob glob ?");
            outAction("     how many Credits is glob prok Silver ?");
            outAction("     how many Credits is glob prok Gold ?");
            outAction("     how many Credits is glob prok Iron ?");
            outAction("Remeber intergalactic transactions follows similar convention to the roman numerals, ");
            outAction("they are based in seven symbols, as follows:");
            outAction("     Symbol Value");
            outAction("       I      1");
            outAction("       V      5");
            outAction("       X      10");
            outAction("       L      50");
            outAction("       C      100");
            outAction("       D      500");
            outAction("       M      1,000");
            outAction("You can load your notes from one file. To do that is easy!");
            outAction("Just enter -f or --file follow with the file directory.");
            outAction("     Example Marvin -f C:\\user\\marvin\\Documents\\myNotes.txt");
        }
    }
}