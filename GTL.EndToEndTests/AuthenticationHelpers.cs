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
            try
            {
                return driver.FindElement(By.LinkText("Logout")).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void LoginAsTestUser(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("test@gtl.dk");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("12345678");
            driver.FindElement(By.Id("login-button")).Click();
        }

        public static void Logout(IWebDriver driver)
        {
            if (ElementHelpers.IsElementPresent(driver, By.LinkText("Logout")))
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }

    }
}
