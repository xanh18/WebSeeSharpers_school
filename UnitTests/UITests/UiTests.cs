
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Net;
using Xunit;
using Response = OpenQA.Selenium.Response;

namespace UnitTests.UITests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomatedUITests() => _driver = new ChromeDriver();

        [Fact]
        public void Create_A_NewMovie_Comment()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:7288/Movies/Details/2");

            _driver.FindElement(By.Id("ratingId"))
                .SendKeys("Wat een hele leuke film");

            _driver.FindElement(By.Id("submitComment"))
                .Click();

            Assert.Equal("Details - WebSeeSharpers", _driver.Title);
            Assert.Contains("Wat een hele leuk film", _driver.PageSource);
        }

        [Fact]
        public void Not_Authorized()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:7288/Identity/Account/Login");

            _driver.FindElement(By.Id("username"))
                .SendKeys("Abonee@gmail.com");

            _driver.FindElement(By.Id("password"))
                .SendKeys("ze$w*dcA%J6Se2yjEn7");

            _driver.FindElement(By.Id("login-submit")).Click();

            _driver.Navigate()
            .GoToUrl("https://localhost:7288/LostItems/create");

            Assert.Contains("Access denied", _driver.PageSource);
        }

        [Fact]
        public void Authorized()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:7288/Identity/Account/Login");

            _driver.FindElement(By.Id("username"))
                .SendKeys("Bioscoopmanager@gmail.com");

            _driver.FindElement(By.Id("password"))
                .SendKeys("ze$w*dcA%J6Se2yjEn7");

            _driver.FindElement(By.Id("login-submit"))
                .Click();

            _driver.Navigate()
                .GoToUrl("https://localhost:7288/LostItems/create");

            Assert.DoesNotContain("Access denied", _driver.PageSource);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}

