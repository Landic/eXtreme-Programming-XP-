using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int value)
    {
        private readonly int _value = value; // Refactoring

        public int Value { get => _value; init { _value = value; } }

        public static RomanNumber Parse(string input)
        {
            //input = input.ToUpper();

            //if (string.IsNullOrEmpty(input))
            //{
            //    return new RomanNumber(0);
            //}

            //Dictionary<char,int> romanMap = new Dictionary<char, int>
            //{
            //    {'I', 1},
            //    {'V', 5},
            //    {'X', 10},
            //    {'L', 50},
            //    {'C', 100},
            //    {'D', 500},
            //    {'M', 1000}
            //};

            //if (romanMap.TryGetValue(input[0], out int value))
            //{
            //    return new RomanNumber(value);
            //}
            //else
            //{
            //    throw new ArgumentException("Invalid Roman number!");
            //}
            int value = 0;
            int prevDigit = 0;
            int pos = input.Length;
            List<String> errors = new();
            foreach (char c in input.Reverse())
            {
                pos -= 1;
                int digit;
                try
                {
                    digit = DigitalValue(c.ToString());
                }
                catch
                {
                    throw new FormatException($"Invalid symbol '{c}' in position {pos}");
                }

                if (digit != 0 && prevDigit / digit > 10)
                {
                    throw new FormatException($"Invalid order '{c}' before '{input[pos + 1]}' in position {pos}");
                }

                if (prevDigit > digit &&
                    !((digit == 1 && (prevDigit == 5 || prevDigit == 10)) ||
                     (digit == 10 && (prevDigit == 50 || prevDigit == 100)) ||
                     (digit == 100 && (prevDigit == 500 || prevDigit == 1000))))
                {
                    throw new FormatException($"Invalid combination of '{c}' in position {pos}");
                }

                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }

            return new RomanNumber(value);
        }

        public static int DigitalValue(String digit) => digit switch
        {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            "M" => 1000,
            _ => throw new ArgumentException($"{nameof(RomanNumber)}::{nameof(DigitalValue)}: 'digit' has invalid value '{digit}'")
        };

        public RomanNumber Plus(RomanNumber other)
        {
            return this with { Value = Value + other.Value };
        }

        public override string? ToString()
        {
            if (_value == 0) return "N";
            Dictionary<int, String> parts = new()
            {
                {1000, "M"},
                {900, "CM" },
                {500, "D"},
                {400, "CD" },
                {100, "C"},
                {90, "XC" },
                {50, "L"},
                {40, "XL"},
                {10, "X"},
                {9,"IX" },
                {5, "V"},
                {4, "IV"},
                {1, "I"}
            };
            int v = _value;
            StringBuilder sb = new();
            foreach (var part in parts)
            {
                while (v >= part.Key)
                {
                    v -= part.Key;
                    sb.Append(part.Value);
                }
            }
            return sb.ToString();
        }
    }
}