// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: DashboardController.cs
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sidekick.Models;
using System.Linq;
using System.Security.Claims;

namespace Sidekick.Controllers
{
    [Authorize(Policy = "SurveyOwner")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IRepository repository;
        private readonly IIdentityHelper identityHelper;

        public DashboardController(
            ILogger<DashboardController> logger,
            IRepository repository,
            IIdentityHelper identityHelper)
        {
            this.logger = logger;
            this.repository = repository;
            this.identityHelper = identityHelper;
        }

        public IActionResult Index()
        {
            repository.UserId = identityHelper.GetUserId(User);

            var model = new DashboardViewModel
            {
                Surveys = repository.GetAllSurveyNameIds(),
                Teams = repository.GetTeams(),
                LaunchedSurveys = repository.GetLaunchedSurveys()
            };
            return View(model);
        }
    }
}
