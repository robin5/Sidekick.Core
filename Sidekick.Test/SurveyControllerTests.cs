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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sidekick.Controllers;
using Sidekick.Models;
using Sidekick.ViewModels;
using Xunit;
using MockRepository = Sidekick.Models.MockRepository;

namespace Sidekick.Test
{
    public class SurveyControllerTests : ControllerTest
    {
        [Fact]
        public void Get_Index_BadId()
        {
            // Arrange
            var repository = new MockRepository();

            var surveyController = new SurveyController(null, repository, IdentityHelper);

            // Act
            var result = surveyController.Index(1);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            RedirectToActionResult redirectToActionResult = result as RedirectToActionResult;

            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void Get_Index_GoodId()
        {
            // Arrange
            var repository = new MockRepository();
            var survey = repository.User(UserId).AddSurvey(new Survey()
            {
                Name = "F2022 CTEC-235 SUMMER",
                Questions = new List<string>() { "Q1", }
            });

            var surveyController = new SurveyController(null, repository, IdentityHelper);

            // Act
            var result = surveyController.Index(survey.Id);

            // Assert
            Assert.IsType<ViewResult>(result);
            ViewResult view = result as ViewResult;

            Assert.IsType<SurveyIndexViewModel>(view.Model);
            SurveyIndexViewModel model = view.Model as SurveyIndexViewModel;

            Assert.Equal(survey.Id, model.Id);
            Assert.Equal(survey.Name, model.Name);
            Assert.Equal(survey.Questions.Count(), model.Questions.Count());
        }

        [Fact]
        public void Get_Create()
        {
            // Arrange
            var repository = new MockRepository();

            var surveyController = new SurveyController(null, repository, IdentityHelper);

            // Act
            var result = surveyController.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
            ViewResult view = result as ViewResult;

            Assert.IsType<SurveyCreateViewModel>(view.Model);
        }

        [Fact]
        public void Post_Create_ModelStateInvalid()
        {
            // Arrange
            var repository = new MockRepository();

            var surveyController = new SurveyController(null, repository, IdentityHelper);
            surveyController.ModelState.AddModelError("sesion", "required");

            var surveyName = "Test Survey";
            var surveyQuestions = new List<string>() { "Q1", "Q2", "Q3" };

            SurveyCreateViewModel viewModel = new SurveyCreateViewModel()
            {
                Name = surveyName,
                Questions = surveyQuestions
            };

            // Act
            var result = surveyController.Create(viewModel);

            // Assert
            Assert.IsType<ViewResult>(result);
            ViewResult view = result as ViewResult;

            var resultModel = Assert.IsType<SurveyCreateViewModel>(view.Model);
            Assert.Equal(surveyName, resultModel.Name);
            Assert.Equal(surveyQuestions, resultModel.Questions);
        }
        
        [Fact]
        public void Post_Create_ModelStateValid()
        {
            // Arrange
            var surveyName = "Test Survey";
            var surveyQuestions = new List<string>() { "Q1", "Q2", "Q3" };

            SurveyCreateViewModel viewModel = new SurveyCreateViewModel()
            {
                Name = surveyName,
                Questions = surveyQuestions
            };

            var repository = new MockRepository();

            var surveyController = new SurveyController(null, repository, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = surveyController.Create(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("ErrorMessage", surveyController.TempData.Keys);
            Assert.Contains("SuccessMessage", surveyController.TempData.Keys);
            Assert.Equal($"Successfully added { surveyName } to peer surveys.", surveyController.TempData["SuccessMessage"]);
        }
        
        [Fact]
        public void Post_Create_DatabaseException()
        {
            // Arrange
            var surveyName = "Test Survey";
            var surveyQuestions = new List<string>() { "Q1" };
            var databaseError = "Database error";
            var expectedMessage = $"Failed adding { surveyName } to peer surveys: { databaseError }";

            SurveyCreateViewModel viewModel = new SurveyCreateViewModel()
            {
                Name = surveyName,
                Questions = surveyQuestions
            };

            Survey survey = new Survey() 
            {
                UserId = UserId,
                Name = surveyName,
                Questions = surveyQuestions
            };

            // Mock the repository to throwing an exception when the AddSurvey call is executed
            var repository = new Mock<Sidekick.Models.MockRepository>();
            repository
                .Setup<Survey>(n => n.AddSurvey(It.IsAny<Survey>()))
                .Throws(new Exception(databaseError));

            // Create the controler
            var surveyController = new SurveyController(null, repository.Object, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = surveyController.Create(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("SuccessMessage", surveyController.TempData.Keys);
            Assert.Contains("ErrorMessage", surveyController.TempData.Keys);
            Assert.Equal(expectedMessage, surveyController.TempData["ErrorMessage"]);
        }

        [Fact]
        public void Get_Edit_GoodId()
        {
            // Arrange
            var repository = new MockRepository();
            var survey = repository.User(UserId).AddSurvey(new Survey()
            {
                Name = "F2022 CTEC-235 SUMMER",
                Questions = new List<string>() { "Q1", }
            });

            var surveyController = new SurveyController(null, repository, IdentityHelper);

            // Act
            var result = surveyController.Edit(survey.Id);

            // Assert
            ViewResult view = Assert.IsType<ViewResult>(result);

            SurveyEditViewModel model = Assert.IsType<SurveyEditViewModel>(view.Model);

            Assert.Equal(survey.Id, model.Id);
            Assert.Equal(survey.Name, model.Name);
            Assert.Equal(survey.Questions.Count(), model.Questions.Count());
        }

        [Fact]
        public void Get_Edit_BadId()
        {
            // Arrange
            var repository = new MockRepository();
            var survey = repository.User(UserId).AddSurvey(new Survey()
            {
                Name = "F2022 CTEC-235 SUMMER",
                Questions = new List<string>() { "Q1", }
            });

            var surveyController = new SurveyController(null, repository, IdentityHelper);

            // Act
            var result = surveyController.Edit(survey.Id + 1); // Any survey.Id other than what was created will be a bad value

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            RedirectToActionResult redirectToActionResult = result as RedirectToActionResult;

            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
        }
        [Fact]
        public void Post_Edit_ModelStateInvalid()
        {
            // Arrange
            var repository = new MockRepository();

            var surveyController = new SurveyController(null, repository, IdentityHelper);
            surveyController.ModelState.AddModelError("", "");

            // Note the specific Id, Name , or Value does not  matter as long 
            // as they are verified to be the same in the returning result
            var surveyId = 1;
            var surveyName = "Test Survey";
            var surveyQuestions = new List<string>();

            SurveyEditViewModel viewModel = new SurveyEditViewModel()
            {
                Id = 1,
                Name = surveyName,
                Questions = surveyQuestions
            };

            // Act
            var result = surveyController.Edit(viewModel);

            // Assert
            var view = Assert.IsType<ViewResult>(result);

            var resultModel = Assert.IsType<SurveyEditViewModel>(view.Model);
            Assert.Equal(surveyId, resultModel.Id);
            Assert.Equal(surveyName, resultModel.Name);
            Assert.Equal(surveyQuestions, resultModel.Questions);
        }

        [Fact]
        public void Post_Edit_ModelStateValid()
        {
            // Arrange -----
            var repository = new MockRepository();
            var survey = repository.User(UserId).AddSurvey(new Survey()
            {
                Name = "original-survey-name",
                Questions = new List<string>() { "original-survey-question" }
            });

            // Define the viewModel containing the updated questions
            var updatedSurveyName = "Test Survey";
            var updatedSurveyQuestions = new List<string>() { "Q1", "Q2", "Q3" };

            SurveyEditViewModel viewModel = new SurveyEditViewModel()
            {
                Id = survey.Id,
                Name = updatedSurveyName,
                Questions = updatedSurveyQuestions
            };

            var surveyController = new SurveyController(null, repository, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act ------
            var result = surveyController.Edit(viewModel);

            // Assert -----
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            // Verify redirection and success message
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("ErrorMessage", surveyController.TempData.Keys);
            Assert.Contains("SuccessMessage", surveyController.TempData.Keys);
            Assert.Equal($"Successfully updated { updatedSurveyName }.", surveyController.TempData["SuccessMessage"]);

            // Verify database update
            var updatedSurvey = repository.GetSurvey(survey.Id);
            Assert.Equal(survey.Name, updatedSurvey.Name);
            Assert.Equal(survey.Questions, updatedSurvey.Questions);
        }
    }
}


