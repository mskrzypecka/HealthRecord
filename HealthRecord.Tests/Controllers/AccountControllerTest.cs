﻿using System.Threading.Tasks;
using System.Web.Mvc;
using HealthRecord.Controllers;
using HealthRecord.Data.Entities;
using HealthRecord.Identity;
using HealthRecord.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HealthRecord.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public async Task ShouldRegisterUser()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "test@test.pl",
                UserName = "test@test.pl"
            };

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore.Object);
            
            var authenticationManager = new Mock<IAuthenticationManager>();
            var signInManager = new Mock<ApplicationSignInManager>(userManager.Object, authenticationManager.Object);

            // Act
            userManager.Setup(x => x.CreateAsync(It.Is<ApplicationUser>(u => u.Email == "test@test.pl"), It.Is<string>(s => s == "12Ed6fg#"))).ReturnsAsync(IdentityResult.Success);
            var controller = new AccountController(userManager.Object, signInManager.Object, new Data.ApplicationDbContext());

            RedirectToRouteResult result = await controller.Register(new RegisterViewModel
            {
                Email = "test@test.pl",
                Password = "12Ed6fg#",
                ConfirmPassword = "12Ed6fg#"
            }) as RedirectToRouteResult;
            
            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual("Profile", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
