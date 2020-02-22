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
        public void TopLevelPicName()
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

        [TestMethod, TestCategory("parser")]
        public void TopLevelPics2()
        {
            int level = 3;
            string name1 = "FOO", name2 = "BAR";
            string patt1 = "X(20)", patt2 = "A(10)";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt1)),
                    X.A(X.PIC(level, name2, patt2))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(2, program.Data.Count);
            var field = program.Data[0] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name1, field.Name);
            Assert.AreEqual(patt1, field.Pattern);
            field = program.Data[1] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name2, field.Name);
            Assert.AreEqual(patt2, field.Pattern);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelLike()
        {
            int level = 3;
            string name1 = "FOO", name2 = "BAR";
            string patt = "X(20)";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt)),
                    X.A(X.LIKE(level, name2, name1))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(2, program.Data.Count);
            var field = program.Data[0] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name1, field.Name);
            Assert.AreEqual(patt, field.Pattern);
            field = program.Data[1] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name2, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelLike2()
        {
            int level = 3;
            string name1 = "FOO", name2 = "BAR", name3 = "QEX";
            string patt = "X(20)";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt)),
                    X.A(X.LIKE(level, name2, name1)),
                    X.A(X.LIKE(level, name3, name2)) // NB: nested LIKE
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(3, program.Data.Count);
            var field = program.Data[0] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name1, field.Name);
            Assert.AreEqual(patt, field.Pattern);
            field = program.Data[1] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name2, field.Name);
            Assert.AreEqual(patt, field.Pattern);
            field = program.Data[2] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name3, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelLikeName()
        {
            int level = 3;
            string name1 = "DISLIKE", name2 = "UNLIKE";
            string patt = "X(20)";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt)),
                    X.A(X.LIKE(level, name2, name1))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(2, program.Data.Count);
            var field = program.Data[0] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name1, field.Name);
            Assert.AreEqual(patt, field.Pattern);
            field = program.Data[1] as BcDataField;
            Assert.IsNotNull(field);
            Assert.AreEqual(level, field.Level);
            Assert.AreEqual(name2, field.Name);
            Assert.AreEqual(patt, field.Pattern);
        }
    }
}