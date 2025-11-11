using CoreLayer.Enum;
using CoreLayer.WebDriver;
using OpenQA.Selenium;
using PageObjects.src;

namespace SwagLabs.Tests.src
{
    public class LoginPageTests : IDisposable
    {
        private readonly IWebDriver driver;
        private bool disposed;

        public LoginPageTests()
        {
            this.driver = BrowserDriver.CreateWebDriver(BrowserType.GoogleChrome);
        }

        [Fact]
        public void UC01_FillLogin_WithEmptyCredentials_UserNameIsRequired()
        {
            // Arrange
            var loginPage = new LoginPage(this.driver).OpenLoginPage();

            // Act
            loginPage.FillLogin("AnyUC01", "credentialsUC01").ClearUserName().ClearUserPassword().ClickLogin().ValidateIncorrectLoginData();

            // Assert
            Assert.Equal("Epic sadface: Username is required", loginPage.ErrorMsg);
        }

        [Fact]
        public void UC02_FillLogin_WithUserNameCredentials_PasswordIsRequired()
        {
            // Arrange
            var loginPage = new LoginPage(this.driver).OpenLoginPage();

            // Act
            loginPage.FillLogin("AnyUC02", "credentialsUC02").ClearUserPassword().ClickLogin().ValidateIncorrectLoginData();

            // Assert
            Assert.Equal("Epic sadface: Password is required", loginPage.ErrorMsg);
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
            var loginPage = new LoginPage(this.driver).OpenLoginPage();
            string userPassword = "secret_sauce";

            // Act
            loginPage.FillLogin(userName, userPassword).ClickLogin().ValidateCorrectLoginData();

            // Assert
            Assert.Equal("Swag Labs", loginPage.ProductsPageSwagLabHeader);
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
