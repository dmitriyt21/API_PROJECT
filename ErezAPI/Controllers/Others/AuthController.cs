using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ErezAPI
{
    public class AuthController : Controller
    {
        private UserManager<ActivityUser> _userManager;
        private SignInManager<ActivityUser> _signInMgr;
        private IPasswordHasher<ActivityUser> _hasher;
        private IConfigurationRoot _config;
        private ILogger<AuthController> _logger;

        public AuthController(UserManager<ActivityUser> userManager, SignInManager<ActivityUser> signInMgr, IConfigurationRoot config, IPasswordHasher<ActivityUser> hasher, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInMgr = signInMgr;
            _hasher = hasher;
            _config = config;
            _logger = logger;
        }

        // POST api/Account/Register
        
        [Route("api/auth/register")]
        [Authorize(Policy = "SuperUsers")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel model, [FromQuery] bool? Manager=false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ActivityUser() { UserName = model.Email, Email = model.Email, AgentId=model.AgentId, AgentName=model.AgentName };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (Manager == true)
            {
                var claimResult = await _userManager.AddClaimAsync(user, new Claim("Manager", "True"));
            }
            
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("api/auth/checkToken")]
        public async Task<IActionResult> CheckToken()
        {
            var Userid = _userManager.GetUserId(User);
            var user = await _userManager.FindByNameAsync(Userid);

            if (!(user == null))
            {
                    var userClaims = await _userManager.GetClaimsAsync(user);
                if (!(userClaims.SingleOrDefault()?.Type == null))
                {
                    return Ok(userClaims.SingleOrDefault()?.Type.ToLower());
                }
                    else
                {
                    return Ok("regular");
                }
            }
            return Unauthorized();
        }


        [AllowAnonymous]
        [HttpPost("api/auth/token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _userManager.GetClaimsAsync(user);

                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())              
                        }.Union(userClaims);

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ErezDmitriyAPIProjectFullPath"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          issuer: "http://localhost:8000",
                          audience: "http://localhost:8000",
                          claims: claims,
                          expires: DateTime.UtcNow.AddMonths(6),
                          signingCredentials: creds
                          );

                        return Ok(
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo,
                                agentId = user.AgentId,
                                agentName = user.AgentName,
                                role = token.Claims.Where(m => m.Type == "Manager" || m.Type == "SuperUser").SingleOrDefault()?.Type.ToLower()
                            }
                            );
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while creating JWT: {ex}");
            }

            return BadRequest("Failed to generate token");
        }
    }
}