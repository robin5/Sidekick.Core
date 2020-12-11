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

using System.Collections.Generic;
using System.Linq;

namespace Sidekick.Models
{
    public class MockRepository : IRepository
    {
        private static int _nextSurveyId = 1;
        private static int NextSurveyId 
        { 
            get { return _nextSurveyId++; } 
        }

        List<Survey> surveys = new List<Survey>();
        List<TeamNameId> teams = null;
        List<LaunchedSurvey> launchedSurveys = null;

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

        public IEnumerable<TeamNameId> GetTeams()
        {
            teams = new List<TeamNameId>()
            {
                #region Team definitions
                new TeamNameId()
                {
                    Name = "Team 1",
                    Id = 1
                },
                new TeamNameId()
                {
                    Name = "Team 2",
                    Id = 2
                },
                new TeamNameId()
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
