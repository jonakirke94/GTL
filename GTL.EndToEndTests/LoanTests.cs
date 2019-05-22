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
                ElementHelpers.ClickElement(_driver, By.PartialLinkText("New Loan"));
                ElementHelpers.SendKeys(_driver, By.Id("LoanerCardBarcode"), "10000");
                ElementHelpers.SendKeys(_driver, By.Id("CopyBarcode"), "100001");
                ElementHelpers.SelectElementByText(_driver, By.Id("LibraryName"), "Georgia Tech Library");
                ElementHelpers.ClickElement(_driver, By.Id("Create"));

                isCreateLoanConfirmationShown = ElementHelpers.IsElementPresent(_driver, By.ClassName("alert-success"));

                // Assert
                Assert.True(isCreateLoanConfirmationShown);
            }
            finally
            {
                DriverHelpers.CleanUpTest(_driver);

            }
        }

    }
}
