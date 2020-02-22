using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcDataEntry
    {
        internal int Level;
        internal string Name;
    }

    internal class BcDataField : BcDataEntry
    {
        internal string Pattern;

        internal BcDataField(int l, string n, string p)
        {
            Level = l;
            Name = n;
            Pattern = p;
        }
    }

    internal class BcDataArray : BcDataEntry
    {
        internal int Occurs;
        internal BcDataEntry Inner;

        internal BcDataArray(BcDataEntry data, int occurs)
        {
            Occurs = occurs;
            Inner = data;
            Level = Inner.Level;
            Name = Inner.Name;
        }
    }

    internal class BcDataView : BcDataEntry
    {
        internal List<BcDataEntry> Inners = new List<BcDataEntry>();
    }
}