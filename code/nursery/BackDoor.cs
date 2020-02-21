using nursery.ast;
using nursery.meta;
using System.Collections.Generic;

namespace nursery
{
    public static class BackDoor
    {
        public static List<LineOfCode> Preprocess(string input)
            => Preprocessor.Preprocess(new List<string>() { input });
    }
}