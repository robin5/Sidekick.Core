using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Models
{
    public class Team
    {
        public string UserId;
        public int Id;
        public string Name;
        public IEnumerable<string> MemberIds;
    }
}
