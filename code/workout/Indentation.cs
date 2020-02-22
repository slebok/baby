using Microsoft.VisualStudio.TestTools.UnitTesting;
using nursery;
using nursery.ast;
using System;
using System.Collections.Generic;

namespace workout
{
    [TestClass]
    public class Indentation
    {
        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine00()
        {
            string input = String.Empty;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine06()
        {
            string input = X.Six;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine07()
        {
            string input = X.Six + X.One;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine11()
        {
            string input = X.Six + X.One + X.Four;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine12()
        {
            string input = X.Six + X.Six;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void LineZoneA()
        {
            string input = X.A(X.D(X.ID_DIV));
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(1, lines.Count);
            LineZoneA line = lines[0] as LineZoneA;
            Assert.IsNotNull(line);
            Assert.AreEqual((uint)1, line.Line);
            Assert.AreEqual(X.D(X.ID_DIV), line.Content);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void LineZoneB()
        {
            string input = X.B(X.D(X.S_NXT_SEN));
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(1, lines.Count);
            LineZoneB line = lines[0] as LineZoneB;
            Assert.IsNotNull(line);
            Assert.AreEqual((uint)1, line.Line);
            Assert.AreEqual(X.D(X.S_NXT_SEN), line.Content);
        }
    }
}