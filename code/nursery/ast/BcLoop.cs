using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcLoop : BcStatement
    {
        // TODO: have support for sentences, not statements
        internal List<BcLoopStatement> Body = new List<BcLoopStatement>();
    }

    internal class BcVarying : BcLoopStatement
    {
        internal BcIdRef Identifier;
        internal BcAtomic From; // default: low-values of type
        internal BcAtomic To; // default: high-values of type

        internal BcAtomic By // default: 1
            = new BcDecLiteral(1);
    }

    internal class BcWhile : BcLoopStatement
    {
        internal BcBoolean Condition;
    }

    internal class BcUntil : BcLoopStatement
    {
        internal BcBoolean Condition;
    }
}