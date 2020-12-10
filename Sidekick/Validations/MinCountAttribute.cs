// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: MinCountAttribute.cs
// *
// * Description: Adds minimum array size validation to ValidationAttribute
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

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sidekick.Validations
{
    public class MinCountAttribute : ValidationAttribute
    {
        private string _errorMessage = null;
        private int _minCount = int.MinValue;
        public MinCountAttribute(int minCount, string ErrorMessage = null)
        {
            _minCount = minCount;
            _errorMessage = ErrorMessage ?? base.ErrorMessage;
        }
        public override bool IsValid(object value)
        {
            IEnumerable<int> iVals = value as IEnumerable<int>;
            IEnumerable<string> sVals = value as IEnumerable<string>;
            IEnumerable<object> oVals = value as IEnumerable<object>;
            IEnumerable<SelectListItem> sliVals = value as IEnumerable<SelectListItem>;

            // Validate an array of strings
            if ((null != iVals) && (iVals.Count() >= _minCount)) { return true; }
            else if ((null != sVals) && (sVals.Count() >= _minCount)) { return true; }
            else if ((null != oVals) && (oVals.Count() >= _minCount)) { return true; }
            else if ((null != sliVals) && (sliVals.Count() >= _minCount)) { return true; }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return _errorMessage ?? $"{name} is empty";
        }
    }
}