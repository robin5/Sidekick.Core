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
