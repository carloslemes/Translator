using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace System
{
    public struct Roman : IComparable, IFormattable, IConvertible, IComparable<Roman>, IEquatable<Roman>
    {
        private struct RomanSymbolConfiguration
        {
            public RomanSymbolConfiguration(int value, int succetionTimes, int maxOccurs) : this()
            {
                Value = value;
                SuccetionTimes = succetionTimes;
                MaxOccurs = maxOccurs;

            }

            public int Value { get; private set; }
            public int SuccetionTimes { get; private set; }
            public int MaxOccurs { get; private set; }
        }
        
        public override int GetHashCode()
        {
            return (_internalTextValue != null ? _internalTextValue.GetHashCode() : 0);
        }

        private static readonly Dictionary<char, RomanSymbolConfiguration> Table = new Dictionary<char, RomanSymbolConfiguration>
                     {
                         {'I', new RomanSymbolConfiguration(1,3,0)},
                         {'V', new RomanSymbolConfiguration(5,0,1)},
                         {'X', new RomanSymbolConfiguration(10,3,0)},
                         {'L', new RomanSymbolConfiguration(50,0,1)},
                         {'C', new RomanSymbolConfiguration(100,3,0)},
                         {'D', new RomanSymbolConfiguration(500,0,1)},
                         {'M', new RomanSymbolConfiguration(1000,3,0)},
                     };

        private readonly string _internalTextValue;

        private int InternalIntValue
        {
            get { return ConvertToInt(_internalTextValue); }
        }

        private Roman(string value)
        {
            foreach (var caracter in value)
            {
                CheckTable(caracter);
            }

            CheckSymbolStructure(value);

            _internalTextValue = value;
        }

        private Roman(char value)
        {
            CheckTable(value);
            
            _internalTextValue = value.ToString(CultureInfo.InvariantCulture);
        }

        private Roman(int value)
        {
            if (Table.Values.All(c => c.Value != value))
            {
                _internalTextValue = ConvertToRoman(value);
            }
            else
            {
                var simpleNumber = Table.First(e => e.Value.Value == value);

                _internalTextValue = simpleNumber.Key.ToString(CultureInfo.InvariantCulture);
            }
        }

        public static bool TryParse(string value, out Roman romanValue)
        {
            try
            {
                romanValue = Parse(value);

                return true;
            }
            catch (Exception)
            {
                romanValue = 'I';

                return false;
            }
        }

        public static Roman Parse(string value)
        {
            return value;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is Roman))
                throw new ArgumentException("");
            
            var r = (Roman)obj;
            if (this < r)
                return -1;
            return this > r ? 1 : 0;
        }

        public int CompareTo(Roman other)
        {
            if (this < other)
                return -1;
            return this > other ? 1 : 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Roman && Equals((Roman)obj);
        }

        public bool Equals(Roman other)
        {
            return this == other;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Convert.ToString(_internalTextValue);
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Decimal;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(InternalIntValue);
        }

        public char ToChar()
        {
            return ToChar(CultureInfo.CurrentCulture);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_internalTextValue);
        }

        public sbyte ToSByte()
        {
            return ToSByte(CultureInfo.CurrentCulture);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(InternalIntValue);
        }

        public byte ToByte()
        {
            return ToByte(CultureInfo.CurrentCulture);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(InternalIntValue);
        }

        public short ToInt16()
        {
            return ToInt16(CultureInfo.CurrentCulture);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(InternalIntValue);
        }

        public ushort ToUInt16()
        {
            return ToUInt16(CultureInfo.CurrentCulture);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(InternalIntValue);
        }

        public int ToInt32()
        {
            return ToInt32(CultureInfo.CurrentCulture);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return InternalIntValue;
        }

        public uint ToUInt32()
        {
            return ToUInt32(CultureInfo.CurrentCulture);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(InternalIntValue);
        }

        public long ToInt64()
        {
            return ToInt64(CultureInfo.CurrentCulture);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(InternalIntValue);
        }

        public ulong ToUInt64()
        {
            return ToUInt64(CultureInfo.CurrentCulture);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(InternalIntValue);
        }

        public float ToSingle()
        {
            return ToSingle(CultureInfo.CurrentCulture);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(InternalIntValue);
        }

        public double ToDouble()
        {
            return ToDouble(CultureInfo.CurrentCulture);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(InternalIntValue);
        }

        public decimal ToDecimal()
        {
            return ToDecimal(CultureInfo.CurrentCulture);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(InternalIntValue);
        }

        public DateTime ToDateTime()
        {
            return ToDateTime(CultureInfo.CurrentCulture);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(InternalIntValue);
        }

        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        public string ToString(IFormatProvider provider)
        {
            return _internalTextValue;
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public static Roman operator +(Roman r1, Roman r2)
        {
            var sum = r1.InternalIntValue + r2.InternalIntValue;

            return sum;
        }

        public static Roman operator -(Roman r1, Roman r2)
        {
            if (r2._internalTextValue.ToLower().Equals("i"))
            {
                if( !r1._internalTextValue.ToLower().Equals("v")  && !r1._internalTextValue.ToLower().Equals("x") )
                    throw new InvalidOperationException("'I' can be subtracted from 'V' and 'X' only");
            }

            if (r2._internalTextValue.ToLower().Equals("x"))
            {
                if (!r1._internalTextValue.ToLower().Equals("l") && !r1._internalTextValue.ToLower().Equals("c"))
                    throw new InvalidOperationException("'X' can be subtracted from 'L' and 'C' only");
            }

            if (r2._internalTextValue.ToLower().Equals("c"))
            {
                if (!r1._internalTextValue.ToLower().Equals("d") && !r1._internalTextValue.ToLower().Equals("m"))
                    throw new InvalidOperationException("'C' can be subtracted from 'D' and 'M' only");
            }

            if (r2._internalTextValue.ToLower().Equals("v") || r2._internalTextValue.ToLower().Equals("l") ||
                    r2._internalTextValue.ToLower().Equals("d"))
            {
                throw new InvalidOperationException("'V', 'L', and 'D' can never be subtracted");
            }

            var sub = r1.InternalIntValue - r2.InternalIntValue;

            return sub;
        }

        public static bool operator ==(Roman r1, Roman r2)
        {
            return r1._internalTextValue == r2._internalTextValue;
        }

        public static bool operator !=(Roman r1, Roman r2)
        {
            return r1._internalTextValue != r2._internalTextValue;
        }

        public static bool operator >(Roman r1, Roman r2)
        {
            return r1.InternalIntValue > r2.InternalIntValue;
        }
        
        public static bool operator <(Roman r1, Roman r2)
        {
            return r1.InternalIntValue < r2.InternalIntValue;
        }

        public static bool operator >=(Roman r1, Roman r2)
        {
            return r1.InternalIntValue >= r2.InternalIntValue;
        }

        public static bool operator <=(Roman r1, Roman r2)
        {
            return r1.InternalIntValue <= r2.InternalIntValue;
        }

        public static implicit operator Roman(string value)
        {
            return new Roman(value);
        }

        public static implicit operator Roman(char value)
        {
            return new Roman(value);
        }

        public static implicit operator Roman(Int32 value)
        {
            return new Roman(value);
        }

        public static implicit operator Int32(Roman roman)
        {
            return roman.InternalIntValue;
        }

        public static implicit operator String(Roman roman)
        {
            return roman.ToString(CultureInfo.InvariantCulture);
        }

        private static Int32 ConvertToInt(string textValue)
        {
            //MCMXLIV = 1000 + (1000 − 100) + (50 − 10) + (5 − 1) = 1944
            //         +1000 - 100 + 1000 - 10 + 50 - 1 + 5
            return textValue.Select((romanCaracter, index) => ConvertedValue(textValue, romanCaracter, index)).Sum();
        }

        private static int ConvertedValue(string textValue, char romanCaracter, int index)
        {
            var romanDecimalEquivalent = RomanDecimalEquivalent(romanCaracter);

            if ((index + 1) == textValue.Length) 
                return romanDecimalEquivalent;

            var romanNextDecimalEquivalent = RomanDecimalEquivalent(textValue[index + 1]);

            if (romanDecimalEquivalent < romanNextDecimalEquivalent)
                return (romanDecimalEquivalent * -1);

            return romanDecimalEquivalent;
        }

        private static string ConvertToRoman(int value)
        {
            return ConvertToRoman(value, string.Empty);
        }

        private static string ConvertToRoman(int value, string tail)
        {

            if ((value < 0) || (value > 3999))
                throw new ArgumentOutOfRangeException(string.Format("The number {0} cannot be converted to roman, please insert value betwheen 1 and 3999", value));

            if (value < 1)
                return tail;

            if (value >= 1000)
                return ConvertToRoman(value - 1000, tail + "M");

            if (value >= 900)
                return ConvertToRoman(value - 900, tail + "CM");

            if (value >= 500)
                return ConvertToRoman(value - 500, tail + "D");

            if (value >= 400)
                return ConvertToRoman(value - 400, tail + "CD");

            if (value >= 100)
                return ConvertToRoman(value - 100, tail + "C");

            if (value >= 90)
                return ConvertToRoman(value - 90, tail + "XC");

            if (value >= 50)
                return ConvertToRoman(value - 50, tail + "L");

            if (value >= 40)
                return ConvertToRoman(value - 40, tail + "XL");

            if (value >= 10)
                return ConvertToRoman(value - 10, tail + "X");

            if (value >= 9)
                return ConvertToRoman(value - 9, tail + "IX");

            if (value >= 5)
                return ConvertToRoman(value - 5, tail + "V");

            if (value >= 4)
                return ConvertToRoman(value - 4, tail + "IV");

            if (value >= 1)
                return ConvertToRoman(value - 1, tail + "I");

            throw new ArgumentOutOfRangeException("Something bad happened");

        }

        private static int RomanDecimalEquivalent(char romanCaracter)
        {
            RomanSymbolConfiguration symbolConfiguration;

            CheckTable(romanCaracter, out symbolConfiguration);

            return symbolConfiguration.Value;
        }

        private static int GetRomanSymbolSucessionTimeConfiguration(char romanCaracter)
        {
            RomanSymbolConfiguration symbolConfiguration;

            CheckTable(romanCaracter, out symbolConfiguration);

            return symbolConfiguration.SuccetionTimes;
        }

        private static int GetRomanSymbolMaxOccursConfiguration(char romanCaracter)
        {
            RomanSymbolConfiguration symbolConfiguration;

            CheckTable(romanCaracter, out symbolConfiguration);

            return symbolConfiguration.MaxOccurs;
        }

        private static void CheckTable(char caracter)
        {
            RomanSymbolConfiguration symbolConfiguration;
            CheckTable(caracter, out symbolConfiguration);
        }

        private static void CheckTable(char romanCaracter, out RomanSymbolConfiguration symbolConfiguration)
        {
            if (!Table.TryGetValue(romanCaracter, out symbolConfiguration))
                throw new ArgumentException(string.Format("The caracter {0} is not a valid roman symbol.", romanCaracter));
        }

        private static void CheckSymbolStructure(string symbol)
        {
            var countSymbolBuffer = new Dictionary<char, int>();

            for (var index = 0; index < symbol.Length; index++)
            {
                var romanCaracter = symbol[index];

                if (!countSymbolBuffer.ContainsKey(romanCaracter))
                    countSymbolBuffer.Add(romanCaracter, 1);
                else
                {
                    if (romanCaracter == symbol[index - 1])
                        countSymbolBuffer[romanCaracter]++;
                }
                
                CheckSymbolStructure(romanCaracter, index, countSymbolBuffer[romanCaracter], symbol);
            }
        }

        private static void CheckSymbolStructure(Roman romanCaracter, int index, int countSymbol, string symbol)
        {
            CheckSuccetionTimes(romanCaracter, index, countSymbol, symbol);

            CheckMaxOccurs(romanCaracter, countSymbol, symbol);
        }

        private static void CheckMaxOccurs(Roman romanCaracter, int countSymbol, string symbol)
        {
            var maxOccursconfiguration = GetRomanSymbolMaxOccursConfiguration(romanCaracter.ToChar());

            if (maxOccursconfiguration == 0) return;

            if (countSymbol > maxOccursconfiguration)
                throw new ArgumentException(string.Format("The symbol {0} is not a valid roman symbol.", symbol));
        }

        private static void CheckSuccetionTimes(Roman romanCaracter, int index, int countSymbol, string symbol)
        {
            var sucessionTimeConfiguration = GetRomanSymbolSucessionTimeConfiguration(romanCaracter.ToChar());

            if (sucessionTimeConfiguration == 0) return;

            if (countSymbol < sucessionTimeConfiguration) return;

            if (symbol.Length - 1 == index) return;

            if (symbol.Length == countSymbol)
                throw new ArgumentException(string.Format("The symbol {0} is not a valid roman symbol.", symbol));

            if (romanCaracter <= symbol[index + 1])
                throw new ArgumentException(string.Format("The symbol {0} is not a valid roman symbol.", symbol));

            var nextedCaracter = symbol.ElementAtOrDefault(index + 2);
            if (nextedCaracter != default(char))
            {
                if (nextedCaracter != romanCaracter)
                {
                    throw new ArgumentException(string.Format("The symbol {0} is not a valid roman symbol.", symbol));
                }
            }
            else
            {
                throw new ArgumentException(string.Format("The symbol {0} is not a valid roman symbol.", symbol));
            }
        }
    }
}