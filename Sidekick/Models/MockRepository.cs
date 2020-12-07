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

        List<SurveyNameId> surveyNameIds = new List<SurveyNameId>()
            {
                #region Survey definitions
                new SurveyNameId()
                {
                    Name = "F2020 CTEC-126 FALL",
                    Id = 1
                },
                new SurveyNameId()
                {
                    Name = "F2021 CTEC-227 WINTER",
                    Id = 2
                },
                new SurveyNameId()
                {
                    Name = "F2021 CTEC-290 SPRING",
                    Id = 3
                },
                new SurveyNameId()
                {
                    Name = "F2021 CTEC-235 SUMMER",
                    Id = 4
                },
                new SurveyNameId()
                {
                    Name = "F2021 CTEC-126 FALL",
                    Id = 5
                },
                new SurveyNameId()
                {
                    Name = "F2022 CTEC-227 WINTER",
                    Id = 6
                },
                new SurveyNameId()
                {
                    Name = "F2022 CTEC-290 SPRING",
                    Id = 7
                },
                new SurveyNameId()
                {
                    Name = "F2022 CTEC-235 SUMMER",
                    Id = 8
                }
                #endregion
            };
        IEnumerable<TeamNameId> teams = null;
        IEnumerable<LaunchedSurvey> launchedSurveys = null;
        List<Survey> surveys = new List<Survey>() 
        {
                new Survey()
                {
                    AspNetId = "robin",
                    Id = 1,
                    Name = "F2022 CTEC-235 SUMMER",
                    Questions = new List<string>()
                    {
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce gravida libero pretium condimentum dignissim. Vestibulum eu elementum enim. Sed congue sapien ligula, nec posuere urna commodo quis. Suspendisse potenti. Aliquam in facilisis purus. Mauris rhoncus eget lectus sed luctus. Nulla non eros sem.",
                        "Integer ullamcorper condimentum hendrerit. Quisque urna nibh, dignissim in ex pulvinar, eleifend fermentum ante. Nulla consectetur dolor et ligula hendrerit, ut elementum erat elementum. Praesent ornare vulputate ipsum, ut fringilla purus suscipit sed. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. ",
                        "Donec varius varius suscipit. Phasellus egestas ex accumsan, blandit tellus id, efficitur tellus. Curabitur vehicula neque vel elit venenatis faucibus. Aliquam id justo malesuada, fringilla dolor ut, facilisis massa. Nam bibendum sed nunc venenatis facilisis. Aenean neque mauris, suscipit non varius ut, egestas a nisl. Quisque porta consectetur tellus, vitae commodo augue semper in. Nunc arcu ante, varius id augue ullamcorper, consectetur suscipit enim. Ut dignissim elementum metus a eleifend. Pellentesque sed velit felis.",
                    }
                }
        };

        #region Survey
        public Survey AddSurvey(string AspNetId, Survey survey) 
        {
            survey.AspNetId = AspNetId;
            survey.Id = NextSurveyId;
            surveys.Add(survey);
            return survey;
        }
        public Survey DeleteSurvey(string AspNetId, int id)
        {
            Survey survey = surveys.FirstOrDefault(e => ((e.Id == id) && (e.AspNetId == AspNetId)));
            if (null != survey)
            {
                surveys.Remove(survey);
            }
            return survey;
        }
        public Survey GetSurvey(string AspNetId, int id)
        {
            return surveys.FirstOrDefault(e => ((e.Id == id) && (e.AspNetId == AspNetId)));
        }
        public Survey UpdateSurvey(string AspNetId, Survey updatedSurvey)
        {
            Survey survey = surveys.FirstOrDefault(e => ((e.Id == updatedSurvey.Id) && (e.AspNetId == AspNetId)));
            if (null != survey)
            {
                survey.AspNetId = AspNetId;
                survey.Id = updatedSurvey.Id;
                survey.Name = updatedSurvey.Name;
                survey.Questions = updatedSurvey.Questions;
            }
            return survey;
        }
        #endregion

        public IEnumerable<SurveyNameId> GetAllSurveyNameIds(string AspNetId)
        {
            surveyNameIds = new List<SurveyNameId>();

            var result = surveys.FindAll(e => e.AspNetId == AspNetId);

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
