namespace nursery.ast
{
    internal class LineOfCode
    {
        internal string Content { get; private protected set; }
        internal uint Line { get; private protected set; }
    }

    internal class LineZoneA : LineOfCode
    {
        internal LineZoneA(uint line, string x)
        {
            Line = line;
            Content = x;
        }
    }

    internal class LineZoneB : LineOfCode
    {
        internal LineZoneB(uint line, string x)
        {
            Line = line;
            Content = x;
        }
    }
}