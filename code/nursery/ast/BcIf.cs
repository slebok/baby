using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcIf : BcStatement
    {
        internal BcBoolean Condition;
        internal BcBlock Then;
        internal BcBlock Else;
    }
}