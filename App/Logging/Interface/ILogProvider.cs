namespace Swift.Balancer.App.Logging.Interface
{
    /// <summary>
    /// Loggable interface for logging object
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// Error log
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        
        /// <summary>
        /// Warning log
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);
        
        /// <summary>
        /// Debug log
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        
        /// <summary>
        /// Success log
        /// </summary>
        /// <param name="message"></param>
        void Success(string message);
    }
}