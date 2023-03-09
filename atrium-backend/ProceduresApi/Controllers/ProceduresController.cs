using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProceduresApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProceduresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private readonly ProcedureDbContext _procedureDbContext;

        public ProceduresController(ProcedureDbContext procedureDbContext)
        {
            _procedureDbContext = procedureDbContext;
        }

        // GET: api/<ProceduresController>
        [HttpGet]
        public ActionResult<IEnumerable<Procedure>> Get()
        {
            return _procedureDbContext.Procedures.ToList();
        }

        // GET api/<ProceduresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> Get(int id)
        {
            var procedure = await _procedureDbContext.Procedures.FindAsync(id);
            return procedure;
        }

        // POST api/<ProceduresController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Procedure procedure)
        {
            await _procedureDbContext.Procedures.AddAsync(procedure);
            await _procedureDbContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ProceduresController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Procedure procedure)
        {
            _procedureDbContext.Procedures.Update(procedure);
            await _procedureDbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ProceduresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var procedure = await _procedureDbContext.Procedures.FindAsync(id);
            _procedureDbContext.Procedures.Remove(procedure);
            await _procedureDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
