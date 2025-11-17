using OpenQA.Selenium;

namespace CoreLayer.WebDriver
{
    internal interface IWebDriverFactory
    {
        IWebDriver CreateDriver();
    }
}
