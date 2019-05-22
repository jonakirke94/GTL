using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TestDatabaseManager;

namespace GTL.EndToEndTests
{
    public static class DriverHelpers
    {

        public static string BaseURL { get; private set; } = "http://gtl.vefi.dk";

        public static IWebDriver GetChromeDriver()
        {
            IWebDriver driver = new ChromeDriver(GetDriverPath());
            NavigationHelpers.NavigateToStartPage(driver);
            return driver;
        }

        private static string GetDriverPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static IWebDriver GetDriver(string browser)
        {
            if (browser.ToLower().Equals("chrome"))
            {
                return GetChromeDriver();
            }

            return null;
        }

        internal static void CleanUpTest(IWebDriver driver)
        {
            driver.Close();
            ScriptRunner.ResetDatabase();
        }
    }
}
