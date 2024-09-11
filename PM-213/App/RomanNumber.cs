using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            if (string.IsNullOrEmpty(input))
            {
                throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: Input string is null or empty");
            }

            int value = 0;
            int prevDigit = 0;
            int pos = input.Length;

            _CheckValidity(input);

            foreach (char c in input.Reverse())
            {
                pos -= 1;
                int digit = DigitalValue(c.ToString());

                if (prevDigit > digit && !((digit == 1 && (prevDigit == 5 || prevDigit == 10)) ||
                                           (digit == 10 && (prevDigit == 50 || prevDigit == 100)) ||
                                           (digit == 100 && (prevDigit == 500 || prevDigit == 1000))))
                {
                    throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: Invalid combination of '{c}' in position {pos}");
                }

                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }

            return new RomanNumber(value);
        }


        private static void _CheckSubs(string input)
        {
            HashSet<char> subs = new HashSet<char>();
            Dictionary<char, int> counts = new Dictionary<char, int>();
            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];
                if (!counts.ContainsKey(current))
                    counts[current] = 0;
                counts[current]++;
                if (i < input.Length - 1)
                {
                    char next = input[i + 1];

                    if ((current == 'I' || current == 'X' || current == 'C') &&
                        DigitalValue(current.ToString()) < DigitalValue(next.ToString()))
                    {
                        if (!((current == 'I' && (next == 'V' || next == 'X')) ||
                              (current == 'X' && (next == 'L' || next == 'C')) ||
                              (current == 'C' && (next == 'D' || next == 'M'))))
                        {
                            throw new FormatException($"Invalid subtractive pair: {current}{next}");
                        }
                        if (subs.Contains(current))
                        {
                            throw new FormatException($"Repeated subtractive notation: {current}");
                        }
                        if (counts[current] > 1)
                        {
                            throw new FormatException($"Invalid repetition before subtractive notation: {current}");
                        }
                        subs.Add(current);
                        i++;
                    }
                }
            }
        }

        private static void _CheckFormat(string input)
        {
            int maxDigit = 0;
            Dictionary<char, int> counts = new Dictionary<char, int>();
            bool hasLesserDigit = false;
            foreach (char c in input.Reverse())
            {
                int digit = DigitalValue(c.ToString());
                if (!counts.ContainsKey(c))
                    counts[c] = 0;
                counts[c]++;
                if ((c == 'I' || c == 'X' || c == 'C' || c == 'M') && counts[c] > 3)
                    throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: Invalid repetition of '{c}'");
                if ((c == 'V' || c == 'L' || c == 'D') && counts[c] > 1)
                    throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: Invalid repetition of '{c}'");

                if (digit < maxDigit)
                {
                    if (hasLesserDigit)
                    {
                        throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: invalid sequence: more than 1 lesser digit before '{input[^1]}'");
                    }
                    hasLesserDigit = true;
                }
                else if (digit > maxDigit)
                {
                    maxDigit = digit;
                    hasLesserDigit = false;
                }
            }
        }

        private static void _CheckPairs(string input)
        {
            for(int i = 0; i < input.Length - 1; i++)
            {
                int rightDigit = DigitalValue(input[i + 1].ToString());
                int leftDigit = DigitalValue(input[i].ToString());
                if(leftDigit != 0 && leftDigit < rightDigit && (rightDigit / leftDigit > 10 || (leftDigit == 5 || leftDigit == 50 || leftDigit == 500)))
                {
                    throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: Invalid order '{input[i]}' before '{input[i + 1]}' in position {i}");
                }
            }
        }

        private static void _CheckValidity(string input)
        {
            _CheckSymbols(input);
            _CheckPairs(input);
            _CheckFormat(input);
            _CheckSubs(input);
        }


        private static void _CheckSymbols(string input)
        {
            int pos = 0;
            foreach (char c in input)
            {
                try
                {
                    DigitalValue(c.ToString());
                }
                catch
                {
                    throw new FormatException($"{nameof(RomanNumber)}.{nameof(Parse)}: Invalid symbol '{c}' in position {pos}");
                }
            }
        }

        private static int method1() => 1;



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