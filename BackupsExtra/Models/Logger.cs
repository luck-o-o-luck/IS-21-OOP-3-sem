using BackupsExtra.Tools;
using Microsoft.Extensions.Logging;

namespace BackupsExtra.Models
{
    public class Logger
    {
        private static ILogger _logger = null;

        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        public static void LoggingInformation(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new BackupsExtraException("String is null");

            _logger.LogInformation(text);
        }
    }
}