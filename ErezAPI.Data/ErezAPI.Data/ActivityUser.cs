using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{
    public class ActivityUser : IdentityUser
    {
        public int AgentId { get; set; }

        public string AgentName { get; set; }
    }
}
