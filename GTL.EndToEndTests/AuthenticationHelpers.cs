using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace GTL.EndToEndTests
{
    public static class AuthenticationHelpers
    {

        public static bool IsLoggedIn(IWebDriver driver)
        {
            return ElementHelpers.IsElementPresent(driver, By.LinkText("Logout"));
        }

        public static void LoginAsTestUser(IWebDriver driver)
        {
            ElementHelpers.ClickElement(driver, By.LinkText("Login"));
            ElementHelpers.ClearElement(driver, By.Id("Email"));
            ElementHelpers.SendKeys(driver, By.Id("Email"), "test@gtl.dk");
            ElementHelpers.ClearElement(driver, By.Id("Password"));
            ElementHelpers.SendKeys(driver, By.Id("Password"), "12345678");
            ElementHelpers.ClickElement(driver, By.Id("login-button"));
        }

        public static void Logout(IWebDriver driver)
        {
            ElementHelpers.ClickElement(driver, By.LinkText("Logout"));
        }

    }
}
