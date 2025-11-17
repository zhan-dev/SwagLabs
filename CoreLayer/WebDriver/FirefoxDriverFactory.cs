using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CoreLayer.WebDriver
{
    internal class FirefoxDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            return new FirefoxDriver();
        }
    }
}