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
        private const string One = " ";
        private const string Six = "      ";
        private const string Four = "    ";

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
            string input = Six;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine07()
        {
            string input = Six + One;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine11()
        {
            string input = Six + One + Four;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void EmptyLine12()
        {
            string input = Six + Six;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(0, lines.Count);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void LineZoneA()
        {
            string id = "IDENTIFICATION DIVISION.";
            string input = Six + One + id;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(1, lines.Count);
            LineZoneA line = lines[0] as LineZoneA;
            Assert.IsNotNull(line);
            Assert.AreEqual(id, line.Content);
        }

        [TestMethod, TestCategory("preprocessor")]
        public void LineZoneB()
        {
            string next = "NEXT SENTENCE";
            string input = Six + One + Four + next;
            List<LineOfCode> lines = BackDoor.Preprocess(input);
            Assert.IsNotNull(lines);
            Assert.AreEqual(1, lines.Count);
            LineZoneB line = lines[0] as LineZoneB;
            Assert.IsNotNull(line);
            Assert.AreEqual(next, line.Content);
        }
    }
}