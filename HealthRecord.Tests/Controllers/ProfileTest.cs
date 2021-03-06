﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HealthRecord.Tests.Controllers
{
    [TestClass]
    public class ProfileTest
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void Init()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(@"http://localhost:60995/");
        }

        [TestMethod]
        public void ShouldChangeUserData()
        {
            _driver.FindElement(By.Id("loginLink")).Click();
            _driver.FindElement(By.Name("Email")).SendKeys("test@test.pl");
            _driver.FindElement(By.Name("Password")).SendKeys("test1234");
            _driver.FindElement(By.Id("loginSubmit")).Click();
        }
    }
}
