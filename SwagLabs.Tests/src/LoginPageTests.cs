using CoreLayer.WebDriver;
using FluentAssertions;
using log4net;
using OpenQA.Selenium;
using PageObjects.src;
using Xunit.Abstractions;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace SwagLabs.Tests.src
{
    public class LoginPageTests : IDisposable
    {
        private readonly IWebDriver driver;
        private bool disposed;
        private readonly TestLogger logger;
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginPageTests));

        public LoginPageTests(ITestOutputHelper output)
        {
            this.driver = new ChromeDriverFactory().CreateDriver();
            this.logger = new TestLogger(output);

            var hierarchy = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            var appender = new XUnitOutputAppender(output);
            appender.Layout = new log4net.Layout.PatternLayout("%date %-5level %logger - %message%newline");
            hierarchy.Root.AddAppender(appender);
            hierarchy.Configured = true;
        }

        [Theory]
        [InlineData("AnyUC01", "credentialsUC01")]
        public void UC01_FillLogin_WithEmptyCredentials_UserNameIsRequired(string userName, string password)
        {
            // Arrange
            this.logger.Step("UC01: Open login page");
            log.Info("UC01: Open login page");
            var loginPage = new LoginPage(this.driver).OpenLoginPage();

            // Act
            this.logger.Info("UC01: Enter login and password. Clear fields. Try to login.");
            log.Debug("UC01: Enter login and password. Clear fields. Try to login.");
            loginPage.FillLogin(userName, password)
                .ClearUserName()
                .ClearUserPassword()
                .ClickLogin()
                .ValidateIncorrectLoginData();

            // Assert
            this.logger.Result($"UC01: expected 'Epic sadface: Username is required', was: '{loginPage.ErrorMsg}'");
            log.Warn($"UC01: expected 'Epic sadface: Username is required', was: '{loginPage.ErrorMsg}'");
            loginPage.ErrorMsg.Should().Be("Epic sadface: Username is required");
        }

        [Theory]
        [InlineData("AnyUC02", "credentialsUC02")]
        public void UC02_FillLogin_WithUserNameCredentials_PasswordIsRequired(string userName, string password)
        {
            // Arrange
            this.logger.Step("UC02: Open login page.");
            log.Info("UC02: Open login page.");
            var loginPage = new LoginPage(this.driver).OpenLoginPage();

            // Act
            this.logger.Info("UC02: Enter login and password. Clear password. Try to login.");
            log.Debug("UC02: Enter login and password. Clear password. Try to login.");
            loginPage.FillLogin(userName, password).
                ClearUserPassword()
                .ClickLogin()
                .ValidateIncorrectLoginData();

            // Assert
            this.logger.Result($"UC02: expected 'Epic sadface: Password is required', was: '{loginPage.ErrorMsg}'");
            log.Warn($"UC02: expected 'Epic sadface: Password is required', was: '{loginPage.ErrorMsg}'");
            loginPage.ErrorMsg.Should().Be("Epic sadface: Password is required");
        }

        [Theory]
        [InlineData("standard_user")]
        [InlineData("locked_out_user")]
        [InlineData("problem_user")]
        [InlineData("performance_glitch_user")]
        [InlineData("error_user")]
        [InlineData("visual_user")]
        public void UC03_FillLogin_WithUserNameAndPasswordCredentials_IsValidUser(string userName)
        {
            // Arrange
            this.logger.Step("UC03: Open login page.");
            log.Info("UC03: Open login page.");
            var loginPage = new LoginPage(this.driver).OpenLoginPage();
            string userPassword = "secret_sauce";

            // Act
            this.logger.Info($"UC03: Enter login and password. Try to login '{userName}'.");
            log.Debug($"UC03: Enter login and password. Try to login '{userName}'.");
            loginPage.FillLogin(userName, userPassword)
                .ClickLogin()
                .ValidateCorrectLoginData();

            // Assert
            this.logger.Result($"UC03: for {userName} expected 'Swag Labs', was: {loginPage.ProductsPageSwagLabHeader}");
            log.Warn($"UC03: for {userName} expected 'Swag Labs', was: {loginPage.ProductsPageSwagLabHeader}");
            loginPage.ProductsPageSwagLabHeader.Should().Be("Swag Labs");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                driver?.Quit();
            }

            disposed = true;
        }
    }
}
