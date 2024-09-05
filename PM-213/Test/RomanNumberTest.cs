using App;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {
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
            Dictionary<String, int> testCases = new()
            {
                {"N", 0},
                {"I", 1 },
                {"II", 2 },
                {"III", 3},
                {"IIII", 4}, // цим ми дозволяємо, неоптимальну форму числа
                {"IV", 4 },
                {"VI", 6},
                {"VII", 7},
                {"IX", 9},
                {"C", 100 },
                {"D", 500 },
                {"CM", 900 },
                {"M", 1000},
                {"MC", 1100 },
                {"MCM", 1900 },
                {"MM", 2000 }
            };
            foreach (var test in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(test.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(test.Value, rn.Value, $"{test.Key} -> {test.Value}");
            }
        }

        [TestMethod]
        public void DigitalValueTest()
        {
            Dictionary<String, int> testCases = new()
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
            foreach (var test in testCases)
            {
                Assert.AreEqual(test.Value, RomanNumber.DigitalValue(test.Key), $"{test.Key} -> {test.Value}");
            }
        }
    }
}
