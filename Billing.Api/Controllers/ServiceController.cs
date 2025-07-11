using Billing.Core;
using Billing.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ApiControllerBase
    {
        private readonly BaseRepository _repo = null;

        public ServiceController(
            ILogger<ServiceController> logger,
            ILoggerFactory loggerF,
            BillingContext db,
            IHttpContextAccessor accessor) : base(db, logger, loggerF, accessor)
        {
            BaseRepository._loggerF = loggerF;
            _repo = new BaseRepository(logger, loggerF, db);
        }

        [HttpGet]
        [Route("ping")]
        public ObjectResult Ping() => new("Pong!");
    }
}
