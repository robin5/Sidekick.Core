using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sidekick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IRepository repository;

        public DashboardController(
            ILogger<DashboardController> logger,
            IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                Surveys = repository.GetSurveys(),
                Teams = repository.GetTeams(),
                LaunchedSurveys = repository.GetLaunchedSurveys()
            };
            return View(model);
        }
    }
}
