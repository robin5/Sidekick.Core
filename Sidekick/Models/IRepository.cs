// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: IRepository.cs
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
    public interface IRepository
    {
        public IRepository User(string userId);
        public string UserId { get; set; }

        #region Surveys
        Survey AddSurvey(Survey survey);
        Survey GetSurvey(int id);
        Survey UpdateSurvey(Survey survey);
        Survey DeleteSurvey(int id);
        #endregion

        #region Teams
        Team AddTeam(Team team);
        Team GetTeam(int id);
        IEnumerable<TeamStudent> GetTeamStudents(int teamId);
        Team UpdateTeam(Team team);
        Team DeleteTeam(int id);
        IEnumerable<Team> GetTeams(IEnumerable<int> teamIds);
        #endregion

        #region Launch
        Launch AddLaunch(Launch launch);
        Launch GetLaunch(int id);
        Launch UpdateLaunch(Launch launch);
        Launch DeleteLaunch(int id);
        IEnumerable<TeamTeamMember> GetLaunchTeams(int launchId);
        #endregion

        #region Student
        Student GetStudent(string studentId);
        IEnumerable<Student> GetStudents(IEnumerable<string> studentIds);
        IEnumerable<Student> GetTeamStudents(IEnumerable<int> teamIds);
        #endregion

        IEnumerable<SurveyNameId> GetAllSurveyNameIds();
        IEnumerable<TeamNameId> GetAllTeamNameIds();
        IEnumerable<Launch> GetAllLaunches();
        IEnumerable<Student> GetAllStudents();
    }
}
