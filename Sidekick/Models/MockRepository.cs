// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: MockRepository.cs
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
using System.Diagnostics;
using System.Linq;

namespace Sidekick.Models
{
    public class MockRepository : IRepository
    {
        private int _nextSurveyId = 1;
        private int NextSurveyId
        {
            get { return _nextSurveyId++; }
        }

        private int _nextTeamId = 1;
        private int NextTeamId
        {
            get { return _nextTeamId++; }
        }

        List<Survey> surveys = new List<Survey>();
        List<Team> teams = new List<Team>();
        List<LaunchedSurvey> launchedSurveys = null;
        List<Student> students = new List<Student>();

        public MockRepository()
        {
            students = new List<Student>()
            {
                #region Mock Students
                new Student()
                {
                    Name = "Amy Jaeger",
                    Email = "amy.ajaeger@robin5.net",
                    UserId = "f4903b8c-e567-4223-81dc-b1c91f892a06",
                    UserName = "ajaeger",
                },
                new Student()
                {
                    Name = "Andrey Demchenko",
                    Email = "andrey.demchenko@robin5.net",
                    UserId = "d0aaf0ee-3da6-4f34-a79c-62714f4e0025",
                    UserName = "ademchenko",
                },
                new Student()
                {
                    Name = "Bilal Sejouk",
                    Email = "Greg@robin5.net",
                    UserId = "823eb7ff-610f-4a9f-abdb-d4b33c490666",
                    UserName = "bsejouk",
                },
                new Student()
                {
                    Name = "Chris McGuire",
                    Email = "chris.mcguire@robin5.net",
                    UserId = "85df04fe-0018-43f9-8961-a6d2ff212445",
                    UserName = "cmcguire",
                },
                new Student()
                {
                    Name = "Dave King",
                    Email = "dave.king@robin5.net",
                    UserId = "5ec4e2989-1e41-49db-a8f5-e907e1908a16",
                    UserName = "dking",
                },
                new Student()
                {
                    Name = "David Richards",
                    Email = "david.richards@robin5.net",
                    UserId = "7ec47fe8-4167-4cb0-8686-5949efbd10ac",
                    UserName = "drichards",
                },
                new Student()
                {
                    Name = "Jacob Ruff",
                    Email = "jacob.ruff@robin5.net",
                    UserId = "68399755-dc08-4fc1-8439-8eb5a7adfa74",
                    UserName = "jruff",
                },
                new Student()
                {
                    Name = "Matt Lehr",
                    Email = "matt.lehr@robin5.net",
                    UserId = "2f037f6c-ac67-482c-8e99-9f2741e37ba9",
                    UserName = "mlehr",
                },
                new Student()
                {
                    Name = "Patrick McCulley",
                    Email = "patrick.mcculley@robin5.net",
                    UserId = "94fa3020-8156-4a3a-bec7-b6bfaabce740",
                    UserName = "pmcculley",
                },
                new Student()
                {
                    Name = "Richard Lint",
                    Email = "richard.lint@robin5.net",
                    UserId = "b44d7f04-7399-4b24-9602-ae071180ac9e",
                    UserName = "rlint",
                },
                new Student()
                {
                    Name = "Robin Murray",
                    Email = "robin.murray@robin5.net",
                    UserId = "7bb9846e-2874-4a44-b23e-10841b6abf80",
                    UserName = "rmurray",
                },
                new Student()
                {
                    Name = "Wayne Woods",
                    Email = "wayne.woods@robin5.net",
                    UserId = "a17ecf50-7e63-4b17-b4ec-0e12f28c749b",
                    UserName = "wwoods",
                },
                new Student()
                {
                    Name = "Yevgen Shapovalov",
                    Email = "yevgen.shapovalov@robin5.net",
                    UserId = "62157336-e8de-43e5-af70-83fdaa2b261f",
                    UserName = "yshapovalov",
                },
            #endregion
            };
        }

        public string UserId { get; set; }

        public IRepository User(string userId)
        {
            UserId = userId;
            return this;
        }

        #region Survey
        public virtual Survey AddSurvey(Survey survey) 
        {
            survey.UserId = UserId;
            survey.Id = NextSurveyId;
            surveys.Add(survey);
            return survey;
        }
        public Survey DeleteSurvey(int id)
        {
            Survey survey = surveys.FirstOrDefault(e => ((e.Id == id) && (e.UserId == UserId)));
            if (null != survey)
            {
                surveys.Remove(survey);
            }
            return survey;
        }
        public Survey GetSurvey(int id)
        {
            return surveys.FirstOrDefault(e => ((e.Id == id) && (e.UserId == UserId)));
        }
        public Survey UpdateSurvey(Survey updatedSurvey)
        {
            Survey survey = surveys.FirstOrDefault(e => ((e.Id == updatedSurvey.Id) && (e.UserId == UserId)));
            if (null != survey)
            {
                survey.UserId = UserId;
                survey.Id = updatedSurvey.Id;
                survey.Name = updatedSurvey.Name;
                survey.Questions = updatedSurvey.Questions;
            }
            return survey;
        }
        #endregion

        #region Team
        public virtual Team AddTeam(Team team)
        {
            team.UserId = UserId;
            team.Id = NextTeamId;
            teams.Add(team);
            return team;
        }
        public Team DeleteTeam(int id)
        {
            Team team = teams.FirstOrDefault(e => ((e.Id == id) && (e.UserId == UserId)));
            if (null != team)
            {
                teams.Remove(team);
            }
            return team;
        }
        public Team GetTeam(int id)
        {
            return teams.FirstOrDefault(e => ((e.Id == id) && (e.UserId == UserId)));
        }
        public Team UpdateTeam(Team updatedTeam)
        {
            Team team = teams.FirstOrDefault(e => ((e.Id == updatedTeam.Id) && (e.UserId == UserId)));
            if (null != team)
            {
                team.UserId = UserId;
                team.Id = updatedTeam.Id;
                team.Name = updatedTeam.Name;
                team.MemberIds = updatedTeam.MemberIds;
            }
            return team;
        }
        public IEnumerable<TeamStudent> GetTeamStudents(int teamId)
        {
            var teamStudents = new List<TeamStudent>();
            try
            {
                var team = GetTeam(teamId);
                if (null != team)
                {
                    var students = this.students.Where(e => team.MemberIds.Contains(e.UserId));
                    if (null != students)
                    {
                        foreach (var student in students)
                        {
                            teamStudents.Add(new TeamStudent()
                            {
                                Team = team,
                                Student = student
                            });
                        }
                    }
                }
            }
            catch
            {

            }

            return teamStudents;
        }
        #endregion

        public IEnumerable<SurveyNameId> GetAllSurveyNameIds()
        {
            var surveyNameIds = new List<SurveyNameId>();

            var result = surveys.FindAll(e => e.UserId == UserId);

            foreach (var surveyNameId in result)
            {
                surveyNameIds.Add(new SurveyNameId()
                {
                    Name = surveyNameId.Name,
                    Id = surveyNameId.Id
                });
            }
            return surveyNameIds;
        }

        public IEnumerable<TeamNameId> GetAllTeamNameIds()
        {
            var teamNameIds = new List<TeamNameId>();

            var result = teams.FindAll(e => e.UserId == UserId);

            foreach (var teamNameId in result)
            {
                teamNameIds.Add(new TeamNameId()
                {
                    Name = teamNameId.Name,
                    Id = teamNameId.Id
                });
            }
            return teamNameIds;
        }

        public IEnumerable<LaunchedSurvey> GetAllLaunchedSurveys()
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

        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }
    }
}
