using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Models
{
    public class SurveyCreate
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Questions { get; set; }
    }
}
