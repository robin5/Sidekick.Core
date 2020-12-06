using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Models
{
    public class MockRepository : IRepository
    {
        IEnumerable<Survey> surveys = null;
        IEnumerable<Team> teams = null;
        IEnumerable<LaunchedSurvey> launchedSurveys = null;
        public IEnumerable<Survey> GetSurveys()
        {
            surveys = new List<Survey>()
            {
                #region Survey definitions
                new Survey()
                {
                    Name = "F2020 CTEC-126 FALL",
                    Id = 1
                },
                new Survey()
                {
                    Name = "F2021 CTEC-227 WINTER",
                    Id = 2
                },
                new Survey()
                {
                    Name = "F2021 CTEC-290 SPRING",
                    Id = 3
                },
                new Survey()
                {
                    Name = "F2021 CTEC-235 SUMMER",
                    Id = 4
                },
                new Survey()
                {
                    Name = "F2021 CTEC-126 FALL",
                    Id = 5
                },
                new Survey()
                {
                    Name = "F2022 CTEC-227 WINTER",
                    Id = 6
                },
                new Survey()
                {
                    Name = "F2022 CTEC-290 SPRING",
                    Id = 7
                },
                new Survey()
                {
                    Name = "F2022 CTEC-235 SUMMER",
                    Id = 8
                }
                #endregion
            };
            return surveys;
        }
        public IEnumerable<Team> GetTeams()
        {
            teams = new List<Team>()
            {
                #region Team definitions
                new Team()
                {
                    Name = "Team 1",
                    Id = 1
                },
                new Team()
                {
                    Name = "Team 2",
                    Id = 2
                },
                new Team()
                {
                    Name = "Team 3",
                    Id = 3
                }
                #endregion
            };
            return teams;
        }
        public IEnumerable<LaunchedSurvey> GetLaunchedSurveys()
        {
            launchedSurveys = new List<LaunchedSurvey>()
            {
                #region Launched survey definitions
                new LaunchedSurvey()
                {
                    Id = 1,
                    Name = "Launched Survey 1",
                    Start = "2020/12/03",
                    End = "2020/12/04",
                    Status = "Unknown"
                },
                new LaunchedSurvey()
                {
                    Id = 2,
                    Name = "Launched Survey 2",
                    Start = "2020/12/05",
                    End = "2020/12/06",
                    Status = "Unknown"
                },
                #endregion
            };
            return launchedSurveys;
        }
    }
}
