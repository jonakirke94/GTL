using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
                AuthenticationHelpers.LoginAsTestUser(_driver);

                // Act
                _driver.FindElement(By.PartialLinkText("New Loan")).Click();
                _driver.FindElement(By.Id("LoanerCardBarcode")).SendKeys("10000");
                _driver.FindElement(By.Id("CopyBarcode")).SendKeys("100000");
                new SelectElement(_driver.FindElement(By.Id("LibraryName"))).SelectByText("Georgia Tech Library");
                _driver.FindElement(By.Id("Create")).Click();

                bool isCreateLoanConfirmationShown = ElementHelpers.IsElementPresent(_driver, By.ClassName("alert alert-success alert-dismissible"));

                // Assert
                Assert.True(isCreateLoanConfirmationShown);
            }
            finally
            {
                _driver.Close();
            }



        }

    }
}
