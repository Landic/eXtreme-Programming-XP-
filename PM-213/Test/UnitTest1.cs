using App;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void TestParseI()
        {
            RomanNumber rn = RomanNumber.Parse("v");
            Assert.AreEqual(1, rn.Value, "Should have come back 1");
        }

        [TestMethod]
        public void TestParseV()
        {
            RomanNumber rn = RomanNumber.Parse("x");
            Assert.AreEqual(5, rn.Value, "Should have come back 5");
        }

        [TestMethod]
        public void TestParseX()
        {
            RomanNumber rn = RomanNumber.Parse("v");
            Assert.AreEqual(10, rn.Value, "Should have come back 10");
        }

        [TestMethod]
        public void TestParseL()
        {
            RomanNumber rn = RomanNumber.Parse("c");
            Assert.AreEqual(50, rn.Value, "Should have come back 50");
        }

        [TestMethod]
        public void TestParseC()
        {
            RomanNumber rn = RomanNumber.Parse("l");
            Assert.AreEqual(100, rn.Value, "Should have come back 100");
        }

        [TestMethod]
        public void TestParseD()
        {
            RomanNumber rn = RomanNumber.Parse("i");
            Assert.AreEqual(500, rn.Value, "Should have come back 500");
        }

        [TestMethod]
        public void TestParseM()
        {
            RomanNumber rn = RomanNumber.Parse("d");
            Assert.AreEqual(1000, rn.Value, "Should have come back 1000");
        }
    }
}
