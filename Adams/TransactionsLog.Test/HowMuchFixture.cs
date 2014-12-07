using System;
using NUnit.Framework;

namespace TransactionsLog.Test
{
    [TestFixture]
    public class HowMuchFixture
    {
        private static readonly object[] ComandosMatchPossiveis =
        {
            new object[] {"how much is", false},
            new object[] {"how many is pish tegj glob glob ?", false},
            new object[] {"how much is pish tegj glob glob ?", true},
            new object[] {"how much is pish tegj glob glob?", true},
            new object[] {"how much is pish tegj glob glob ", false},
            new object[] {"how much wood could a woodchuck chuck if a woodchuck could chuck wood ?", true},
            new object[] {"how much wood could a woodchuck chuck if a woodchuck could chuck wood?", true},
            new object[] {"how much wood could a woodchuck chuck if a woodchuck could chuck wood", false},
        };

        [Test, TestCaseSource("ComandosMatchPossiveis")]
        public void TestarMatchDoComando(string commandText, bool expected)
        {
            var command = new Command.HowMuch();

            var haveMatch = command.Match(commandText.Split(' '));

            Assert.AreEqual(haveMatch, expected);
        }
    }

    [TestFixture]
    public class HowManyFixture
    {
        private static readonly object[] ComandosMatchPossiveis =
        {
            new object[] {"how many Credits is", false},
            new object[] {"foo many Credits is", false},
            new object[] {"how much Credits is", false},
            new object[] {"how many Dollars is glob prok Silver ?", false},
            new object[] {"how many Credits is glob prok Silver", false},

            new object[] {"how many Roman is glob prok Silver ?", true},
            new object[] {"how many Credits is glob prok Silver ?", true},
            
            new object[] {"how many Roman is glob prok Gold ?", true},
            new object[] {"how many Credits is glob prok Gold ?", true},

            new object[] {"how many Roman is glob prok Iron ?", true},
            new object[] {"how many Credits is glob prok Iron ?", true},

            new object[] {"how many Roman is glob prok Silver?", true},
            new object[] {"how many Credits is glob prok Silver?", true},

            new object[] {"how many Roman is glob prok Gold?", true},
            new object[] {"how many Credits is glob prok Gold?", true},

            new object[] {"how many Roman is glob prok Iron?", true},
            new object[] {"how many Credits is glob prok Iron?", true},
        };

        [Test, TestCaseSource("ComandosMatchPossiveis")]
        public void TestarMatchDoComando(string commandText, bool expected)
        {
            var command = new Command.HowMany();

            var haveMatch = command.Match(commandText.Split(' '));

            Assert.AreEqual(haveMatch, expected);
        }
    }
}