// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: LaunchController.cs
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sidekick.ViewModels;
using Sidekick.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Sidekick.Controllers
{
    [Authorize(Policy = "SurveyOwner")]
    public class LaunchController : Controller
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IRepository repository;
        private readonly IIdentityHelper identityHelper;

        public LaunchController(
            ILogger<DashboardController> logger,
            IRepository repository,
            IIdentityHelper identityHelper)
        {
            this.logger = logger;
            this.repository = repository;
            this.identityHelper = identityHelper;
        }

        // GET: Launch
        public IActionResult Create()
        {
            var surveys = repository.User(identityHelper.GetUserId(User)).GetAllSurveyNameIds();

            var teams = repository.User(identityHelper.GetUserId(User)).GetAllTeamNameIds();

            var viewModel = new LaunchCreateViewModel()
            {
                Surveys = surveys,
                Teams = teams
            };
            return View(viewModel);
        }

        // POST: Launch
        [HttpPost]
        public IActionResult Create(LaunchCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Surveys = repository.User(identityHelper.GetUserId(User)).GetAllSurveyNameIds();
                    viewModel.Teams = repository.User(identityHelper.GetUserId(User)).GetAllTeamNameIds();
                    return View(viewModel);
                }

                var launch = new Launch()
                {
                    Name = viewModel.LaunchName,
                    SurveyId = viewModel.Survey,
                    Start = viewModel.StartDateTime,
                    End = viewModel.EndDateTime,
                    Teams = viewModel.SelectedTeams
                };

                repository.User(identityHelper.GetUserId(User)).AddLaunch(launch);

                TempData.SetSuccessMessage("Successfully launched surveys");
            }
            catch (Exception ex)
            {
                TempData.SetErrorMessage($"Failed launching {viewModel.LaunchName}: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        // GET: LaunchedSurveySummary
        public ActionResult Index(int id)
        {
            var launch = repository.User(identityHelper.GetUserId(User)).GetLaunch(id);

            var viewModel = new LaunchIndexViewModel()
            {
                Launch = launch,
                Teams = repository.GetTeams(launch.Teams),
                Students = repository.GetTeamStudents(launch.Teams)
            };

            return View(viewModel);
        }

        // GET: CommentsAbout
        [Route("Launch/CommentsAbout/{launchId}/{teamId}/{userId}")]
        public IActionResult CommentsAbout(int launchId, int teamId, string userId)
        {
            repository.UserId = identityHelper.GetUserId(User);

            var Launch = repository.GetLaunch(launchId);
            var survey = repository.GetSurvey(Launch.SurveyId);
            //var answers = repository.GetCommentsAbout(launchId, teamId, userId);

            var viewModel = new CommentsAboutViewModel()
            {
                Launch = Launch,
                Team = repository.GetTeam(teamId),
                Student = repository.GetStudent(userId),
                Survey = repository.GetSurvey(Launch.SurveyId)
            };

            return View(viewModel);
        }

        // GET: CommentsBy
        [Route("Launch/CommentsBy/{launchId}/{teamId}/{userId}")]
        public IActionResult CommentsBy(int launchId, int teamId, string userId)
        {
            repository.UserId = identityHelper.GetUserId(User);

            var viewModel = new CommentsByViewModel()
            {
                Launch = repository.GetLaunch(launchId),
                Team = repository.GetTeam(teamId),
                Student = repository.GetStudent(userId)
            };

            return View(viewModel);
        }
    }
}
