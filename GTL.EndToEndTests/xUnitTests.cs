using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace GTL.EndToEndTests
{
    public class xUnitTests
    {
        private IWebDriver driver;

        private string GetDriverPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        [Fact]
        public void CanOpenGoogleTest()
        {
            driver = new ChromeDriver(GetDriverPath());

            driver.Navigate().GoToUrl("http://google.com/");

            Console.WriteLine("Test completed");

            driver.Close();
        }

        [Fact]
        public void SomeSampleTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44327/");
            driver.FindElement(By.LinkText("GTL")).Click();
        }
        private bool IsElementPresent(By by)
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

        private bool IsAlertPresent()
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
