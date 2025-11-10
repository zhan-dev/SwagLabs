using CoreLayer.Enum;
using CoreLayer.WebDriver;
using PageObjects.src;
using System;
using System.Runtime.CompilerServices;
using Xunit;

//string userName = "standard_user", string userPassword = "secret_sauce"

namespace SwagLabs.Tests.src
{
    public class LoginPageTests
    {
        [Fact]
        public void UC01_FillLogin_WithEmptyCredentials_UserNameIsRequired()
        {
            // Arrange
            var chromeDriver = BrowserDriver.CreateWebDriver(BrowserType.GoogleChrome);
            var loginPage = new LoginPage(chromeDriver).OpenLoginPage();

            // Act
            loginPage.FillLogin("Any", "credentials").ClearLogin().ClickLogin();

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(() => loginPage.ValidateLogin());
            Assert.Equal("Epic sadface: Username is required", ex.Message);
        }

        //[Fact]
        //public void ValidateLogin_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var loginPage = new LoginPage(TODO);

        //    // Act
        //    loginPage.ValidateLogin();

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public void IsCorrectLogin_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var loginPage = new LoginPage(TODO);

        //    // Act
        //    var result = loginPage.IsCorrectLogin();

        //    // Assert
        //    Assert.True(false);
        //}
    }
}
