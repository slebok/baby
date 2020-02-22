namespace nursery.ast
{
    internal class LineOfCode
    {
        public string Content { get; protected set; }
        public uint Line { get; protected set; }
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