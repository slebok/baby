using System.Collections.Generic;

namespace nursery.ast
{

    public class BcSentence : AstNode
    {
        internal List<BcStatement> Statements = new List<BcStatement>();
    }

    public class BcStatement : BcLoopStatement
    {
    }

    public class BcLoopStatement : AstNode
    {
    }
}