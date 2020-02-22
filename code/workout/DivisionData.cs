using Microsoft.VisualStudio.TestTools.UnitTesting;
using nursery;
using nursery.ast;
using System;

namespace workout
{
    [TestClass]
    public class DivisionData
    {
        [TestMethod, TestCategory("parser")]
        public void TopLevelPic()
        {
            int level = 1;
            string name = "FOO";
            string patt = "9V99";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            var field = program.Data[0] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelPicCalledPicture()
        {
            int level = 1;
            string name = "PICTUREIS";
            string patt = "X(20)";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            var field = program.Data[0] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }
    }
}