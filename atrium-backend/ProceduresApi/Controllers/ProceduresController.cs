using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProceduresApi.Data;
using ProceduresApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProceduresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private readonly IRepository<Procedure> _repository;

        public ProceduresController(IRepository<Procedure> repos)
        {
            _repository = repos;
        }

        // GET: api/<ProceduresController>
        [HttpGet]
        public ActionResult<IEnumerable<Procedure>> Get()
        {
            return _repository.GetAll().ToList();
        }

        // GET api/<ProceduresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> Get(int id)
        {
            var procedure = await _repository.Get(id);
            return procedure;
        }

        // POST api/<ProceduresController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Procedure procedure)
        {
            procedure.Status = (Procedure.ProcedureStatus)1;
            await _repository.Add(procedure);
            return Ok();
        }

        // PUT api/<ProceduresController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Procedure procedure)
        {
            _repository.Edit(procedure);
            return Ok();
        }

        // DELETE api/<ProceduresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _repository.Remove(id);
            return Ok();
        }
    }
}
