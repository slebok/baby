using System.Collections.Generic;

namespace nursery.ast
{
    internal class AstNode
    {
    }
    internal class BcSentence : AstNode
    {
        internal List<BcStatement> Statements = new List<BcStatement>();
    }


    internal class BcStatement : BcLoopStatement
    {
    }

    internal class BcLoopStatement : AstNode
    {
    }
}