using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTL.EndToEndTests
{
    public static class NavigationHelpers
    {

        public static void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void NavigateToStartPage(IWebDriver driver)
        {
            NavigateToUrl(driver, DriverHelpers.BaseURL);
        }

    }
}
