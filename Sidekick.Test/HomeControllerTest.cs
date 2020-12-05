using Microsoft.AspNetCore.Mvc;
using Sidekick.Controllers;
using Sidekick.Models;
using System;
using Xunit;

namespace Sidekick.Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void GetIndex()
        {
            // Arrange
            var homeController = new HomeController(null);

            // Act
            var result = homeController.Index();
            
            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void GetPrivacy()
        {
            // Arrange
            var homeController = new HomeController(null);

            // Act
            var result = homeController.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
