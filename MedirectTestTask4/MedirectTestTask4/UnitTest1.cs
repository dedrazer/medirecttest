using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("D:\\3rdparty\\chrome");
        }

        [Test]
        public void test()
        {
            driver.Url = "http://www.google.co.in";
        }
    }
}