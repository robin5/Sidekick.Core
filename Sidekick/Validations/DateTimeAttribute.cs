// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: DateTimeAttribute.cs
// *
// * Description: Attribute for validating DateTime parameters
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
using System.ComponentModel.DataAnnotations;

namespace Sidekick.Validations
{
    public class DateTimeAttribute : ValidationAttribute
    {
        private string _errorMessage = null;
        public DateTimeAttribute(string errorMessage = null)
        {
            _errorMessage = errorMessage ?? base.ErrorMessage;
        }
        public override bool IsValid(object value)
        {
            string sDateTime;
            DateTime dateTime;

            if (null == (sDateTime = value as string))
            {
                return false;
            }

            if (DateTime.TryParse(sDateTime, out dateTime))
            {
                return true;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return _errorMessage ?? $"{name} is invalid";
        }
    }
}
