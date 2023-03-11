using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Infrastructure;
using UserApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        // GET: api/<ProceduresController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _userDbContext.Users.ToList();
        }

        // GET api/<ProceduresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userDbContext.Users.FindAsync(id);
            return user;
        }

        // POST api/<ProceduresController>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ProceduresController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            _userDbContext.Users.Update(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProceduresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userDbContext.Users.FindAsync(id);
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
