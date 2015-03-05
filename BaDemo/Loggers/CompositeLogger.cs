using System.Collections.Generic;
using Microsoft.Practices.Prism.Logging;

namespace BaDemo.Loggers
{
    public class CompositeLogger : ILoggerFacade
    {
        public static ILoggerFacade Create(IEnumerable<ILoggerFacade> loggers)
        {
            return new CompositeLogger(loggers);
        }

        private readonly IEnumerable<ILoggerFacade> _loggers;

        private CompositeLogger(IEnumerable<ILoggerFacade> loggers)
        {
            _loggers = loggers;
        }

        public void Log(string message, Category category, Priority priority)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(message, category, priority);
            }
        }
    }
}
