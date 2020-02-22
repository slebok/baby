using System.Collections.Generic;

namespace nursery.ast
{
    internal class BcProgram : AstNode
    {
        // IDENTIFICATION DIVISION
        internal SortedDictionary<string, string> Identifications = new SortedDictionary<string, string>();

        // DATA DIVISION
        // TODO
        // PROCEDURE DIVISION
        internal SortedDictionary<string, List<BcSentence>> Paragraphs = new SortedDictionary<string, List<BcSentence>>();
    }
}