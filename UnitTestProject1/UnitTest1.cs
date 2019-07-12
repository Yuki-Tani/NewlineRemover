using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewlineRemover.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RemoveNewlineTest1()
        {
            Sentence sentence = new Sentence("test test, \n test.");
            Console.WriteLine(sentence.ToString());
            string result = sentence.RemoveNewline().ToString();
            Console.WriteLine(result);
            Assert.AreEqual(
                "test test, test.", result);
        }
    }
}
