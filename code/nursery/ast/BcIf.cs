using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcIf : BcStatement
    {
        internal BcBoolean Condition;
        internal List<BcStatement> Then = new List<BcStatement>();
        internal List<BcStatement> Else = new List<BcStatement>();
    }
}