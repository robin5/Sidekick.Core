using Sidekick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sidekick.ViewModels
{
    public class LaunchIndexViewModel
    {
        public Launch Launch;
        public IEnumerable<Team> Teams;
        public IEnumerable<Student> Students;
    }
}
