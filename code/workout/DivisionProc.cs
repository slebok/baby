using Microsoft.VisualStudio.TestTools.UnitTesting;
using nursery;
using nursery.ast;
using System;
using System.Collections.Generic;
using System.Text;

namespace workout
{
    [TestClass]
    public class DivisionProc
    {
        private void ValidateDataField(BcDataEntry data, int level, string name, string patt)
        {
            var field = data as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }

        [TestMethod, TestCategory("parser")]
        public void Accept1()
        {
            int level = 1;
            string name = "FOO";
            string patt = "9V99";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name, patt)),
                    X.A(X.D(X.PR_DIV)),
                    X.A(X.ACCEPT(name))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            ValidateDataField(program.Data[0], level, name, patt);
            Assert.AreEqual(1, program.Paragraphs.Count);
        }
    }
}
