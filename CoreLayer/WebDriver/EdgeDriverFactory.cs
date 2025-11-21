using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace CoreLayer.WebDriver
{
    internal class EdgeDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            var service = EdgeDriverService.CreateDefaultService();

            var options = new EdgeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--inprivate");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            var driver = new EdgeDriver(service, options, TimeSpan.FromSeconds(5));
            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}
