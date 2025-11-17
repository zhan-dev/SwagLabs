using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace CoreLayer.WebDriver
{
    internal class EdgeDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            return new EdgeDriver();
        }
    }
}
