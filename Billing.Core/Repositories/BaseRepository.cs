using Microsoft.Extensions.Logging;

namespace Billing.Core.Repositories
{
    public class BaseRepository
    {
        public readonly BillingContext _context;
        private readonly ILogger _logger;
        public static ILoggerFactory _loggerF;
        public BaseRepository(ILogger logger, ILoggerFactory loggerF, BillingContext db)
        {
            _logger = logger;
            _loggerF = loggerF;
            this._context = db;
        }
    }
}
