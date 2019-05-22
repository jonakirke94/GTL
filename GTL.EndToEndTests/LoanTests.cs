using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestDatabaseManager;
using Xunit;

namespace GTL.EndToEndTests
{
    public class LoanTests
    {
        private readonly IWebDriver _driver = DriverHelpers.GetChromeDriver();

        [Fact]
        public void CanCreateLoanTest()
        {
            try
            {
                // Arrange
                if (!AuthenticationHelpers.IsLoggedIn(_driver))
                {
                    AuthenticationHelpers.LoginAsTestUser(_driver);
                }

                bool isCreateLoanConfirmationShown = false;

                // Act
                _driver.FindElement(By.PartialLinkText("New Loan")).Click();
                _driver.FindElement(By.Id("LoanerCardBarcode")).SendKeys("10000");
                _driver.FindElement(By.Id("CopyBarcode")).SendKeys("100001");
                new SelectElement(_driver.FindElement(By.Id("LibraryName"))).SelectByText("Georgia Tech Library");
                _driver.FindElement(By.Id("Create")).Click();

                isCreateLoanConfirmationShown = ElementHelpers.IsElementPresent(_driver, By.ClassName("alert-success"));

                // Assert
                Assert.True(isCreateLoanConfirmationShown);
            }
            finally
            {
                _driver.Close();
                ScriptRunner.ResetDatabase();
            }
        }

    }
}
