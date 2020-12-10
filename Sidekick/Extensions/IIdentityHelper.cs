using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Sidekick
{
    public interface IIdentityHelper
    {
        string GetUserId(ClaimsPrincipal user);
    }
}
