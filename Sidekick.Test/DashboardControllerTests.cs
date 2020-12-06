using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sidekick.Controllers;
using Sidekick.Models;
using Xunit;

namespace Sidekick.Test
{
    public class DashboardControllerTests
    {
        [Fact]
        public void GetIndex()
        {
            // Arrange
            var repository = new MockRepository();
            var surveys = repository.GetSurveys();
            var teams = repository.GetTeams();
            var launchedSurveys = repository.GetLaunchedSurveys();

            var dashboardController = new DashboardController(null, repository);

            // Act
            var result = dashboardController.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
            ViewResult view = result as ViewResult;

            Assert.IsType<DashboardViewModel>(view.Model);
            DashboardViewModel model = view.Model as DashboardViewModel;
            
            Assert.Equal(surveys.Count(), model.Surveys.Count());
            Assert.Equal(teams.Count(), model.Teams.Count());
            Assert.Equal(launchedSurveys.Count(), model.LaunchedSurveys.Count());
        }
    }
}
