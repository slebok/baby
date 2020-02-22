using Microsoft.VisualStudio.TestTools.UnitTesting;
using nursery;
using nursery.ast;

namespace workout
{
    [TestClass]
    public class DivisionData
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
        public void TopLevelPic()
        {
            int level = 1;
            string name = "FOO";
            string patt = "9V99";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            ValidateDataField(program.Data[0], level, name, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelPicName()
        {
            int level = 1;
            string name = "PICTUREISCOOL";
            string patt = "X(20)";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            ValidateDataField(program.Data[0], level, name, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelPics2()
        {
            int level = 3;
            string name1 = "FOO", name2 = "BAR";
            string patt1 = "X(20)", patt2 = "A(10)";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt1)),
                    X.A(X.PIC(level, name2, patt2))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(2, program.Data.Count);
            ValidateDataField(program.Data[0], level, name1, patt1);
            ValidateDataField(program.Data[1], level, name2, patt2);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelLike()
        {
            int level = 3;
            string name1 = "FOO", name2 = "BAR";
            string patt = "X(20)";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt)),
                    X.A(X.LIKE(level, name2, name1))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(2, program.Data.Count);
            ValidateDataField(program.Data[0], level, name1, patt);
            ValidateDataField(program.Data[1], level, name2, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelLike2()
        {
            int level = 3;
            string name1 = "FOO", name2 = "BAR", name3 = "QEX";
            string patt = "X(20)";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt)),
                    X.A(X.LIKE(level, name2, name1)),
                    X.A(X.LIKE(level, name3, name2)) // NB: nested LIKE
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(3, program.Data.Count);
            ValidateDataField(program.Data[0], level, name1, patt);
            ValidateDataField(program.Data[1], level, name2, patt);
            ValidateDataField(program.Data[2], level, name3, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void TopLevelLikeName()
        {
            int level = 3;
            string name1 = "DISLIKE", name2 = "UNLIKE";
            string patt = "X(20)";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.PIC(level, name1, patt)),
                    X.A(X.LIKE(level, name2, name1))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(2, program.Data.Count);
            ValidateDataField(program.Data[0], level, name1, patt);
            ValidateDataField(program.Data[1], level, name2, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void NestedPic()
        {
            int level1 = 1, level2 = 5;
            string name1 = "VIEW", name2 = "FOO";
            string patt = "99999V999";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.VIEW(level1, name1)),
                    X.B(X.PIC(level2, name2, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            var view = program.Data[0] as BcDataView;
            Assert.IsNotNull(view);
            Assert.AreEqual(level1, view.Level);
            Assert.AreEqual(name1, view.Name);
            Assert.AreEqual(1, view.Inners.Count);
            ValidateDataField(view.Inners[0], level2, name2, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void NestedPicName1()
        {
            int level1 = 1, level2 = 5;
            string name1 = "PICTUREOFTHEKING", name2 = "FOO";
            string patt = "99999V999";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.VIEW(level1, name1)),
                    X.B(X.PIC(level2, name2, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            var view = program.Data[0] as BcDataView;
            Assert.IsNotNull(view);
            Assert.AreEqual(level1, view.Level);
            Assert.AreEqual(name1, view.Name);
            Assert.AreEqual(1, view.Inners.Count);
            ValidateDataField(view.Inners[0], level2, name2, patt);
        }

        [TestMethod, TestCategory("parser")]
        public void NestedPicName2()
        {
            int level1 = 1, level2 = 5;
            string name1 = "UNLIKELY", name2 = "FOO";
            string patt = "99999V999";
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.DA_DIV)),
                    X.A(X.VIEW(level1, name1)),
                    X.B(X.PIC(level2, name2, patt))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(1, program.Data.Count);
            var view = program.Data[0] as BcDataView;
            Assert.IsNotNull(view);
            Assert.AreEqual(level1, view.Level);
            Assert.AreEqual(name1, view.Name);
            Assert.AreEqual(1, view.Inners.Count);
            ValidateDataField(view.Inners[0], level2, name2, patt);
        }
    }
}