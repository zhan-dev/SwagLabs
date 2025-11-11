using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        private readonly By productsPageSwagLabHeaderBy = By.CssSelector(".app_logo");
        public string? ErrorMsg { get; private set; }
        public string? ProductsPageSwagLabHeader { get; private set; }

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
            var userNameInput = this.webDriver!.FindElement(userNameBy);
            userNameInput.Click();
            userNameInput.SendKeys(Keys.Control + "a");
            userNameInput.SendKeys(Keys.Delete);

            return this;
        }

        public LoginPage ClearUserPassword()
        {
            var userPasswordInput = this.webDriver!.FindElement(userPasswordBy);
            userPasswordInput.Click();
            userPasswordInput.SendKeys(Keys.Control + "a");
            userPasswordInput.SendKeys(Keys.Delete);

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

        public LoginPage ValidateIncorrectLoginData()
        {
            string requiredUsername = "Epic sadface: Username is required";
            string requiredPassword = "Epic sadface: Password is required";
            string wrongUser = "Epic sadface: Username and password do not match any user in this service";

            var errorMessage = this.webDriver!.FindElements(errorMsgBy).FirstOrDefault();

            if (errorMessage is null)
            {
                this.ErrorMsg = null;
                return this;
            }

            if (errorMessage.Text.Equals(requiredUsername))
            {
                this.ErrorMsg = requiredUsername;
            }
            else if (errorMessage.Text.Equals(requiredPassword))
            {
                this.ErrorMsg = requiredPassword;
            }
            else if (errorMessage.Text.Equals(wrongUser))
            {
                this.ErrorMsg = wrongUser;
            }
            else
            {
                this.ErrorMsg = errorMessage.Text;
            }

                return this;
        }

        public LoginPage ValidateCorrectLoginData()
        {
            var wait = new WebDriverWait(this.webDriver!, TimeSpan.FromSeconds(3));
            try
            {
                var productSwagLabHeader = wait.Until(drv => drv.FindElement(productsPageSwagLabHeaderBy));
                this.ProductsPageSwagLabHeader = productSwagLabHeader.Text;
            }
            catch (WebDriverException ex)
            {
                this.ProductsPageSwagLabHeader = ex.Message;
            }

            return this;
        }

        public LoginPage ClickLogin()
        {
            var wait = new WebDriverWait(this.webDriver!, TimeSpan.FromSeconds(3));

            try
            {
                var logButton = wait.Until(x => x.FindElement(loginBy));
                logButton.Click();
            }
            catch (WebDriverException ex)
            {

                this.ProductsPageSwagLabHeader = ex.Message;
                throw new WebDriverException(this.ProductsPageSwagLabHeader);
            }

            return this;
        }
    }
}
