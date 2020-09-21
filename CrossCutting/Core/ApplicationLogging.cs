using Microsoft.Extensions.Logging;

namespace CrossCutting.Core
{
    public static  class ApplicationLogging
    {
        /// <summary>
        /// The logger factory.
        /// </summary>
        private static ILoggerFactory loggerFactory;

        public static void ConfigureNlogLogger(ILoggerFactory factory)
        {
            loggerFactory = factory;
        }

        public static ILogger CreateLogger<T>()
        {
            return LoggerFactoryExtensions.CreateLogger<T>(loggerFactory);
        }
    }
}
