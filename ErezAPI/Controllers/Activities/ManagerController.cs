using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ErezAPI
{
    
    [Produces("application/json")]
    [Route("api/Manager")]
    public class ManagerController : Controller
    {
        private AgentsActionsContext _repository;
        private IRepository _repo;
        private IMapper _mapper;

        public ManagerController(AgentsActionsContext repository, IRepository repo, IMapper mapper)
        {
            _repository = repository;
            _repo = repo;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetManagerData/{ActNum}/{Month}/{WeekNum}")]
        public IActionResult GetManagerData(int? ActNum, int? Month, int? WeekNum)
        {

                return Ok(_mapper.Map<IEnumerable<ManagerModel>>(_repo.GetManagerData(ActNum, Month, WeekNum)));
        }

        [HttpGet("GetLastManagerData")]
        public IActionResult GetLastManagerData()
        {
            return Ok(_mapper.Map<IEnumerable<ManagerModel>>(_repo.GetLastManagerData()));
        }

        [Authorize(Policy = "Managers")]
        [HttpGet("/MessageUsers")]
        public IActionResult GetMessageUsersList()
        {

            var MessageUsersList = _repository.MessageUsers.ToList();

            if (MessageUsersList == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(MessageUsersList);
            }

        }

    }
}