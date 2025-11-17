using OpenQA.Selenium;
using OpenQA.Selenium.Safari;

namespace CoreLayer.WebDriver
{
    internal class SafariDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            return new SafariDriver();
        }
    }
}
