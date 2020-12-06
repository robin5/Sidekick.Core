using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Models
{
    public interface IRepository
    {
        IEnumerable<Survey> GetSurveys();
        IEnumerable<Team> GetTeams();
        IEnumerable<LaunchedSurvey> GetLaunchedSurveys();
    }
}
