﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SessionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        // GET: api/<SessionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value3", "value4" };
        }

        // GET api/<SessionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SessionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SessionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SessionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
