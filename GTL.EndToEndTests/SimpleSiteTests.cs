using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Xunit;

namespace GTL.EndToEndTests
{
    public class SimpleSiteTests
    {
        private IWebDriver _driver;

        [Theory]
        [InlineData("chrome")]
        public void CanOpenStartPageInBrowserTest(string browser)
        {
            try
            {
                _driver = DriverHelpers.GetDriver(browser);
                Assert.True(ElementHelpers.IsElementPresent(_driver, By.LinkText("GTL")));
            }
            finally
            {
                DriverHelpers.CleanUpTest(_driver);
            }
        }

    }
}
