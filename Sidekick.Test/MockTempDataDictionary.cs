using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidekick.Test
{
    public class MockTempDataDictionary
    {
        public MockTempDataDictionary()
        {
            // Create mocked TempData
            var httpContext = new DefaultHttpContext();
            Object = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        }
        public TempDataDictionary Object { get; }
    }
}
