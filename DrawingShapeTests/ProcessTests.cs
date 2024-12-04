using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingShape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawingShapeTests;

namespace DrawingShape.Tests
{
    [TestClass()]
    public class ProcessTests
    {
        Process process = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "100", "100" });
        [TestMethod()]
        public void ProcessTest()
        {
            string[] testShapeData = new string[] { "ProcessTest", "1", "1", "1", "1" };
            Process test = new Process(ShapeType.PROCESS, testShapeData);

            Assert.AreEqual(test.ShapeType, ShapeType.PROCESS);
            Assert.AreEqual(test.Text, testShapeData[0]);
            Assert.AreEqual(test.X, int.Parse(testShapeData[1]));
            Assert.AreEqual(test.Y, int.Parse(testShapeData[2]));
            Assert.AreEqual(test.Height, int.Parse(testShapeData[3]));
            Assert.AreEqual(test.Width, int.Parse(testShapeData[4]));
        }

        [TestMethod()]
        public void DrawTest()
        {
            MockGraphics mock = new MockGraphics();
            process.Draw(mock);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(mock.testDatas[i], process.ShapeDatas[i]);
            }
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {
            Assert.IsFalse(process.IsPointInRange(101, 0));
            Assert.IsFalse(process.IsPointInRange(0, 101));
            Assert.IsFalse(process.IsPointInRange(0, 0));
            Assert.IsFalse(process.IsPointInRange(101, 102));
            Assert.IsFalse(process.IsPointInRange(102, 101));

            Assert.IsTrue(process.IsPointInRange(101, 101));
            Assert.IsTrue(process.IsPointInRange(50, 50));
        }
    }
}