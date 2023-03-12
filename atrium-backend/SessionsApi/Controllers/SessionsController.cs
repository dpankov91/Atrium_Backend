using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SessionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly SessionDbContext _sessionDbContext;

        public SessionsController(SessionDbContext sessionDbContext)
        {
            _sessionDbContext = sessionDbContext;
        }

        // GET: api/<ProceduresController>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Session>> Get()
        {
            return _sessionDbContext.Sessions.ToList();
        }

        // GET api/<ProceduresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> Get(int id)
        {
            var session = await _sessionDbContext.Sessions.FindAsync(id);
            return session;
        }

        // POST api/<ProceduresController>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Post([FromBody] Session session)
        {
            await _sessionDbContext.Sessions.AddAsync(session);
            await _sessionDbContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ProceduresController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Put(int id, [FromBody] Session session)
        {
            _sessionDbContext.Sessions.Update(session);
            await _sessionDbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProceduresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var session = await _sessionDbContext.Sessions.FindAsync(id);
            _sessionDbContext.Sessions.Remove(session);
            await _sessionDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
