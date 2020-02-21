using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcDisplay : BcStatement
    {
        internal List<BcDisplayable> Values = new List<BcDisplayable>();
        internal bool Advancing = true;
    }

    internal class BcDisplayable : AstNode
    {
        internal BcAtomic Value;
        internal Delimited By = Delimited.BySize;
    }

    internal enum Delimited { BySize, BySpace };
}