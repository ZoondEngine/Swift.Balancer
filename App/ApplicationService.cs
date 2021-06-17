using System;
using System.IO;
using Extensions.Object;
using Swift.Balancer.App.Interface;
using Swift.Balancer.App.Logging;

namespace Swift.Balancer.App
{
    public abstract class ApplicationService : ExObject, IContainFolders, IService
    {
        private const string HeadServerFolder = "head-server";

        /// <summary>
        /// Logger object
        /// </summary>
        private readonly LogExObject _log;

        /// <summary>
        ///  Default constructor
        /// </summary>
        protected ApplicationService()
            : base()
        {
            // Try to search already instanced log object,
            // cuz why some one more for us ?
            _log = FindObjectOfType<LogExObject>() ?? Instantiate<LogExObject>();
        }

        /// <summary>
        /// Get name of service
        /// </summary>
        /// <returns></returns>
        public abstract string Name();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract Version Version();

        /// <summary>
        /// Throw exception if condition reversed
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public void ThrowUnless(bool condition, Exception ex, string message)
            => ThrowIf(!condition, ex, message);

        /// <summary>
        /// Throw exception if condition is true
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <exception cref="Exception"></exception>
        public void ThrowIf(bool condition, Exception ex, string message)
        {
            if (!condition) return;
            ex ??= new Exception(message ?? $"Exception occured in service {Name()} v{Version()}");

            throw ex;
        }
        
        /// <summary>
        /// Gets the system folder
        /// </summary>
        /// <returns></returns>
        public string SystemFolder()
        {
            return Path.Combine(HeadServerFolder, "system");
        }

        /// <summary>
        /// Gets the configuration folder
        /// </summary>
        /// <returns></returns>
        public string ConfigFolder()
        {
            return Path.Combine(HeadServerFolder, "config");
        }

        /// <summary>
        /// Gets the plugins folder
        /// </summary>
        /// <returns></returns>
        public string PluginsFolder()
        {
            return Path.Combine(HeadServerFolder, "plugins");
        }

        /// <summary>
        /// Gets the log folder
        /// </summary>
        /// <returns></returns>
        public string LogFolder()
        {
            return Path.Combine(HeadServerFolder, "logs");
        }

        /// <summary>
        /// Drop error message
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
            => _log.Error(message);

        /// <summary>
        /// Drop error message
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
            => _log.Warning(message);

        /// <summary>
        /// Drop error message
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
            => _log.Debug(message);

        /// <summary>
        /// Drop success message
        /// </summary>
        /// <param name="message"></param>
        public void Success(string message)
            => _log.Success(message);
    }
}