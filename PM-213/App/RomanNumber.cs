using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int value)
    {
        private readonly int _value = value;

        public int Value => _value;

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
                    errors.Add($"Invalid symbol '{c}' in position {pos}");
                    continue;
                }
                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }

            if (errors.Any())
            {
                throw new FormatException(string.Join("; ", errors));
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
            "W" => 5000,
            _ => throw new ArgumentException($" {nameof(RomanNumber)} : {nameof(DigitalValue)}'digit' has invalid value '{digit}'")
        };
    }
}