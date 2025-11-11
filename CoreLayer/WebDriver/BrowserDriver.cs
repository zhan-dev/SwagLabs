using CoreLayer.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SwagLabs.Tests")]

namespace CoreLayer.WebDriver
{
    internal static class BrowserDriver
    {
        public static IWebDriver CreateWebDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.GoogleChrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        //service.HideCommandPromptWindow = true;

                        var options = new ChromeOptions();
                        options.AddExcludedArgument("enable-automation");
                        options.AddArgument("--incognito");
                        //options.AddAdditionalOption("useAutomationExtension", false);
                        //options.AddArgument("--disable-blink-features=AutomationControlled");

                        var driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(5));
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                        driver.Manage().Window.Maximize();

                        return driver;
                    }

                case BrowserType.MozillaFirefox:
                    return new FirefoxDriver();

                case BrowserType.MicrosoftEdge:
                    return new EdgeDriver();

                case BrowserType.InternetExplorer:
                    return new InternetExplorerDriver();

                case BrowserType.Safari:
                    return new SafariDriver();

                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}
