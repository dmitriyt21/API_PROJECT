using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErezAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ErezAPI.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/privateMessages")]
    
    public class PrivateMessagesController : Controller
    {
        private readonly AgentsActionsContext _context;
        private readonly UserManager<ActivityUser> _userManager;

        public PrivateMessagesController(AgentsActionsContext context, UserManager<ActivityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/PrivateMessages
        [HttpGet]
        [Authorize(Policy = "Managers")]
        public IEnumerable<PrivateMessage> GetPrivateMessages()
        {
            return _context.PrivateMessages;
        }

        //*****************************//
        //GET PRIVATE MESSAGES LIST
        //*****************************//
        [HttpGet("{AgentId}")]
        [Authorize]
        public IActionResult GetPrivateMessages([FromRoute] int AgentId)
        {
            var Userid = _userManager.GetUserId(User);
            
            var user = _userManager.Users.Where(u => u.AgentId == AgentId).Select( a => a.Email).SingleOrDefault();

            if (!(Userid == user))
            {
                return Unauthorized();
            }

                 var privateMessageList = _context.PrivateMessages.Where(m => m.AgentId == AgentId).OrderByDescending(m => m.Date).Take(5).ToList();
                if (privateMessageList == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(privateMessageList);
                }

            
            
        }

        [HttpGet("{AgentId}/{MessageId}")]
        [Authorize]
        public IActionResult GetPrivateMessage([FromRoute] int MessageId, int AgentId)
        {
            var Userid = _userManager.GetUserId(User);

            var user = _userManager.Users.Where(u => u.AgentId == AgentId).Select(a => a.Email).SingleOrDefault();

            if (!(Userid == user))
            {
                return Unauthorized();
            }

            var privateMessage = _context.PrivateMessages.Where(m => m.MessageId == MessageId).FirstOrDefault();

            if (privateMessage == null)
            {
                return BadRequest();
            }
            else
            {
                Seen(privateMessage);
                return Ok(privateMessage);
            }
        }
        private void Seen (PrivateMessage privateMessage)
        {
            if (!privateMessage.IsSeen)
            {
                privateMessage.IsSeen = true;

                _context.Entry(privateMessage).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }


        //POST: api/PostPrivateMessage
        [HttpPost]
        [Authorize(Policy = "Managers")]
        public async Task<IActionResult> PostPrivateMessage([FromBody] PrivateMessage privateMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!PrivateMessageExists(privateMessage.AgentId, privateMessage.Date))
            {
                var Userid = _userManager.GetUserId(User);
                var user = await _userManager.FindByNameAsync(Userid);

                privateMessage.Issuer = user.AgentName;

                _context.PrivateMessages.Add(privateMessage);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPrivateMessage", new { MessageId = privateMessage.MessageId }, privateMessage);
            }
            else
            {
                return BadRequest("The message to this Agent & Date already exists");
            }
        }

        //DELETE: api/PrivateMessages/5
        [Authorize]
        [HttpDelete("{AgentId}/{MessageId}")]
        public async Task<IActionResult> DeletePrivateMessage([FromRoute] int MessageId, int AgentId)
        {
            var Userid = _userManager.GetUserId(User);

            var user = _userManager.Users.Where(u => u.AgentId == AgentId).Select(a => a.Email).SingleOrDefault();

            if (!(Userid == user))
            {
                return Unauthorized();
            }

            var privateMessage = await _context.PrivateMessages.SingleOrDefaultAsync(m => m.MessageId == MessageId);
            if (privateMessage == null)
            {
                return NotFound();
            }

            _context.PrivateMessages.Remove(privateMessage);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PrivateMessageExists(int id, DateTime date)
        {
            return _context.PrivateMessages.Any(e => e.AgentId == id && e.Date == date);
        }
    }
}