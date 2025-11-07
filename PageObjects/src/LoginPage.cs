using OpenQA.Selenium;

namespace PageObjects.src
{
    internal class LoginPage
    {
        private readonly IWebDriver? webDriver;
        private readonly int timeout = 5;
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

        public LoginPage OpenPage()
        {
            this.webDriver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
            this.webDriver!.Navigate().GoToUrl(Url);
            this.webDriver!.Manage().Window.Maximize();

            return this;
        }

        public void FillLogin(string userName = "standard_user", string userPassword = "secret_sauce")
        {
            var userNameInput = this.webDriver!.FindElement(userNameBy);
            userNameInput.Clear();
            userNameInput.SendKeys(userName);

            var userPasswordInput = this.webDriver!.FindElement(userPasswordBy);
            userPasswordInput.Clear();
            userPasswordInput.SendKeys(userPassword);

            var loginButton = this.webDriver!.FindElement(loginBy);
            loginButton.Click();
        }

        public void ValidateLogin()
        {
            var errorMessage = this.webDriver!.FindElement(errorMsgBy);

            string requiredUsername = "Epic sadface: Username is required";
            string requiredPassword = "Epic sadface: Password is required";
            string wrongUser = "Epic sadface: Username and password do not match any user in this service";

            if (!errorMessage.Displayed)
            {
                return;
            }

            if (errorMessage.Text.Equals(requiredUsername))
            {
                throw new ArgumentException(requiredUsername);
            }

            if (errorMessage.Text.Equals(requiredPassword))
            {
                throw new ArgumentException(requiredPassword);
            }

            if (errorMessage.Text.Equals(wrongUser))
            {
                throw new ArgumentException(wrongUser);
            }
        }

        public bool IsCorrectLogin()
        {
            return this.webDriver!.FindElement(footerBy).Displayed;
        }
    }
}
