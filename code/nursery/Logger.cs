using System;

namespace nursery
{
    internal static class Logger
    {
        internal static void Error(uint line, string message)
        {
            Console.WriteLine($"FATAL @ {line}: {message}");
            //Environment.Exit(-1);
            throw new Exception(message);
        }

        internal static void ErrorIdDivNotFound(uint line)
            => Error(line, "Identification division not found");

        internal static void ErrorIdDivClauseBug(uint line, string clause)
            => Error(line, "Identification clause incorrect: " + clause);

        internal static void ErrorIdDivClauseDup(uint line, string clause)
            => Error(line, "Identification clause duplicate: " + clause);
    }
}