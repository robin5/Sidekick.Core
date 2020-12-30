// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: TeamCreateViewModel.cs
// *
// * Description: View model for the TeamController for creating a team
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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sidekick.Models;
using Sidekick.Validations;


namespace Sidekick.ViewModels
{
    public class LaunchCreateViewModel
    {
        public IEnumerable<SurveyNameId> Surveys { get; set; }
        public IEnumerable<TeamNameId> Teams { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage = "Please enter a Launch Name.")]
        public string LaunchName { get; set; }

        [NoneSelected("Survey not selected")]
        public int Survey { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage = "Please enter a start date and time.")]
        [DateTime]
        [Display(Name = "Start date and time")]
        public string StartDateTime { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage = "Please enter an end date and time.")]
        [DateTime]
        [Display(Name = "End date and time")]
        public string EndDateTime { get; set; }

        [MinCount(1, ErrorMessage = "Please add one or more Peer Groups to send.")]
        public IEnumerable<int> SelectedTeams { get; set; }
    }
}
