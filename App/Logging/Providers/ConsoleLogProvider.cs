using Swift.Balancer.App.Logging.Interface;
using System;

namespace Swift.Balancer.App.Logging.Providers
{
    public class ConsoleLogProvider : ILoggable
    {
        private void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        private void WriteLine(string message, ConsoleColor color)
            => Write($"{message}{Environment.NewLine}", color);

        public void Error(string message)
            => WriteLine($"[-] - {message}", ConsoleColor.Red);

        public void Warning(string message)
            => WriteLine($"[!] - {message}", ConsoleColor.Yellow);

        public void Debug(string message)
            => WriteLine($"[*] - {message}", ConsoleColor.Cyan);

        public void Success(string message)
            => WriteLine($"[+] - {message}", ConsoleColor.Green);
    }
}