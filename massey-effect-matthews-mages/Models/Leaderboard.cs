using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace massey_effect_matthews_mages.Models
{
    public class Leaderboard
    {
        public IEnumerable<User> Users { get; set; }
        public bool IsGlobal { get; set; }
    }
}