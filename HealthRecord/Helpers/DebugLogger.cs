using System.Diagnostics;

namespace HealthRecord.Helpers
{
    public class DebugLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Debug.WriteLine($"{level}:\n\t{message}");
        }
    }
}