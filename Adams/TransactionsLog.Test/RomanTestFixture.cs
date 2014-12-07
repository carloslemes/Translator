using System;
using NUnit.Framework;

namespace TransactionsLog.Test
{
    [TestFixture]
    public class RomanTestFixture
    {
        private static readonly object[] CenarioDeConversoesValidas =
        {
            new object[] {"I", 1},
            new object[] {"III", 3},
            new object[] {"IV", 4},
            new object[] {"V", 5},
            new object[] {"IX", 9},
            new object[] {"X", 10},
            new object[] {"XXX", 30}, 
            new object[] {"XL", 40},
            new object[] {"L", 50},
            new object[] {"XC", 90},
            new object[] {"C", 100},
            new object[] {"CCC", 300},
            new object[] {"CD", 400},
            new object[] {"D", 500},
            new object[] {"CM", 900},
            new object[] {"M", 1000},
            new object[] {"MMM", 3000},
            new object[] {"MMVI", 2006},
            new object[] {"MCMXLIV", 1944},
            new object[] {"XXXIX", 39},
            new object[] {"MCMIII", 1903},
        };

        private static readonly object[] CenarioDeConversoesInvalidas =
        {
            new object[]{"A"},
            new object[]{"MCMZXLV"},
            new object[]{"XXXMX"},
            new object[]{"XXXI"},
            new object[]{"XXXX"},
            new object[]{"DD"},
            new object[]{"LL"},
            new object[]{"VV"},
        };

        private static readonly object[] CenarioDeSubtracoesInvalidas =
        {
            new object[] {"L","I"},
            new object[] {"D","X"},
            new object[] {"X","C"},
            new object[] {"C","V"},
            new object[] {"C","L"},
            new object[] {"C","D"},
        };

        [Test, TestCaseSource("CenarioDeConversoesValidas")]
        public void TestarConversoesParaInteiro(String romanText, Int32 expected)
        {
            Roman roman = romanText;

            Int32 converted = roman;

            Assert.AreEqual(expected, converted);
        }

        [Test, TestCaseSource("CenarioDeConversoesInvalidas")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestarConversaoCaracterInvalido(String romanInvalidText)
        {
            Roman roman = romanInvalidText;
        }

        [Test, TestCaseSource("CenarioDeConversoesValidas")]
        public void TestarConversaoNumericoValido(String expected, Int32 number)
        {
            Roman roman = number;

            Assert.AreEqual(roman.ToString(), expected);
        }

        [Test]
        public void TestarSoma()
        {
            Roman foo = "X";

            Roman bar = "I";

            var sum = foo + bar;

            Roman expected = "XI";

            Assert.AreEqual(sum, expected);
        }

        [Test]
        public void TestarSubtracaoValida()
        {
            Roman foo = "X";

            Roman bar = "I";

            var sum = foo - bar;

            Roman expected = "IX";

            Assert.AreEqual(sum, expected);
        }

        [Test, TestCaseSource("CenarioDeSubtracoesInvalidas")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestarSubtracaoInvalida(String romanText1, String romanText2)
        {
            Roman foo = romanText1;

            Roman bar = romanText2;

            var sum = foo - bar;
        }
    }
}
