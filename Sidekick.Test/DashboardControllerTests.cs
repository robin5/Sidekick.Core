﻿// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: DashboardControllerTest.cs
// *
// * Description: Tests for DashboardController
// *
// * Author: Robin Murray
// *
// **********************************************************************************
// *
// * Granting License: The MIT License (MIT)
// * 
// *   Permission is hereby granted, free of charge, to any person obtaining a copy
// *   of this software and associated documentation files (the "Software"), to deal
// *   in the Software without restriction, including without limitation the rights
// *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// *   copies of the Software, and to permit persons to whom the Software is
// *   furnished to do so, subject to the following conditions:
// *   The above copyright notice and this permission notice shall be included in
// *   all copies or substantial portions of the Software.
// *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// *   THE SOFTWARE.
// * 
// **********************************************************************************

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
            var surveys = repository.GetAllSurveyNameIds("TBD");
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
