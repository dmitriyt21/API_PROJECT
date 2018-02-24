using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErezAPI
{
    [Produces("application/json")]
    [Route("api/agents")]
    public class AgentsController : Controller
    {
        private AgentsActionsContext _repository;

        public AgentsController(AgentsActionsContext repository)
        {
            _repository = repository;
           // _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.EnrollActivity.ToList();

                return Ok(results);
            }
            catch (Exception ex)
            {
               // _logger.LogError($"Failed to get All Trips: {ex}");

                return BadRequest("Error occurred");
            }
        }
    }
}