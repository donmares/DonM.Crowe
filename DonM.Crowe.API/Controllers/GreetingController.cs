using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DonM.Crowe.Application.Interfaces;
using DonM.Crowe.Infrastructure.Models;

namespace DonM.Crowe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingService _greetingService;

        public GreetingController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Greeting>> Get()
        {
            IEnumerable<Greeting> greetingList = _greetingService.GetAll();
            if (greetingList != null && greetingList.Count() > 0)
                return Ok(greetingList);
            else
                return NoContent();
        }


        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] Greeting value)
        {
            _greetingService.Save(value);
            return Ok(value);
        }


    }
}
