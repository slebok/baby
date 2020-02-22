using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcBlock : AstNode
    {
        internal List<BcSentence> Sentences = new List<BcSentence>();
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