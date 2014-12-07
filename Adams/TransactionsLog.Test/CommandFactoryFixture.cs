using System;
using NUnit.Framework;
using TransactionsLog.Command;

namespace TransactionsLog.Test
{
    [TestFixture]
    public class CommandFactoryFixture
    {
        private static readonly object[] ComandosPossiveis =
        {
            new object[] {"glob is I", typeof(Assignment)},
            new object[] {"prok is V", typeof(Assignment)},
            new object[] {"pish is X", typeof(Assignment)},
            new object[] {"tegj is L", typeof(Assignment)},
            new object[] {"glob glob Silver is 34 Credits", typeof(Assignment)},
            new object[] {"glob prok Gold is 57800 Credits", typeof(Assignment)},
            new object[] {"pish pish Iron is 3910 Credits", typeof(Assignment)},
            new object[] {"how much is pish tegj glob glob ?", typeof(HowMuch)},
            new object[] {"how many Credits is glob prok Silver ?", typeof(HowMany)},
            new object[] {"how many Credits is glob prok Gold ?", typeof(HowMany)},
            new object[] {"how many Credits is glob prok Iron ?", typeof(HowMany)},
            new object[] {"how much wood could a woodchuck chuck if a woodchuck could chuck wood ?", typeof(HowMuch)},

        };

        [Test, TestCaseSource("ComandosPossiveis")]
        public void TestaArgumentosValidosDeEntrada(string commandArg, Type expectedCommandType)
        {
            var comand = CommandFactory.Parse(commandArg.Split(' '));

            Assert.IsInstanceOf(expectedCommandType, comand);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestarArgumentoInvalidosDeEntrada()
        {
            var comand = CommandFactory.Parse("Foo Bar".Split(' '));
        }
    }
}