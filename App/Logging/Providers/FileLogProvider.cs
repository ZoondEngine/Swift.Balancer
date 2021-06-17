using System;
using System.Globalization;
using System.IO;
using Swift.Balancer.App.Logging.Interface;

namespace Swift.Balancer.App.Logging.Providers
{
    public class FileLogProvider : ILoggable
    {
        private readonly string _fileSrc;

        public FileLogProvider()
        {
            _fileSrc = "head-server\\logs\\" + DateTime.Now.ToFileTimeUtc() + ".log";
        }
        
        private void Write(string message)
        {
            message = $"[{DateTime.Now.ToString(CultureInfo.CurrentCulture)}] - {message}";
            File.AppendAllLines(_fileSrc, new[] { message });
        }

        public void Error(string message)
            => Write($"[ERROR] - {message}");

        public void Warning(string message)
            => Write($"[WARNING] - {message}");

        public void Debug(string message)
            => Write($"[DEBUG] - {message}");

        public void Success(string message)
            => Write($"[SUCCESS] - {message}");
    }
}