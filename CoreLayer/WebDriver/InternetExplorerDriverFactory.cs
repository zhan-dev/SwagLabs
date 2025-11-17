using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace CoreLayer.WebDriver
{
    internal class InternetExplorerDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            return new InternetExplorerDriver();
        }
    }
}
