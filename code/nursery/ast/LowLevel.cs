namespace nursery.ast
{
    public class LineOfCode
    {
        public string Content { get; protected set; }
    }

    public class LineZoneA : LineOfCode
    {
        internal LineZoneA(string x)
        {
            Content = x;
        }
    }

    public class LineZoneB : LineOfCode
    {
        internal LineZoneB(string x)
        {
            Content = x;
        }
    }
}