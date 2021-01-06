using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
    }
}
