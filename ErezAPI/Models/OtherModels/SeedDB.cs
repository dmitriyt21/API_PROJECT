using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{
    public class SeedDB
    {
        private AgentsActionsContext _context;
        private UserManager<ActivityUser> _userManager;

        public SeedDB(AgentsActionsContext context, UserManager<ActivityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
    }
}

