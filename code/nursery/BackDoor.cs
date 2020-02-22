using nursery.ast;
using nursery.meta;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("workout")]

namespace nursery
{
    internal static class BackDoor
    {
        internal static List<LineOfCode> Preprocess(string input)
            => Preprocessor.Preprocess(new List<string>() { input });

        internal static BcProgram Parse(params string[] inputs)
            => Parser.Parse(Preprocessor.Preprocess(inputs.ToList()));
    }
}