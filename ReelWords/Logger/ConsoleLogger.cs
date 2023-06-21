using System;

namespace ReelWords.Logger
{
	public class ConsoleLogger : ILogger
	{
        private static readonly ConsoleLogger instance = new ConsoleLogger();

        public static ConsoleLogger Instance
        {
            get
            {
                return instance;
            }
        }

        private ConsoleLogger()
		{
		}

        public void Error(string error)
        {
            Console.WriteLine($"[Error] message: {error}");
        }

        public void Error(string error, Exception ex)
        {
            Console.WriteLine($"[Error] message: {error} {ex.ToString}");
        }

        public void Info(string info)
        {
            Console.WriteLine($"[Info] message: {info}");
        }
    }
}

