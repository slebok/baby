using System;

namespace nursery
{
    internal static class Logger
    {
        internal static void Error(string message)
        {
            Console.WriteLine("FATAL: " + message);
            Environment.Exit(-1);
        }
    }
}