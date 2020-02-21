namespace nursery.ast
{
    internal class BcAtomic : AstNode
    {
    }

    internal class BcIdRef : BcAtomic
    {
    }

    internal class BcLiteral : BcAtomic
    {
    }

    internal class BcCharLiteral : BcLiteral
    {
        internal string Value;

        public BcCharLiteral(string x)
        {
            Value = x;
        }
    }

    internal class BcDecLiteral : BcLiteral
    {
        internal long Value; // TODO: make it scale

        public BcDecLiteral(long x)
        {
            Value = x;
        }
    }

    internal class BcBoolean : AstNode
    {
    }
}