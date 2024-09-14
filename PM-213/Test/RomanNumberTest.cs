using App;
using System.Reflection;

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

        [TestMethod]
        public void ConstructorTest()
        {
            var rn = new RomanNumber("IX");
            Assert.IsNotNull(rn);

            rn = new RomanNumber(3);
            Assert.IsNotNull(rn);
        }
        [TestMethod]
        public void ConvertTest()
        {
            var rn = new RomanNumber("IX");
            Assert.IsInstanceOfType<Int32>(rn.ToInt());
            Assert.IsInstanceOfType<UInt32>(rn.ToUnsignedInt());
            Assert.IsInstanceOfType<Int16>(rn.ToShort());
            Assert.IsInstanceOfType<UInt16>(rn.ToUnsignedShort());
            Assert.IsInstanceOfType<Single>(rn.ToFloat());
            Assert.IsInstanceOfType<Double>(rn.ToDouble());

        }


        //[TestMethod]
        //public void _CheckSubsTest()
        //{
        //    Type? rnType = typeof(RomanNumber);
        //    MethodInfo? m1Info = rnType.GetMethod("_CheckSubs",
        //        BindingFlags.NonPublic | BindingFlags.Static);
        //    string[] validCases = { "IV", "IX", "XL", "XC", "CD", "CM", "MCMXCIV" };
        //    foreach (var validCase in validCases)
        //    {
        //        m1Info?.Invoke(null, [validCase]);
        //    }
        //    string[] invalidCases = { "IIV", "IIX", "XXL", "XXC", "CCD", "CCM", "IIVX", "IIXX", "IVIV", "IXIX" };
        //    foreach (var invalidCase in invalidCases)
        //    {
        //        var ex = Assert.ThrowsException<TargetInvocationException>(
        //            () => m1Info?.Invoke(null, [invalidCase]),
        //            $"_CheckSubs '{invalidCase}' must throw FormatException"
        //        );
        //        Assert.IsInstanceOfType<FormatException>(
        //            ex.InnerException,
        //            "_CheckSubs: FormatException from InnerException"
        //        );
        //    }
        //}

        [TestMethod]
        public void _CheckSymbolsTest()
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod("_CheckSymbols",
            BindingFlags.NonPublic | BindingFlags.Static);

            // Assert Not Throws
            m1Info?.Invoke(null, ["IX"]);

            var ex = Assert.ThrowsException<TargetInvocationException>(
            () => m1Info?.Invoke(null, ["IW"]),
            $"Parse 'IW' must throw FormatException"
                );

            Assert.IsInstanceOfType<FormatException>(
             ex.InnerException,
             $"FormatException from InnerException");
        }

        //[TestMethod]
        //public void _CheckPairsTest()
        //{
        //    Type? rnType = typeof(RomanNumber);
        //    MethodInfo? m1Info = rnType.GetMethod("_CheckPairs",
        //        BindingFlags.NonPublic | BindingFlags.Static);

        //    string[] validCases = { "IV", "IX", "XL", "XC", "CD", "CM", "MCMLIV", "MMXXIII" };
        //    foreach (var validCase in validCases)
        //    {
        //        m1Info?.Invoke(null, [validCase]);
        //    }

        //    string[] invalidCases = { "IC", "ID", "IM", "XM", "VX", "VL", "VC", "VD", "VM", "LC", "LD", "LM", "DM" };
        //    foreach (var invalidCase in invalidCases)
        //    {
        //        var ex = Assert.ThrowsException<TargetInvocationException>(
        //            () => m1Info?.Invoke(null, [invalidCase]),
        //            $"_CheckPairs '{invalidCase}' must throw FormatException"
        //        );
        //        Assert.IsInstanceOfType<FormatException>(
        //            ex.InnerException,
        //            "_CheckPairs: FormatException from InnerException"
        //        );
        //    }
        //}

        [TestMethod]
        public void _CheckFormatTest()
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod("_CheckFormat",
            BindingFlags.NonPublic | BindingFlags.Static);

            m1Info?.Invoke(null, ["IX"]);

            var ex = Assert.ThrowsException<TargetInvocationException>(
            () => m1Info?.Invoke(null, ["IIX"]),
            $"_CheckFormat 'IIX' must throw FormatException"
                );

            Assert.IsInstanceOfType<FormatException>(
             ex.InnerException,
             $"_CheckFormat:FormatException from InnerException");
        }

        [TestMethod]
        public void _CheckValidityTest()
        {
            Type? rnType = typeof(RomanNumberFactory);
            MethodInfo? m1Info = rnType.GetMethod("_CheckValidity",
            BindingFlags.NonPublic | BindingFlags.Static);

            // Assert Not Throws
            m1Info?.Invoke(null, ["IX"]);

            string[] testCases = ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL", "CMCD"];
            foreach (var testCase in testCases)
            {

                var ex = Assert.ThrowsException<TargetInvocationException>(
                () => m1Info?.Invoke(null, [testCase]),
                $"_CheckValidity '{testCase}' must throw FormatException"
                    );

                Assert.IsInstanceOfType<FormatException>(
                 ex.InnerException,
                 $"_CheckValidity:FormatException from InnerException");
            }
        }

        [TestMethod]
        public void ParseTest()
        {
            var testCases = new Dictionary<string, int>()
            {
                { "N",      0 },
                { "I",      1 },
                { "II",     2 },
                { "III",    3 },
                { "IV",     4 },
                { "V",      5 },
                { "VI",     6 },
                { "VII",    7 },
                { "VIII",   8 },
                { "D",      500 },
                { "CM",     900 },
                { "M",      1000 },
                { "MC",     1100 },
                { "MCM",    1900 },
                { "MM",     2000 },
            };

            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumberFactory.Parse(testCase.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(testCase.Value
                    ,
                    rn.Value,
                    $"{testCase.Key} parsing failed. Expected {testCase.Value}, got {rn.Value}."
                );
            }
            //Dictionary<String, int> romanMap = new()
            //{
            //    {"N", 0},
            //    {"I", 1},
            //    {"II", 2},
            //    {"III", 3},
            //    {"IIII", 4},
            //    {"IV", 4},
            //    {"V", 5},
            //    {"VI", 6},
            //    {"VII", 7},
            //    {"VIII", 8},
            //    {"VIIII", 9},
            //    {"IX", 9},
            //    {"X", 10},
            //    {"XI", 11},
            //    {"XII", 12},
            //    {"XIII", 13},
            //    {"XIIII", 14},
            //    {"XIV", 14},
            //    {"XV", 15},
            //    {"XVI", 16},
            //    {"XX", 20},
            //    {"XXX", 30},
            //    {"XL", 40},
            //    {"XXXX", 40},
            //    {"L", 50},
            //    {"LX", 60},
            //    {"LXXXX", 90},
            //    {"XC", 90},
            //    {"C", 100},
            //    {"CC", 200},
            //    {"CCC", 300},
            //    {"CD", 400},
            //    {"D", 500},
            //    {"DC", 600},
            //    {"DCCC", 800},
            //    {"CM", 900},
            //    {"M", 1000},
            //    {"MC", 1100},
            //    {"MCM", 1900},
            //    {"MM", 2000},
            //    {"MMM", 3000},
            //    {"MMMM", 4000}
            //};
            //foreach (var test in romanMap)
            //{
            //    RomanNumber rn = RomanNumber.Parse(test.Key);
            //    Assert.IsNotNull(rn);
            //    Assert.AreEqual(test.Value, rn.Value, $"{test.Key} -> {test.Value}");
            //}


            //Dictionary<string, (char, int)[]> exTestCases = new()
            //{
            //    {"W", new[] {('W', 0)}},
            //    {"Q", new[] {('Q', 0)}},
            //    {"s", new[] {('s', 0)}},
            //    {"Xd", new[] {('d', 1)}},
            //    {"SWXF", new[] { ('S', 0), ('W', 1), ('F', 3) }},
            //    {"AIXL", new[] { ('A', 0) }},
            //    {"MMQ", new[] { ('Q', 2) }},
            //    {"XDDX", new[] { ('D', 1), ('D', 2) }},
            //    {"YIYV", new[] { ('Y', 0), ('Y', 2) }},
            //};
            //foreach (var testCase in exTestCases)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumber.Parse(testCase.Key),
            //        $"{nameof(FormatException)} Parse '{testCase.Key}' must throw"
            //    );
            //    foreach (var (symbol, position) in testCase.Value)
            //    {
            //        Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
            //            $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
            //            $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
            //        );
            //    }
            //}
            //Dictionary<String, Object[]> invalidOrderTestCases = new()
            //{
            //    { "IM",  ['I', 'M', 0] },
            //    { "XIM", ['I', 'M', 1] },
            //    { "IMX", ['I', 'M', 0] },
            //    { "XMD", ['X', 'M', 0] },
            //    { "XID", ['I', 'D', 1] },
            //    { "ID", ['I', 'D', 0] },
            //    { "VX", ['V', 'X', 0] },
            //    { "LC", ['L', 'C', 0] },
            //    { "VV", ['V', 'V', 0] },
            //    { "LL", ['L', 'L', 0] }
            //};

            //foreach (var testCase in invalidOrderTestCases)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //        () => RomanNumber.Parse(testCase.Key),
            //        $"{nameof(FormatException)} Parse '{testCase.Key}' must throw"
            //    );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"Invalid order '{testCase.Value[0]}' before '{testCase.Value[1]}' in position {testCase.Value[2]}"),
            //        $"FormatException must contain data about mis-ordered symbols. TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'"
            //    );
            //}

            //String[] exTestCases3 =
            //{
            //    "IXC", "IIX", "VIX",
            //    "IIXC", "IIIX", "VIIX",
            //    "VIXC", "IVIX", "CVIIX",
            //    "IXCC", "IXCM", "IXXC",
            //};

            //foreach (var testCase in exTestCases3)
            //{
            //    var ex = Assert.ThrowsException<FormatException>(
            //       () => RomanNumber.Parse(testCase),
            //        $"{nameof(FormatException)} Parse '{testCase}' must throw"
            //    );
            //    Assert.IsTrue(ex.Message.Contains(nameof(RomanNumber)) &&
            //                  ex.Message.Contains(nameof(RomanNumber.Parse)) &&
            //                  ex.Message.Contains($"invalid sequence: more than 1 less digit before '{testCase[^1]}'"),
            //                  $"ex.Message must contain info about origin, cause and data. {ex.Message}");
            //}

        }

        //[TestMethod]
        //public void InvalidParseTest()
        //{
        //    string[] invalidTestCases =
        //    {
        //        "IIIIII",
        //        "VV",
        //        "LL",
        //        "DD",
        //        "MMMMM",
        //        "IC",
        //        "IM",
        //        "XD",
        //        "IL",
        //        ""
        //    };
        //    foreach (var invalidCase in invalidTestCases)
        //    {
        //        Assert.ThrowsException<ArgumentException>(() => RomanNumber.Parse(invalidCase), $"Expected exception for {invalidCase}");
        //    }
        //}


        [TestMethod]
        public void DigitalValueTest()
        {
            Dictionary<string, int> testCases = new()
            {
                {"N", 0 },
                {"I", 1 },
                {"V", 5 },
                {"X", 10 },
                {"L", 50 },
                {"C", 100 },
                {"D", 500 },
                {"M", 1000 },
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    testCase.Value,
                    RomanNumberFactory.DigitValue(testCase.Key),
                    $"{testCase.Key} -> {testCase.Value}");
            }

            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                string invalidDigit = ((char)random.Next(256)).ToString();
                if (testCases.ContainsKey(invalidDigit))
                {
                    i--;
                    continue;
                }
                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                    () => RomanNumberFactory.DigitValue(invalidDigit),
                    $"ArgumentException expected for digit = '{invalidDigit}'"
                    );
                Assert.IsFalse(
                   string.IsNullOrEmpty(ex.Message),
                   "ArgumnetExceptionmust have a message"
                );
                Assert.IsTrue(
                   ex.Message.Contains($"'digit' has invalid value '{invalidDigit}'"),
                   "ArgumnetException must must contain a <'digit' has invalid value ''>"
                   );
                Assert.IsTrue(
                   ex.Message.Contains($"'digit'"),
                   "ArgumnetExceptionmust must contain a 'digit'"
                   );
                Assert.IsTrue(
                   ex.Message.Contains(nameof(RomanNumber)) &&
                   ex.Message.Contains(nameof(RomanNumberFactory.DigitValue)),
                   $"ArgumnetExceptionmust must contain '{nameof(RomanNumber)}' and  '{nameof(RomanNumberFactory.DigitValue)}'"
                   );
                var ex2 = Assert.ThrowsException<FormatException>(
                    () => RomanNumberFactory.Parse("W"),
                    "Invalid format"
                    );
                Assert.IsTrue(
                    ex2.Message.Contains("Invalid symbol 'W' in position 0"),
                    "FormatException must contain data about symbol and it's position"
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

        //[TestMethod]
        //public void PlusTest()
        //{
        //    RomanNumber rn1 = new(1);
        //    RomanNumber rn2 = new(2);
        //    RomanNumber rn3 = rn1.Plus(rn2);
        //    Assert.IsNotNull(rn3);
        //    Assert.IsInstanceOfType(rn3, typeof(RomanNumber), "Plus result must have RomanNumber type");
        //    Assert.AreNotSame(rn3, rn1, "Plus result is new instance, neither (v)first, not second arg");
        //    Assert.AreNotSame(rn3, rn2, "Plus result is new instance, neither first, not (v)second arg");
        //    Assert.AreEqual(rn1.Value + rn2.Value, rn3.Value, "Plus arithmetic");
        //    var testCases = new[]
        //    {
        //        (first: "IV", second: "VI", expected: "X"),   
        //        (first: "X", second: "V", expected: "XV"),    
        //        (first: "XL", second: "IX", expected: "XLIX"),
        //        (first: "L", second: "L", expected: "C"), 
        //        (first: "C", second: "D", expected: "DC"), 
        //        (first: "MMM", second: "MMM", expected: "MMMMMM") 
        //    };

        //    foreach (var testCase in testCases)
        //    {
        //        rn1 = RomanNumber.Parse(testCase.first);
        //        rn2 = RomanNumber.Parse(testCase.second);
        //        rn3 = rn1.Plus(rn2);

        //        Assert.AreEqual(testCase.expected, rn3.ToString(), $"Expected {testCase.first} + {testCase.second} = {testCase.expected}, but got {rn3}");
        //    }
        //}
    }
}