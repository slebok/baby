namespace nursery.ast
{
    internal class BcPerform : BcStatement
    {
        internal BcIdRef Target;
        internal BcIdRef Through;
        internal int Times = 1;
    }
}