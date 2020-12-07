// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: SurveyControllerTests.cs
// *
// * Description: Tests for SurveyController
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

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sidekick.Controllers;
using Sidekick.Models;
using Sidekick.ViewModels;
using Xunit;

namespace Sidekick.Test
{
    public class SurveyControllerTests
    {
        [Fact]
        public void GetIndexBadId()
        {
            // Arrange
            var repository = new MockRepository();

            var surveyController = new SurveyController(null, repository);

            // Act
            var result = surveyController.Index(99);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            RedirectToActionResult redirectToActionResult = result as RedirectToActionResult;

            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void GetIndexGoodId()
        {
            // Arrange
            var repository = new MockRepository();
            var survey = new Survey()
            {
                Id = 1,
                Name = "F2022 CTEC-235 SUMMER",
                Questions = new List<string>() { "Q1", }
            };
            repository.AddSurvey("TestUser", survey);

            var surveyController = new SurveyController(null, repository);

            // Act
            var result = surveyController.Index(1);

            // Assert
            Assert.IsType<ViewResult>(result);
            ViewResult view = result as ViewResult;

            Assert.IsType<SurveyIndexViewModel>(view.Model);
            SurveyIndexViewModel model = view.Model as SurveyIndexViewModel;

            Assert.Equal(survey.Id, model.Id);
            Assert.Equal(survey.Name, model.Name);
            Assert.Equal(survey.Questions.Count(), model.Questions.Count());
        }
    }
}
