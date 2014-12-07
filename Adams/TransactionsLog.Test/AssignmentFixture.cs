using System;
using NUnit.Framework;

namespace TransactionsLog.Test
{
    [TestFixture]
    public class AssignmentFixture
    {
        private static readonly object[] ComandosMatchPossiveis =
        {
            new object[] {"glob is I", true},
            new object[] {"glob I", false},
            new object[] {"glob is", false},
            new object[] {"glob glob Silver is 34 Credits", true},
            new object[] {"glob prok Gold is 57800 Credits", true},
            new object[] {"pish pish Iron is 3910 Credits", true},
        };

        private static readonly object[] ComandosAnaliesesPossiveis =
        {
            new object[] {null, typeof(InvalidOperationException) },
            new object[] {"How is I", typeof(ArgumentException)},
            new object[] {"Much is I", typeof(ArgumentException)},
            new object[] {"Many is I", typeof(ArgumentException)},
            new object[] {"glob is A", typeof(ArgumentException)},
            new object[] {"glob is 999999999999999", typeof(ArgumentException)},
        };


        [Test, TestCaseSource("ComandosMatchPossiveis")]
        public void TestarMatchDoComando(string commandText, bool expected)
        {
            var command = new Command.Assignment();

            var haveMatch = command.Match(commandText.Split(' '));

            Assert.AreEqual(haveMatch, expected);
        }

        [Test, TestCaseSource("ComandosAnaliesesPossiveis")]
        public void TestarAnalysisDoComando(string commandText, Type expected)
        {
            var command = new Command.Assignment(commandText !=null? commandText.Split(' '):null);

            Assert.Throws(expected, () => command.Analysis(string.Empty ));
        }
    }
}