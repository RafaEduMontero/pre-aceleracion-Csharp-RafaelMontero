using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeAlkemyDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieOrSerieController : ControllerBase
    {
        // GET: api/<MovieOrSerieController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MovieOrSerieController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MovieOrSerieController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MovieOrSerieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieOrSerieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
