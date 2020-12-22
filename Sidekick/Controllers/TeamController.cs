// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: TeamController.cs
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
    [Authorize(Policy = "TeamOwner")]
    public class TeamController : Controller
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IRepository repository;
        private readonly IIdentityHelper identityHelper;

        public TeamController(
            ILogger<DashboardController> logger,
            IRepository repository,
            IIdentityHelper identityHelper)
        {
            this.logger = logger;
            this.repository = repository;
            this.identityHelper = identityHelper;
        }

        // Displays team details
        [HttpGet]
        public IActionResult Index(int id)
        {
            var teamStudents = repository.User(identityHelper.GetUserId(User)).GetTeamStudents(id);
            if (teamStudents.Count() == 0)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var viewModel = new TeamIndexViewModel()
            {
                TeamStudents = teamStudents
            };

            return View(viewModel);
        }

        // Displays the view for creating a team
        [HttpGet]
        public IActionResult Create()
        {
            var students = repository.User(identityHelper.GetUserId(User)).GetAllStudents();

            var viewModel = new TeamCreateViewModel()
            {
                Students = students
            };
            return View(viewModel);
        }

        // Creates a team
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Students = repository.User(identityHelper.GetUserId(User)).GetAllStudents();
                    return View(model);
                }

                var team = new Team()
                {
                    Name = model.Name,
                    MemberIds = model.PeerSelection
                };

                repository.User(identityHelper.GetUserId(User)).AddTeam(team);

                TempData.SetSuccessMessage($"Successfully added {model.Name} to peer groups.");
            }
            catch (Exception ex)
            {
                TempData.SetErrorMessage($"Failed adding {model.Name} to peer groups: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        // Displays the view for editing a team
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var students = repository.User(identityHelper.GetUserId(User)).GetAllStudents();
            
            var team = repository.User(identityHelper.GetUserId(User)).GetTeam(id);
            if (null == team)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var viewModel = new TeamEditViewModel()
            {
                Id = id,
                Students = students,
                Name = team.Name,
                PeerSelection = team.MemberIds,
            };
            return View(viewModel);
        }

        // Updates a team
        [HttpPost]
        public IActionResult Edit(TeamEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Students = repository.User(identityHelper.GetUserId(User)).GetAllStudents();
                    return View(model);
                }

                Team team = new Team()
                {
                    Id = model.Id,
                    Name = model.Name,
                    MemberIds = model.PeerSelection
                };

                repository.User(identityHelper.GetUserId(User)).UpdateTeam(team);

                TempData.SetSuccessMessage($"Successfully updated {model.Name}.");
            }
            catch (Exception ex)
            {
                TempData.SetErrorMessage($"Failed updating {model.Name}: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        // Deletes a team
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Team team = repository.User(identityHelper.GetUserId(User)).DeleteTeam(id);
                if (null != team)
                {
                    TempData.SetSuccessMessage($"Successfully deleted - {team.Name}.");
                }
                else
                {
                    TempData.SetErrorMessage("Delete failed: peer group not found!");
                }
            }
            catch (Exception ex)
            {
                TempData.SetErrorMessage("Delete failed!");
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
