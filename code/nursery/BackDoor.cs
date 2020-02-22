using nursery.ast;
using nursery.meta;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("workout")]

namespace nursery
{
    internal static class BackDoor
    {
        internal static List<LineOfCode> Preprocess(string input)
            => Preprocessor.Preprocess(new List<string>() { input });

        internal static BcProgram Parse(string input)
            => Parser.Parse(Preprocessor.Preprocess(new List<string>() { input }));

        internal static BcProgram Parse(string input1, string input2)
            => Parser.Parse(Preprocessor.Preprocess(new List<string>() { input1, input2 }));
    }
}