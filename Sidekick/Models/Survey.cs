using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.Models
{
    public class Survey
    {
        public string AspNetId;
        public int Id;
        public string Name;
        public IEnumerable<string> Questions;
    }
}
