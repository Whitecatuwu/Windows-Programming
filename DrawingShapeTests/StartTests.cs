using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingShape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DrawingShapeTests;
using System.Diagnostics;

namespace DrawingShape.Tests
{
    [TestClass()]
    public class StartTests
    {
        Start start = new Start(ShapeType.START, new string[] { "StartTest", "1", "1", "100", "100" });
        [TestMethod()]
        public void StartTest()
        {
            string[] testShapeData = new string[] { "StartTest", "1", "1", "1", "1" };
            Start test = new Start(ShapeType.START, testShapeData);

            Assert.AreEqual(test.ShapeType, ShapeType.START);
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
            start.Draw(mock);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(mock.testDatas[i], start.ShapeDatas[i]);
            }
        }

        [TestMethod()]
        public void IsPointInRangeTest()
        {
            Assert.IsTrue(start.IsPointInRange(50, 50));
            Assert.IsFalse(start.IsPointInRange(0, 0));
        }
    }
}