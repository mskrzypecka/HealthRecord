using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecord.Helpers
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }
}
