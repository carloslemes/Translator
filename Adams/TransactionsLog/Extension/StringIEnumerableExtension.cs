using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class StringIEnumerableExtension
    {
        public static string ToString(this IEnumerable<string> array)
        {
            var builder = new StringBuilder();
            foreach (var value in array)
            {
                builder.AppendFormat("{0} ", value);
            }
            return builder.ToString();
        }

        public static Roman ToRoman(this IEnumerable<string> symbols, IReadOnlyDictionary<string, string> dataTable)
        {
            var result = new StringBuilder();
            foreach (var symbol in symbols)
            {
                result.Append(dataTable[symbol]);
            }
            return result.ToString();
        }
    }
}