using System.Collections.Generic;
using Extensions.Object;
using Extensions.Object.Exceptions;
using Swift.Balancer.App.Logging.Interface;
using Swift.Balancer.App.Logging.Providers;

namespace Swift.Balancer.App.Logging
{
    public class LogExObject : ExObject
    {
         /// <summary>
        /// Registered logging providers
        /// </summary>
        private readonly List<ILoggable> _logProviders = new List<ILoggable>();
        
        /// <summary>
        /// Indicates if debug mode detected
        /// </summary>
        private bool _debugEnabled = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        public LogExObject()
            : base()
        {
            Bootstrap();
        }

        /// <summary>
        /// Constructor with manually tag
        /// </summary>
        /// <param name="tag"></param>
        public LogExObject(string tag)
            : base(tag)
        {
            Bootstrap();
        }

        /// <summary>
        /// Bootstrap the systems
        /// </summary>
        private void Bootstrap()
        {
            #if DEBUG
            _debugEnabled = true;
            #else
            DebugEnabled = false;
            #endif
            
            Register();
            
            // It's would be throws if debug mode enabled, but providers not detected
            ThrowIf(_logProviders.Count <= 0 && _debugEnabled, 
                new ExException("Log providers list is empty"));
        }

        /// <summary>
        /// Registering the providers
        /// </summary>
        private void Register()
        {
            _logProviders.Add(new ConsoleLogProvider());
            _logProviders.Add(new FileLogProvider());
        }
        
        /// <summary>
        /// Get the debug mode flag
        /// </summary>
        /// <returns></returns>
        public bool IsDebug()
            => _debugEnabled;

        /// <summary>
        /// Write error message
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
            => _logProviders.ForEach(x => x.Error(message));

        /// <summary>
        /// Write warning message
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
            => _logProviders.ForEach(x => x.Warning(message));

        /// <summary>
        /// Write success message
        /// </summary>
        /// <param name="message"></param>
        public void Success(string message)
            => _logProviders.ForEach(x => x.Success(message));

        /// <summary>
        /// Write debug message, works if debug mode
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            if (_debugEnabled)
            {
                _logProviders.ForEach(x => x.Debug(message));
            }
        }
    }
}