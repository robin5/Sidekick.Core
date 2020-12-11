// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: SurveyController.cs
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
using Microsoft.AspNetCore.Identity;

namespace Sidekick.Controllers
{
    [Authorize(Policy = "SurveyOwner")]
    public class SurveyController : Controller
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IRepository repository;
        private readonly IIdentityHelper identityHelper;

        public SurveyController(
            ILogger<DashboardController> logger,
            IRepository repository,
            IIdentityHelper identityHelper)
        {
            this.logger = logger;
            this.repository = repository;
            this.identityHelper = identityHelper;
        }

        // GET: Displays a survey with the "Index" view
        public IActionResult Index(int id)
        {
            var survey = repository.User(identityHelper.GetUserId(User)).GetSurvey(id);
            if (null == survey)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var viewModel = new SurveyIndexViewModel()
            {
                Id = survey.Id,
                Name = survey.Name,
                Questions = survey.Questions
            };

            return View(viewModel);
        }

        // GET: Create a Survey with the "Create" view
        public IActionResult Create()
        {
            return View(new SurveyCreateViewModel());
        }

        // POST: Create a Survey with the "Create" view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SurveyCreateViewModel model)
        {
            var userId = identityHelper.GetUserId(User);

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var survey = new Survey()
                {
                    UserId = userId,
                    Name = model.Name,
                    Questions = model.Questions
                };

                repository.AddSurvey(survey);

                TempData.SetSuccessMessage($"Successfully added {model.Name} to peer surveys.");
            }
            catch (Exception ex)
            {
                TempData.SetErrorMessage($"Failed adding {model.Name} to peer surveys: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Displays a survey with the "Index" view
        public IActionResult Edit(int id)
        {
            var survey = repository.User(identityHelper.GetUserId(User)).GetSurvey(id);
            if (null == survey)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var viewModel = new SurveyEditViewModel()
            {
                Id = survey.Id,
                Name = survey.Name,
                Questions = survey.Questions
            };

            return View(viewModel);
        }
        // POST: Edit a Survey with the "Edit" view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SurveyEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Survey survey = new Survey()
                {
                    UserId = identityHelper.GetUserId(User),
                    Id = model.Id,
                    Name = model.Name,
                    Questions = model.Questions
                };

                repository.UpdateSurvey(survey);

                TempData.SetSuccessMessage($"Successfully updated {model.Name}.");
            }
            catch (Exception ex)
            {
                TempData.SetErrorMessage($"Failed updating {model.Name}: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Survey survey = repository.User(identityHelper.GetUserId(User)).DeleteSurvey(id);
                if (null != survey)
                {
                    TempData.SetSuccessMessage($"Successfully deleted - {survey.Name}.");
                }
                else
                {
                    TempData.SetErrorMessage("Delete failed: survey not found!");
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
