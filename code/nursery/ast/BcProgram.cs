using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcProgram : AstNode
    {
        // IDENTIFICATION DIVISION
        internal SortedDictionary<string, string> Identifications = new SortedDictionary<string, string>();

        // DATA DIVISION
        internal List<BcDataEntry> Data = new List<BcDataEntry>();

        // PROCEDURE DIVISION
        internal SortedDictionary<string, BcBlock> Paragraphs = new SortedDictionary<string, BcBlock>();
    }
}