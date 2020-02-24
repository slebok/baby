using nursery.ast;
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

        internal static void Error(uint line, uint col, string message)
        {
            Console.WriteLine($"FATAL @ {line}:{col}: {message}");
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

        internal static void ErrorDataDivWrongLike(uint line, string val)
            => Error(line, "Wrong like clause in the data entry: " + val);

        internal static void ErrorDataDivNotAView(uint line, string name)
            => Error(line, "A field cannot be connected to a view: " + name);

        internal static void ErrorQuoteNotFound(uint line, string str)
            => Error(line, "Unmatched quote in: " + str);

        internal static void ErrorStrayQuoted(QuotedToken tokenQ)
            => Error(tokenQ.Row, tokenQ.Col, "Stray quoted token: " + tokenQ.Value);

        internal static void ErrorUnrecognisedToken(Token token)
            => Error(token.Row, token.Col, "Unrecognised token: " + token.Value);

        internal static void ErrorNotImplementedYet(uint row, uint col, string value)
            => Error(row, col, "Not implemented yet: " + value);
    }
}