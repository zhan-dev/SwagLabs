using OpenQA.Selenium;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SwagLabs.Tests")]

namespace PageObjects.src
{
    internal class LoginPage
    {
        private readonly IWebDriver? webDriver;
        private string Url { get; } = "https://www.saucedemo.com";
        private readonly By userNameBy = By.CssSelector("[data-test='username']");
        private readonly By userPasswordBy = By.CssSelector("[data-test='password']");
        private readonly By loginBy = By.CssSelector("[data-test='login-button']");
        private readonly By errorMsgBy = By.CssSelector("[data-test='error']");
        private readonly By footerBy = By.CssSelector("[data-test='footer-copy']");

        public LoginPage(IWebDriver? webDriver)
        {
            this.webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
        }

        public LoginPage OpenLoginPage()
        {
            this.webDriver!.Navigate().GoToUrl(Url);

            return this;
        }

        public LoginPage FillUserName(string userName)
        {
            var userNameInput = this.webDriver!.FindElement(userNameBy);
            userNameInput.Clear();
            userNameInput.SendKeys(userName);

            return this;
        }

        public LoginPage FillUserPassword(string userPassword)
        {
            var userPasswordInput = this.webDriver!.FindElement(userPasswordBy);
            userPasswordInput.Clear();
            userPasswordInput.SendKeys(userPassword);

            return this;
        }

        public LoginPage ClearUserName()
        {
            this.webDriver!.FindElement(userNameBy).Clear();

            return this;
        }

        public LoginPage ClearUserPassword()
        {
            this.webDriver!.FindElement(userPasswordBy).Clear();

            return this;
        }

        public LoginPage FillLogin(string userName, string userPassword)
        {
            this.FillUserName(userName);
            this.FillUserPassword(userPassword);

            return this;
        }

        public LoginPage ClearLogin()
        {
            this.ClearUserName();
            this.ClearUserPassword();

            return this;
        }

        public LoginPage ValidateLogin()
        {
            string requiredUsername = "Epic sadface: Username is required";
            string requiredPassword = "Epic sadface: Password is required";
            string wrongUser = "Epic sadface: Username and password do not match any user in this service";

            var errorMessage = this.webDriver!.FindElements(errorMsgBy).FirstOrDefault();

            if (errorMessage is null)
            {
                return this;
            }

            if (errorMessage.Text.Equals(requiredUsername))
            {
                ErrorMsg(requiredUsername);
            }

            if (errorMessage.Text.Equals(requiredPassword))
            {
                ErrorMsg(requiredPassword);
            }

            if (errorMessage.Text.Equals(wrongUser))
            {
                ErrorMsg(wrongUser);
            }

            static void ErrorMsg(string msg)
            {
                throw new InvalidOperationException(msg);
            }

            return this;
        }

        public LoginPage ClickLogin()
        {
            var loginButton = this.webDriver!.FindElement(loginBy);
            loginButton.Click();

            return this;
        }

        public bool CheckLoginResult()
        {
            var footer = this.webDriver!.FindElements(footerBy).FirstOrDefault()?.Displayed;

            if (footer is null || footer is false)
            {
                return false;
            }

            return true;
        }
    }
}
