using System.Collections.Generic;

namespace nursery.ast
{
    // BcAdd, BcSubtract, BcDivide, BcMultiply
    internal class BcCompute : BcStatement
    {
        internal List<BcAtomic> Added = new List<BcAtomic>();
        internal BcAtomic Source;
        internal BcIdRef Target;
        internal BcIdRef Target2; // remainder
    }
}