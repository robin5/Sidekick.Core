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

namespace Sidekick.Controllers
{
    [Authorize(Policy = "SurveyOwner")]
    public class SurveyController : Controller
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IRepository repository;
        private string userId = null;

        public SurveyController(
            ILogger<DashboardController> logger,
            IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
            userId = "robin";
        }

        // GET: Displays a survey with the "Index" view
        public IActionResult Index(int id)
        {
            var survey = repository.GetSurvey(userId, id);
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
        public ActionResult Create()
        {
            return View(new SurveyCreateViewModel());
        }

        // POST: Create a Survey with the "Create" view
        [HttpPost]
        public ActionResult Create(SurveyCreateViewModel model)
        {
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
    }
}
