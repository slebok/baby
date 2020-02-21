using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcMove : AstNode
    {
        internal List<BcIdRef> Target = new List<BcIdRef>();
    }

    internal class BcMoveCorresponding : BcMove
    {
        internal BcIdRef Source;
    }

    internal class BcMoveFigurative : BcMove
    {
        internal Figurative Source;
    }

    internal enum Figurative { HighValues, LowValues, Spaces };
}