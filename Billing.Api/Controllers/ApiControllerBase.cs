using Billing.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Billing.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ApiControllerBase : ControllerBase
    {
        public readonly BillingContext _context;
        private readonly ILogger<ApiControllerBase> _logger;
        private readonly ILoggerFactory _loggerF;
        private readonly IHttpContextAccessor _accessor;

        public ApiControllerBase(BillingContext context, ILogger<ApiControllerBase> logger, ILoggerFactory loggerF, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _loggerF = loggerF;
            _context = context;
            _accessor = accessor;
        }

        protected ActionResult JsonResponse(object obj)
        {
            var jobj = JObject.FromObject(obj);

            return new ObjectResult(jobj.ToString());
        }
    }
}
