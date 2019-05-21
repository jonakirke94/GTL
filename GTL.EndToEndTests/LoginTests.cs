using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace GTL.EndToEndTests
{
    public class LoginTests
    {
        private readonly IWebDriver _driver = DriverHelpers.GetChromeDriver();

        [Fact]
        public void CanLoginTest()
        {
            try
            {
                // Act
                AuthenticationHelpers.LoginAsTestUser(_driver);

                // Assert
                Assert.True(AuthenticationHelpers.IsLoggedIn(_driver));
            }
            finally
            {
                _driver.Close();
            }

        }

        [Fact]
        public void CanLogoutTest()
        {
            try
            {
                // Arrange
                AuthenticationHelpers.LoginAsTestUser(_driver);

                // Act
                AuthenticationHelpers.Logout(_driver);

                // Assert
                Assert.False(AuthenticationHelpers.IsLoggedIn(_driver));
            }
            finally
            {
                _driver.Close();
            }
        }
    }
}
