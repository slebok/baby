using System;
using System.Collections.Generic;
using System.Text;

namespace nursery.ast
{
    class BcSignal:BcStatement
    {
        internal BcIdRef Target; // null means OFF
    }
}
