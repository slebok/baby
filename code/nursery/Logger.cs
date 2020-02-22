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

        internal static void ErrorIdDivClauseBug(uint line, string clause)
            => Error(line, "Identification clause incorrect: " + clause);

        internal static void ErrorIdDivClauseDup(uint line, string clause)
            => Error(line, "Identification clause duplicate: " + clause);

        internal static void ErrorDataDivWrongLevel(uint line, string level)
            => Error(line, "Wrong level in the data entry: " + level);

        internal static void ErrorDataDivWrongOccurs(uint line, string name)
            => Error(line, "Wrong occurs clause in the data entry: " + name);

        internal static void ErrorDataDivNotAView(uint line, string name)
            => Error(line, "A field cannot be connected to a view: " + name);
    }
}