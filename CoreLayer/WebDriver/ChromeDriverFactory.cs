using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CoreLayer.WebDriver
{
    internal class ChromeDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            var service = ChromeDriverService.CreateDefaultService();

            var options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--incognito");

            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            var driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(5));
            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}
