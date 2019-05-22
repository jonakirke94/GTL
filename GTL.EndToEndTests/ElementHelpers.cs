using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GTL.EndToEndTests
{
    public static class ElementHelpers
    {

        internal static bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        internal static void ClickElement(IWebDriver driver, By by)
        {
            if (IsElementPresent(driver, by))
            {
                driver.FindElement(by).Click();
            }
        }

        internal static void SendKeys(IWebDriver driver, By by, string keys)
        {
            if (IsElementPresent(driver, by))
            {
                driver.FindElement(by).SendKeys(keys);
            }
        }

        internal static void ClearElement(IWebDriver driver, By by)
        {
            if (IsElementPresent(driver, by))
            {
                driver.FindElement(by).Clear();
            }
        }

        internal static void SelectElementByText(IWebDriver driver, By by, string text)
        {
            if (IsElementPresent(driver, By.Id("LibraryName")))
            {
                new SelectElement(driver.FindElement(by)).SelectByText(text);
            }
        }

        internal static void SelectElementByValue(IWebDriver driver, By by, string value)
        {
            if (IsElementPresent(driver, By.Id("LibraryName")))
            {
                new SelectElement(driver.FindElement(by)).SelectByValue(value);
            }
        }

        internal static void SelectElementByValue(IWebDriver driver, By by, int index)
        {
            if (IsElementPresent(driver, By.Id("LibraryName")))
            {
                new SelectElement(driver.FindElement(by)).SelectByIndex(index);
            }
        }

        internal static bool IsAlertPresent(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

    }
}
