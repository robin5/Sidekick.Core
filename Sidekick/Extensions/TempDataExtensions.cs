// **********************************************************************************
// * Copyright (c) 2020 Robin Murray
// **********************************************************************************
// *
// * File: TempDataExtensions.cs
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

using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Sidekick
{
    public static class TempDataExtensions
    {
        const string KEY_SUCCESS_MESSAGE = "SuccesMessage";
        const string KEY_ERROR_MESSAGE = "ErrorMessage";

        /// <summary>
        /// Sets the success message in TempData
        /// </summary>
        /// <param name="tempData"></param>
        /// <param name="message"></param>
        public static void SuccessMessage(this TempDataDictionary tempData, string message)
        {
            tempData[KEY_SUCCESS_MESSAGE] = message;
        }

        /// <summary>
        /// Retrieves the success message from TempData
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static string SuccessMessage(this TempDataDictionary tempData)
        {
            return tempData[KEY_SUCCESS_MESSAGE] as string;
        }

        /// <summary>
        /// Sets the error message in TempData
        /// </summary>
        /// <param name="tempData"></param>
        /// <param name="message"></param>
        public static void ErrorMessage(this TempDataDictionary tempData, string message)
        {
            tempData[KEY_ERROR_MESSAGE] = message;
        }

        /// <summary>
        /// Retrieves the error message from TempData
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        public static string ErrorMessage(this TempDataDictionary tempData)
        {
            return tempData[KEY_ERROR_MESSAGE] as string;
        }
    }
}