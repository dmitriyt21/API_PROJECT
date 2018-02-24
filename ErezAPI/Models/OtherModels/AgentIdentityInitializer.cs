using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ErezAPI
{
  public class AgentIdentityInitializer
  {
    private RoleManager<IdentityRole> _roleMgr;
    private UserManager<ActivityUser> _userMgr;

    public AgentIdentityInitializer(UserManager<ActivityUser> userMgr, RoleManager<IdentityRole> roleMgr)
    {
      _userMgr = userMgr;
      _roleMgr = roleMgr;
    }

    public async Task Seed()
    {
      var user = await _userMgr.FindByNameAsync("itai@admin.com");

      // Add User
      if (user == null)
      {

        user = new ActivityUser()
        {
          UserName = "***@admin.com",
          Email = "***@admin.com",
          AgentId = 100,
          AgentName = "***"
        };
      
        var userResult = await _userMgr.CreateAsync(user, "***");
        //var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
        var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

      }
    }
  }
}
