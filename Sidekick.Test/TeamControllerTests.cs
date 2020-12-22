// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: TeamControllerTests.cs
// *
// * Description: Tests for TeamController
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
    public class TeamControllerTests : ControllerTest
    {
        [Fact]
        public void Get_Index_BadId()
        {
            // Arrange
            var repository = new MockRepository();

            var teamController = new TeamController(null, repository, IdentityHelper);

            // Act
            var result = teamController.Index(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void Get_Index_GoodId()
        {
            // Arrange
            var repository = new MockRepository();

            repository.UserId = UserId;

            // Get list of student UserIds
            var memberIds = new List<string>();
            var students = repository.GetAllStudents();
            foreach(var student in students)
            {
                memberIds.Add(student.UserId);
            }

            // Create a team consiting of the list of students
            var team = repository.User(UserId).AddTeam(new Team()
            {
                UserId = UserId,
                Name = NewRandomString(),
                MemberIds = memberIds
            });

            // Create the controller under test
            var teamController = new TeamController(null, repository, IdentityHelper);

            // Act
            var result = teamController.Index(team.Id);

            // Assert
            var view = Assert.IsType<ViewResult>(result);

            var viewModel = Assert.IsType<TeamIndexViewModel>(view.Model);

            // Assert that all members of the team have been included in the ViewModel
            var teamStudents = viewModel.TeamStudents;
            Assert.Equal(memberIds.Count(), teamStudents.Count());
            foreach(TeamStudent teamStudent in teamStudents)
            {
                Assert.Equal(team.UserId, teamStudent.Team.UserId);
                Assert.Equal(team.Id, teamStudent.Team.Id);
                Assert.Equal(team.Name, teamStudent.Team.Name);
                Assert.Contains(teamStudent.Student.UserId, memberIds);
            }
        }

        [Fact]
        public void Get_Create()
        {
            // Arrange
            var repository = new MockRepository();

            var teamController = new TeamController(null, repository, IdentityHelper);

            // Act
            var result = teamController.Create();

            // Assert
            var view = Assert.IsType<ViewResult>(result);

            Assert.IsType<TeamCreateViewModel>(view.Model);
        }

        [Fact]
        public void Post_Create_ModelStateInvalid()
        {
            // Arrange
            var repository = new MockRepository();

            var teamController = new TeamController(
                null, 
                repository, 
                IdentityHelper);

            teamController.ModelState.AddModelError(
                NewRandomString(),
                NewRandomString());

            var teamName = NewRandomString();

            var viewModel = new TeamCreateViewModel() 
            {
                Name = teamName,
            };

            // Act
            var iActionResult = teamController.Create(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(iActionResult);

            var teamCreateViewModel = Assert.IsType<TeamCreateViewModel>(viewResult.Model);
            Assert.Equal(viewModel, teamCreateViewModel);
            Assert.Equal(teamName, teamCreateViewModel.Name);
        }

        [Fact]
        public void Post_Create_ModelStateValid()
        {
            // Arrange
            var teamName = NewRandomString();
            var peerSelection = new List<string>() 
            {
                NewRandomString(),
                NewRandomString(),
                NewRandomString(),
            };

            var viewModel = new TeamCreateViewModel()
            {
                Name = teamName,
                PeerSelection = peerSelection
            };

            var repository = new MockRepository();

            var teamController = new TeamController(null, repository, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = teamController.Create(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("ErrorMessage", teamController.TempData.Keys);
            Assert.Contains("SuccessMessage", teamController.TempData.Keys);
            Assert.Equal($"Successfully added { teamName } to peer groups.", teamController.TempData["SuccessMessage"]);

            var team = repository.GetTeam(1);
            Assert.NotNull(team);
            Assert.Equal(teamName, team.Name);
            Assert.True(Enumerable.SequenceEqual<string>(peerSelection, team.MemberIds));
        }

        [Fact]
        public void Post_Create_DatabaseException()
        {
            // Arrange
            var teamName = NewRandomString();
            var peerSelection = new List<string>()
            {
                NewRandomString(),
                NewRandomString(),
                NewRandomString(),
            };

            var databaseError = NewRandomString();
            var expectedMessage = $"Failed adding { teamName } to peer groups: { databaseError }";

            var viewModel = new TeamCreateViewModel()
            {
                Name = teamName,
                PeerSelection = peerSelection
            };

            // Mock the repository to throwing an exception when the AddTeam call is executed
            var repository = new Mock<MockRepository>();
            repository
                .Setup<Team>(n => n.AddTeam(It.IsAny<Team>()))
                .Throws(new Exception(databaseError));

            // Create the controller
            var teamController = new TeamController(null, repository.Object, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = teamController.Create(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("SuccessMessage", teamController.TempData.Keys);
            Assert.Contains("ErrorMessage", teamController.TempData.Keys);
            Assert.Equal(expectedMessage, teamController.TempData["ErrorMessage"]);
        }

        [Fact]
        public void Get_Edit_GoodId()
        {
            // Arrange
            var repository = new MockRepository();
            var team = repository.User(UserId).AddTeam(new Team()
            {
                Name = NewRandomString(),
                MemberIds = new List<string>() { NewRandomString(), NewRandomString(), NewRandomString() }
            });

            var teamController = new TeamController(null, repository, IdentityHelper);

            // Act
            var result = teamController.Edit(team.Id);

            // Assert
            var view = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<TeamEditViewModel>(view.Model);

            Assert.Equal(team.Id, model.Id);
            Assert.Equal(team.Name, model.Name);
            Assert.True(Enumerable.SequenceEqual<string>(team.MemberIds, model.PeerSelection));
        }

        [Fact]
        public void Get_Edit_BadId()
        {
            // Arrange
            var repository = new MockRepository();
            var team = repository.User(UserId).AddTeam(new Team()
            {
                Name = NewRandomString(),
                MemberIds = new List<string>() { NewRandomString(), NewRandomString(), NewRandomString() }
            });

            var teamController = new TeamController(null, repository, IdentityHelper);

            // Act
            var result = teamController.Edit(team.Id + 1); // Any team.Id other than what was created will be a bad value

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
        }

        [Fact]
        public void Post_Edit_ModelStateInvalid()
        {
            // Arrange
            var repository = new MockRepository();

            var teamController = new TeamController(null, repository, IdentityHelper);
            teamController.ModelState.AddModelError(NewRandomString(), NewRandomString());

            // Note the specific Id, Name , or Value does not  matter as long 
            // as they are verified to be the same in the returning result
            var teamId = 1;
            var teamName = NewRandomString();
            var peerSelection = new List<string>() { NewRandomString(), NewRandomString(), NewRandomString() };

            var viewModel = new TeamEditViewModel()
            {
                Id = 1,
                Name = teamName,
                PeerSelection = peerSelection
            };

            // Act
            var result = teamController.Edit(viewModel);

            // Assert
            var view = Assert.IsType<ViewResult>(result);

            var resultViewModel = Assert.IsType<TeamEditViewModel>(view.Model);
            Assert.Equal(teamId, resultViewModel.Id);
            Assert.Equal(teamName, resultViewModel.Name);
            Assert.True(Enumerable.SequenceEqual<string>(peerSelection, resultViewModel.PeerSelection));
        }

        [Fact]
        public void Post_Edit_ModelStateValid()
        {
            // Arrange -----
            var repository = new MockRepository();
            var team = repository.User(UserId).AddTeam(new Team()
            {
                Name = NewRandomString(),
                MemberIds = new List<string>() { NewRandomString() }
            });

            // Define the viewModel containing the updated questions
            var updatedTeamName = NewRandomString();

            var updatedPeerSelection = new List<string>() { NewRandomString(), NewRandomString(), NewRandomString() };

            var viewModel = new TeamEditViewModel()
            {
                Id = team.Id,
                Name = updatedTeamName,
                PeerSelection = updatedPeerSelection
            };

            var teamController = new TeamController(null, repository, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act ------
            var result = teamController.Edit(viewModel);

            // Assert -----
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            // Verify redirection and success message
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("ErrorMessage", teamController.TempData.Keys);
            Assert.Contains("SuccessMessage", teamController.TempData.Keys);
            Assert.Equal($"Successfully updated { updatedTeamName }.", teamController.TempData["SuccessMessage"]);

            // Verify database update
            var updatedTeam = repository.GetTeam(team.Id);
            Assert.Equal(team.Name, updatedTeam.Name);
            Assert.Equal(team.MemberIds, updatedTeam.MemberIds);
            Assert.True(Enumerable.SequenceEqual<string>(team.MemberIds, updatedTeam.MemberIds));
        }

        [Fact]
        public void Post_Delete_GoodId()
        {
            // Arrange
            var repository = new MockRepository();
            var team = repository.User(UserId).AddTeam(new Team()
            {
                Name = NewRandomString(),
                MemberIds = new List<string>() { NewRandomString(), NewRandomString(), NewRandomString() }
            });

            var teamController = new TeamController(null, repository, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = teamController.Delete(team.Id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            // Verify redirection and success message
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("ErrorMessage", teamController.TempData.Keys);
            Assert.Contains("SuccessMessage", teamController.TempData.Keys);
            Assert.Equal($"Successfully deleted - {team.Name}.", teamController.TempData["SuccessMessage"]);

            // Verify team was deleted
            var updatedTeam = repository.GetTeam(team.Id);
            Assert.Null(updatedTeam);
        }

        [Fact]
        public void Post_Delete_BadId()
        {
            // Arrange
            var repository = new MockRepository();

            var team = repository.User(UserId).AddTeam(new Team());

            var teamController = new TeamController(null, repository, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = teamController.Delete(team.Id + 1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            // Verify redirection and success message
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Contains("ErrorMessage", teamController.TempData.Keys);
            Assert.DoesNotContain("SuccessMessage", teamController.TempData.Keys);
            Assert.Equal("Delete failed: peer group not found!", teamController.TempData["ErrorMessage"]);
        }

        [Fact]
        public void Post_Delete_DatabaseException()
        {
            // Arrange

            // Mock the repository to throwing an exception when the AddTeam call is executed
            var repository = new Mock<IRepository>();

            // Mock repository.User()
            repository
                .Setup<IRepository>(n => n.User(It.IsAny<string>()))
                .Returns(repository.Object);

            // Mock repository.DeleteTeam()
            repository
                .Setup<Team>(n => n.DeleteTeam(It.IsAny<int>()))
                .Throws(new Exception("Database error"));

            // Create the controller
            var teamController = new TeamController(null, repository.Object, IdentityHelper)
            {
                TempData = (new MockTempDataDictionary()).Object
            };

            // Act
            var result = teamController.Delete(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain("SuccessMessage", teamController.TempData.Keys);
            Assert.Contains("ErrorMessage", teamController.TempData.Keys);
            Assert.Equal("Delete failed!", teamController.TempData["ErrorMessage"]);
        }
    }
}
