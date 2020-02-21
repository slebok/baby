using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcEvaluate : BcStatement
    {
        // TODO: support expressions of different types and binary/unary operators
        internal BcBoolean Source;

        internal List<BcEvaluateBranch> Whens = new List<BcEvaluateBranch>();
    }

    internal class BcEvaluateBranch : AstNode
    {
        // NB: length 0 means OTHER
        internal List<BcAtomic> Conditions = new List<BcAtomic>();

        internal List<BcSentence> Code = new List<BcSentence>();
    }
}