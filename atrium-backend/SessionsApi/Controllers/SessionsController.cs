using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionsApi.Infrastructure;
using SessionsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SessionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly SessionDbContext _sessionDbContext;
        private readonly IMessagePublisher messagePublisher;
        private readonly ILogger<SessionsController> _logger;

        public SessionsController(
            SessionDbContext sessionDbContext,
            IMessagePublisher publisher,
            ILogger<SessionsController> logger)
        {
            _sessionDbContext = sessionDbContext;
            messagePublisher = publisher;
            _logger = logger;
        }

        // GET: api/<SessionsController>
        [HttpGet]
        public ActionResult<IEnumerable<Session>> Get()
        {
            return _sessionDbContext.Sessions.ToList();
        }

        // GET api/<SessionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> Get(int id)
        {
            var session = await _sessionDbContext.Sessions.FindAsync(id);
            return session;
        }

        // POST api/<SessionsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Session session)
        {
            var credentials = new BasicAWSCredentials("AKIAZMR5L5GY7M4LASUD", "pHz9OWhTtRIElopqutJzXaGoNr8DgNIXZucsBAzl"); // provide aws credentials

            var logClient = new AmazonCloudWatchLogsClient(credentials, RegionEndpoint.USEast2);
            var logGroupName = "atrium-app";
            var logStreamName = DateTime.UtcNow.ToString("yyyyMMddHHmmssff");
            await logClient.CreateLogGroupAsync(new CreateLogGroupRequest(logStreamName));
            await logClient.CreateLogStreamAsync(new CreateLogStreamRequest(logGroupName, logStreamName));
            await logClient.PutLogEventsAsync(new PutLogEventsRequest()
            {
                LogGroupName = logGroupName,
                LogStreamName = logStreamName,
                LogEvents = new List<InputLogEvent>()
                {
                    new InputLogEvent() { Message = "Create session called", Timestamp = DateTime.UtcNow }
                }



            }); ;
            messagePublisher.PublishProcedureStatusChangedMessage(
                session.ProcedureId, "in_process");

            await _sessionDbContext.Sessions.AddAsync(session);
            await _sessionDbContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<SessionsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Put(int id, [FromBody] Session session)
        {


            _sessionDbContext.Sessions.Update(session);
            await _sessionDbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<SessionsController>/5
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
