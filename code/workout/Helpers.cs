﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using nursery.ast;

namespace workout
{
    internal static class X
    {
        internal const string One = " ";
        internal const string Six = "      ";
        internal const string Four = "    ";

        internal const string ID_DIV = "IDENTIFICATION DIVISION";
        internal const string DA_DIV = "DATA DIVISION";
        internal const string PR_DIV = "PROCEDURE DIVISION";

        internal const string S_NXT_SEN = "NEXT_SENTENCE";

        internal static string A(string code)
            => Six + One + code;

        internal static string B(string code)
            => Six + One + Four + code;

        internal static string D(string code)
            => code + '.';

        internal static string PIC(int level, string name, string pattern)
            => $"{level:00} {name} PICTURE IS {pattern}";

        internal static string LIKE(int level, string name, string likeName)
            => $"{level:00} {name} LIKE {likeName}";

        internal static string VIEW(int level, string name)
            => $"{level:00} {name}.";

        internal static string ACCEPT(string name)
            => $"ACCEPT {name}.";

        internal static void ValidateDataField(BcDataEntry data, int level, string name, string patt)
        {
            var field = data as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }
    }
}