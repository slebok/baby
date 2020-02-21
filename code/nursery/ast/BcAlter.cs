namespace nursery.ast
{
    internal class BcAlter : BcStatement
    {
        internal BcIdRef Scope;
        internal BcIdRef AlteredDestination;
    }
}