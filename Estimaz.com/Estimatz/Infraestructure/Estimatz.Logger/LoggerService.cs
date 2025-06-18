using Serilog;

namespace Estimatz.Logger
{
	public class LoggerService : ILoggerService
	{
		public ILogger Logger { get; }

		public LoggerService()
        {
			Logger = Log.Logger;
        }
    }
}
