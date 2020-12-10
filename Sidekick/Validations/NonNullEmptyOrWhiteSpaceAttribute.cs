// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: NonNullEmptyOrWhiteSpaceAttribute.cs
// *
// * Description: Adds non null, empty, or white space validation to ValidationAttribute
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
using System.ComponentModel.DataAnnotations;

namespace Sidekick.Validations
{
    public class NonNullEmptyOrWhiteSpaceAttribute : ValidationAttribute
    {
        private string _errorMessage = null;
        public NonNullEmptyOrWhiteSpaceAttribute(string ErrorMessage = null)
        {
            _errorMessage = ErrorMessage;
        }

        public override bool IsValid(object value)
        {
            string val;
            IEnumerable<string> vals;

            // Validate a string
            if (null != (val = value as string))
            {
                return !string.IsNullOrWhiteSpace(val);
            }
            // Validate an array of strings
            else if (null != (vals = value as IEnumerable<string>))
            {
                foreach (string val2 in vals)
                {
                    if (string.IsNullOrWhiteSpace(val2)) { return false; }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return _errorMessage ?? $"{name} contains a blank entry";
        }
    }
}
