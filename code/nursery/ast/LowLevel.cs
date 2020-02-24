using System.Text;

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

    internal class Token
    {
        internal string Value { get; private protected set; }
        internal uint Row { get; private protected set; }
        internal uint Col { get; private protected set; }
    }

    internal class UnquotedToken : Token
    {
        public UnquotedToken(StringBuilder sb)
        {
            Value = sb.ToString();
        }

        public UnquotedToken(string s)
        {
            Value = s;
        }
    }

    internal class QuotedToken : Token
    {
        public QuotedToken(StringBuilder sb)
        {
            Value = sb.ToString();
        }

        public QuotedToken(string s)
        {
            Value = s;
        }
    }

    /// <summary>
    /// End of paragraph
    /// </summary>
    internal class EOPToken : Token
    {
    }

    /// <summary>
    /// End of sentence
    /// </summary>
    internal class EOSToken : Token
    {
    }
}