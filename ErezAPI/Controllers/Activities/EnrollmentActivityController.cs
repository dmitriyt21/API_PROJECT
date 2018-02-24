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
    [Route("api/enrollmentActivity")]
    public class EnrollmentActivityController : Controller
    {
        private IRepository _repository;
        private IMapper _mapper;

        public EnrollmentActivityController(IRepository repository
            , IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var EA = _repository.GetRecentEnrollmentActivity();
                IEnumerable<EnrollmentActivityModel> EAM = _mapper.Map<IEnumerable<EnrollmentActivityModel>>(EA);
                return Ok(EAM);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        //[HttpGet("{id}/{hist}")]
        public IActionResult Get(int id, bool? hist)
        {
            try
            {

                return Ok(_repository.GetEnrollmentActivity(id, hist));

            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred: {ex}");
            }
        }
    }
}