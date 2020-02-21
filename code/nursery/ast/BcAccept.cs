using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcAccept : BcStatement
    {
        internal List<BcIdRef> Accepted = new List<BcIdRef>();
    }
}