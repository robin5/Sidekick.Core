﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sidekick.Test
{
    public class ControllerTest
    {
        public string UserId { get; }  = "TestUser";
        public IdentityHelper IdentityHelper { get; }
        public ControllerTest()
        {
            var identityHelper = new Mock<IdentityHelper>();

            identityHelper
                .Setup<string>(n => n.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(UserId);

            IdentityHelper = identityHelper.Object;
        }
    }
}
