using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using HealthCatalyst.Assessment.Domain.Validation;

namespace HealthCatalyst.Assessment.API.Logging
{
    /// <summary>
    /// Writes data to the log
    /// </summary>
    public class Logger
    {
        private static readonly Lazy<ILog> _log = new Lazy<ILog>(() =>
            LogManager.GetLogger(_logName));

        private static string _logName = null;
        private readonly static string ERR_MSG = "The logger must be initialized at program start before writing to the log.";
        private readonly static string CLASS_NAME = "LogFile";

        public static void Initialize(string logConfiguration)
        {
            if (_logName != null)
                throw new NotSupportedException("The Log has already been initialized and cannot be initialized more than once.");

            _logName = logConfiguration;
        }

        /// <summary>
        /// Writes message on same line
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="append"></param>
        public static void Write(string msg, bool append = false)
        {
            Guard.NotNull(_log.Value, CLASS_NAME, ERR_MSG);

            if (append)
                _log.Value.Info(msg);
            else
                _log.Value.Info($"\r{msg}");

        }

        /// <summary>
        /// Writes message with new line
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLine(string msg)
        {
            Guard.NotNull(_log.Value, CLASS_NAME, ERR_MSG);
            _log.Value.Info($"{msg}");
        }

        /// <summary>
        /// Writes an exception message
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="ex"></param>
        public static void WriteError(string errorMsg, Exception ex = null)
        {
            Guard.NotNull(_log.Value, CLASS_NAME, ERR_MSG);
            _log.Value.Error(errorMsg, ex);
        }

        /// <summary>
        /// Writes a warning message
        /// </summary>
        /// <param name="warningMsg"></param>
        /// <param name="ex"></param>
        public static void WriteWarning(string warningMsg, Exception ex = null)
        {
            Guard.NotNull(_log.Value, CLASS_NAME, ERR_MSG);
            _log.Value.Warn(warningMsg, ex);
        }
    }
}