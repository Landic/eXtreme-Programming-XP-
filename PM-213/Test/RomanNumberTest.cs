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
            var Assert_ThrowsException_Methods = typeof(Assert).GetMethods().Where(x => x.Name == "ThrowsException").Where(x => x.IsGenericMethod);
            var Assert_ThrowsException_Method = Assert_ThrowsException_Methods.Skip(3).FirstOrDefault();

            TestCase[] testCases1 = [
                new(){Source ="N", Value = 0},
                new(){Source ="I", Value = 1},
                new(){Source ="II", Value = 2},
                new(){Source ="III", Value = 3},
                new(){Source ="IV", Value = 4},
                new(){Source ="V", Value = 5},
                new(){Source ="VI", Value = 6},
                new(){Source ="VII", Value = 7},
                new(){Source ="VIII", Value = 8},
                new(){Source ="D", Value = 500},
                new(){Source ="CM", Value = 900},
                new(){Source ="M", Value = 1000},
                new(){Source ="MC", Value = 1100},
                new(){Source ="MCM", Value = 1900},
                new(){Source ="MM", Value = 2000},
            ];

            foreach (TestCase testCase in testCases1)
            {
                RomanNumber rn = RomanNumberFactory.Parse(testCase.Source);
                Assert.IsNotNull(rn);
                Assert.AreEqual(testCase.Value, rn.Value, $"{testCase.Source} parsing failed. Expected {testCase.Value}, got {rn.Value}.");
            }

            var formatExceptionType = typeof(FormatException);
            String part1Template = "Invalid symbol '{0}' in position {1}";

            TestCase[] testCases2 = [
                new(){Source = "W", ExceptionType = formatExceptionType, ExceptionMessageParts=[String.Format(part1Template, 'W', 0)]},
                new(){Source = "Q", ExceptionType = formatExceptionType, ExceptionMessageParts=[String.Format(part1Template, 'Q', 0)]},
                new(){Source = "s", ExceptionType = formatExceptionType, ExceptionMessageParts=[String.Format(part1Template, 's', 0)]},
                new(){Source = "sX", ExceptionType = formatExceptionType, ExceptionMessageParts=[String.Format(part1Template, 's', 0)]},
                new(){Source = "Xd", ExceptionType = formatExceptionType, ExceptionMessageParts=[String.Format(part1Template, 'd', 1)]},
            ];

            foreach (TestCase testCase in testCases2)
            {
                dynamic? ex = Assert_ThrowsException_Method?.MakeGenericMethod(testCase.ExceptionType!)
                    .Invoke(null, [() => RomanNumberFactory.Parse(testCase.Source), $"Parse('{testCase.Source}') must throw FormatException"]);
                Assert.IsTrue(ex!.Message.Contains(testCase.ExceptionMessageParts!.First()),
                    $"Parse('{testCase.Source}') FormatException must contain '{testCase.ExceptionMessageParts!.First()}'");
            }

            TestCase[] testCases3 = {
                new() { Source = "IM",  ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "XIM", ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 1" }, ExceptionType = formatExceptionType },
                new() { Source = "IMX", ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "XMD", ExceptionMessageParts = new[] { "Invalid order 'X' before 'M' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "XID", ExceptionMessageParts = new[] { "Invalid order 'I' before 'D' in position 1" }, ExceptionType = formatExceptionType },
                new() { Source = "VX",  ExceptionMessageParts = new[] { "Invalid order 'V' before 'X' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "VL",  ExceptionMessageParts = new[] { "Invalid order 'V' before 'L' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "LC",  ExceptionMessageParts = new[] { "Invalid order 'L' before 'C' in position 0" }, ExceptionType = formatExceptionType },
                new() { Source = "DM",  ExceptionMessageParts = new[] { "Invalid order 'D' before 'M' in position 0" }, ExceptionType = formatExceptionType }
            };

            foreach (var testCase in testCases3)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumberFactory.Parse(testCase.Source),
                    $"Parse '{testCase.Source}' must throw FormatException"
                );
                Assert.IsTrue(
                    ex.Message.Contains(testCase.ExceptionMessageParts!.First()),
                    "FormatException must contain data about mis-ordered symbols and its position"
                );
            }
            TestCase[] testCases4 = {
                new() { Source = "IXIX", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "IXX",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "IXIV", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCXC", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "CMM",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "CMCD", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCXL", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCC",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
                new() { Source = "XCCI", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType }
            };

            foreach (TestCase testCase in testCases4)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumberFactory.Parse(testCase.Source),
                    $"Parse('{testCase.Source}') must throw FormatException"
                );
                foreach (var part in testCase.ExceptionMessageParts!)
                {
                    Assert.IsTrue(ex.Message.Contains(part),
                        $"Parse('{testCase.Source}') FormatException must contain '{part}'. Actual message: {ex.Message}");
                }
            }
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

class TestCase
{
    public String Source { get; set; }
    public int? Value { get; set; }
    public Type? ExceptionType { get; set; }
    public IEnumerable<String>? ExceptionMessageParts { get; set; }
}