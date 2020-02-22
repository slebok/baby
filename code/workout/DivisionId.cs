using Microsoft.VisualStudio.TestTools.UnitTesting;
using nursery;
using nursery.ast;
using System;
using System.Linq;

namespace workout
{
    [TestClass]
    public class DivisionId
    {
        [TestMethod, TestCategory("parser")]
        public void EmptyProgram()
        {
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.ID_DIV))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(0, program.Identifications.Count);
            Assert.AreEqual(0, program.Paragraphs.Count);
        }

        [TestMethod, TestCategory("parser")]
        public void OneClaimBoolean()
        {
            string claim = "AUTHOR-is-grammarware";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.ID_DIV)),
                    X.B(X.D(claim))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(1, program.Identifications.Count);
            string k = program.Identifications.Keys.First();
            Assert.AreEqual(claim, k);
            Assert.AreEqual("", program.Identifications[k]);
            Assert.AreEqual(0, program.Paragraphs.Count);
        }

        [TestMethod, TestCategory("parser")]
        public void OneClaimEndDot()
        {
            string claim1 = "AUTHOR";
            string claim2 = "grammarware";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.ID_DIV)),
                    X.B(X.D(claim1) + X.One + X.D(claim2))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(1, program.Identifications.Count);
            string k = program.Identifications.Keys.First();
            Assert.AreEqual(claim1, k);
            Assert.AreEqual(claim2, program.Identifications[k]);
            Assert.AreEqual(0, program.Paragraphs.Count);
        }

        [TestMethod, TestCategory("parser")]
        public void OneClaimManyDots()
        {
            string claim1 = "AUTHOR";
            string claim2 = "g.r.a.m.m.a.r.w.a.r.e";
            string input = String.Empty;
            BcProgram program = BackDoor.Parse(
                    X.A(X.D(X.ID_DIV)),
                    X.B(X.D(claim1) + X.D(claim2))
                );
            Assert.IsNotNull(program);
            Assert.AreEqual(1, program.Identifications.Count);
            string k = program.Identifications.Keys.First();
            Assert.AreEqual(claim1, k);
            Assert.AreEqual(claim2, program.Identifications[k]);
            Assert.AreEqual(0, program.Paragraphs.Count);
        }
    }
}