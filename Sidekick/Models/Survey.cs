using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Models
{
    public class Survey
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Questions { get; set; }
    }
}
