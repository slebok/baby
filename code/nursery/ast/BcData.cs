using System.Collections.Generic;

namespace nursery.ast
{
    internal abstract class BcDataEntry
    {
        internal int Level;
        internal string Name;

        internal abstract BcDataEntry Like();
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

        internal override BcDataEntry Like()
            => new BcDataField(Level, Name, Pattern);
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

        internal override BcDataEntry Like()
            => Inner.Like();
    }

    internal class BcDataView : BcDataEntry
    {
        internal List<BcDataEntry> Inners = new List<BcDataEntry>();

        internal BcDataView(int level, string v)
        {
            Level = level;
            Name = v;
            if (v.EndsWith('.'))
                Name = Name.Substring(0, Name.Length - 1);
        }

        internal override BcDataEntry Like()
        {
            var r = new BcDataView(Level, Name);
            foreach (var f in Inners)
                r.Inners.Add(f.Like());
            // NB: this can lead to screwed up levels inside, but at this point we won't care
            return r;
        }
    }
}