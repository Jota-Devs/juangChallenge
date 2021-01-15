using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheSchoolTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SampleUnitTest()
        {
            int a = 1;
            int b = 2;
            Assert.IsTrue(a + b == 3);
        }
    }
}
