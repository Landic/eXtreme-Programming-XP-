using App;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {
        private readonly Dictionary<String, int> _digitValues = new()
        {
                {"N", 0},
                {"I", 1 },
                {"V", 5 },
                {"X", 10},
                {"L", 50},
                {"C", 100 },
                {"D", 500 },
                {"M", 1000}
         };

        //[TestMethod]
        //public void TestParseI()
        //{
        //    RomanNumber rn = RomanNumber.Parse("v");
        //    Assert.AreEqual(1, rn.Value, "Should have come back 1");
        //}

        //[TestMethod]
        //public void TestParseV()
        //{
        //    RomanNumber rn = RomanNumber.Parse("x");
        //    Assert.AreEqual(5, rn.Value, "Should have come back 5");
        //}

        //[TestMethod]
        //public void TestParseX()
        //{
        //    RomanNumber rn = RomanNumber.Parse("v");
        //    Assert.AreEqual(10, rn.Value, "Should have come back 10");
        //}

        //[TestMethod]
        //public void TestParseL()
        //{
        //    RomanNumber rn = RomanNumber.Parse("c");
        //    Assert.AreEqual(50, rn.Value, "Should have come back 50");
        //}

        //[TestMethod]
        //public void TestParseC()
        //{
        //    RomanNumber rn = RomanNumber.Parse("l");
        //    Assert.AreEqual(100, rn.Value, "Should have come back 100");
        //}

        //[TestMethod]
        //public void TestParseD()
        //{
        //    RomanNumber rn = RomanNumber.Parse("i");
        //    Assert.AreEqual(500, rn.Value, "Should have come back 500");
        //}

        //[TestMethod]
        //public void TestParseM()
        //{
        //    RomanNumber rn = RomanNumber.Parse("d");
        //    Assert.AreEqual(1000, rn.Value, "Should have come back 1000");
        //}

        [TestMethod]
        public void ParseTest()
        {
            Dictionary<String, int> romanMap = new()
            {
                {"N", 0},
                {"I", 1},
                {"II", 2},
                {"III", 3},
                {"IIII", 4},
                {"IV", 4},
                {"V", 5},
                {"VI", 6},
                {"VII", 7},
                {"VIII", 8},
                {"VIIII", 9},
                {"IX", 9},
                {"X", 10},
                {"XI", 11},
                {"XII", 12},
                {"XIII", 13},
                {"XIIII", 14},
                {"XIV", 14},
                {"XV", 15},
                {"XVI", 16},
                {"XX", 20},
                {"XXX", 30},
                {"XL", 40},
                {"XXXX", 40},
                {"L", 50},
                {"LX", 60},
                {"LXXXX", 90},
                {"XC", 90},
                {"C", 100},
                {"CC", 200},
                {"CCC", 300},
                {"CD", 400},
                {"D", 500},
                {"DC", 600},
                {"DCCC", 800},
                {"CM", 900},
                {"M", 1000},
                {"MC", 1100},
                {"MCM", 1900},
                {"MM", 2000},
                {"MMM", 3000},
                {"MMMM", 4000}
            };
            foreach (var test in romanMap)
            {
                RomanNumber rn = RomanNumber.Parse(test.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(test.Value, rn.Value, $"{test.Key} -> {test.Value}");
            }


            Dictionary<string, (char, int)[]> exTestCases = new()
            {
                {"W", new[] {('W', 0)}},
                {"Q", new[] {('Q', 0)}},
                {"s", new[] {('s', 0)}},
                {"Xd", new[] {('d', 1)}},
                {"SWXF", new[] { ('S', 0), ('W', 1), ('F', 3) }},
                {"AIXL", new[] { ('A', 0) }},
                {"MMQ", new[] { ('Q', 2) }},
                {"XDDX", new[] { ('D', 1), ('D', 2) }},
                {"YIYV", new[] { ('Y', 0), ('Y', 2) }},
            };
            foreach (var testCase in exTestCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"{nameof(FormatException)} Parse '{testCase.Key}' must throw"
                );
                foreach (var (symbol, position) in testCase.Value)
                {
                    Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
                        $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
                        $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
                    );
                }
            }
            Dictionary<String, Object[]> invalidOrderTestCases = new()
            {
                { "IM",  ['I', 'M', 0] },
                { "XIM", ['I', 'M', 1] },
                { "IMX", ['I', 'M', 0] },
                { "XMD", ['X', 'M', 0] },
                { "XID", ['I', 'D', 1] },
                { "ID", ['I', 'D', 0] },
                { "VX", ['V', 'X', 0] },
                { "LC", ['L', 'C', 0] },
                { "VV", ['V', 'V', 0] },
                { "LL", ['L', 'L', 0] }
            };

            foreach (var testCase in invalidOrderTestCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"{nameof(FormatException)} Parse '{testCase.Key}' must throw"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"Invalid order '{testCase.Value[0]}' before '{testCase.Value[1]}' in position {testCase.Value[2]}"),
                    $"FormatException must contain data about mis-ordered symbols. TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
                );
            }

        }

        [TestMethod]
        public void InvalidParseTest()
        {
            string[] invalidTestCases =
            {
                "IIIIII",
                "VV",
                "LL",
                "DD",
                "MMMMM",
                "IC",
                "IM",
                "XD",
                "IL",
                ""
            };
            foreach (var invalidCase in invalidTestCases)
            {
                Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(invalidCase), $"Expected exception for {invalidCase}");
            }
        }


        [TestMethod]
        public void DigitalValueTest()
        {
            foreach (var test in _digitValues)
            {
                Assert.AreEqual(test.Value, RomanNumber.DigitalValue(test.Key), $"{test.Key} -> {test.Value}");
            }
            Random rand = new();
            for (int i = 0; i < 100; i++)
            {
                String invalidDigit = ((char)rand.Next(256)).ToString();
                if (_digitValues.ContainsKey(invalidDigit))
                {
                    i--;
                    continue;
                }
                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.DigitalValue(invalidDigit),
                $"ArgumentException erxpected for digit = '{invalidDigit}'"
                 );
                // виманатимемо від винятку
                // - повідомлення, що
                // = не є порожнім
                // = містить назву аргументу (digit)
                // = містить значення аргументу, що призвело до винятку
                Assert.IsFalse(
                     String.IsNullOrEmpty(ex.Message),
                     "ArgumentException must have a message"
                     );
                Assert.IsTrue(
                    ex.Message.Contains($"'digit' has invalid value '{invalidDigit}'"),
                    $"ArgumentException message must contain <'digit' has invalid value '{invalidDigit}'>"
                    );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNumber)) &&
                    ex.Message.Contains(nameof(RomanNumber.DigitalValue)),
                    $"ArgumentException message must contain '{nameof(RomanNumber)}' and '{nameof(RomanNumber.DigitalValue)}'"
                    );
            }

        }


        [TestMethod]
        public void ToStringTest()
        {
            Dictionary<int, String> testCases = new()
            {
                {2, "II"},
                {3343, "MMMCCCXLIII"},
                {4, "IV" },
                {44, "XLIV" },
                {9,"IX" },
                {90, "XC" },
                {1400, "MCD" },
                {999, "CMXCIX" },
                {444, "CDXLIV" },
                {990, "CMXC" }

            };
            _digitValues.Keys.ToList().ForEach(i => testCases.Add(_digitValues[i], i));
            foreach (var test in testCases)
            {
                Assert.AreEqual(test.Value, new RomanNumber(test.Key).ToString(), $"ToString({test.Key}) --> {test.Value}");
            }
        }
    }
}