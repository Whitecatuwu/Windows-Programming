using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingShape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using DrawingShapeTests;
using System.ComponentModel;

namespace DrawingShape.Tests
{
    [TestClass()]
    public class ShapeTests
    {
        Process process;
        PrivateObject privateProcess;
        [TestInitialize()]
        public void Initialize()
        {
            process = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "100", "100" });
            privateProcess = new PrivateObject(process);
            MockGraphics mock = new MockGraphics();
            mock.ClearAll();
        }

        [TestMethod()]
        public void ShapeTest()
        {
            string[] testShapeData = new string[] { "ProcessTest", "1", "1", "1", "1" };
            Process test = new Process(ShapeType.PROCESS, testShapeData);

            Assert.AreEqual(test.ShapeType, ShapeType.PROCESS);
            Assert.AreEqual(test.Text, testShapeData[0]);
            Assert.AreEqual(test.X, int.Parse(testShapeData[1]));
            Assert.AreEqual(test.Y, int.Parse(testShapeData[2]));
            Assert.AreEqual(test.Height, int.Parse(testShapeData[3]));
            Assert.AreEqual(test.Width, int.Parse(testShapeData[4]));

            test.X = 1145;
            Assert.AreEqual(test.X, 1145);

            test.Y = 11451;
            Assert.AreEqual(test.Y, 11451);

            test.Height = 114514;
            Assert.AreEqual(test.Height, 114514);

            test.Width = 1145141;
            Assert.AreEqual(test.Width, 1145141);

            test.TextBox_X = 1919;
            Assert.AreEqual(test.TextBox_X, 1919);

            test.TextBox_Y = 861;
            Assert.AreEqual(test.TextBox_Y, 861);

            Assert.AreEqual(process.Id, privateProcess.GetFieldOrProperty("_id"));

            test.Text = "114514";
            Assert.AreEqual(test.Text, "114514");

        }

        [TestMethod()]
        public void DrawFrameTest()
        {
            MockGraphics mock = new MockGraphics();
            process.DrawFrame(mock);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(mock.testDatas[i], process.ShapeDatas[i]);
            }
        }

        [TestMethod()]
        public void DrawTextTest()
        {
            MockGraphics mock = new MockGraphics();
            process.DrawText(mock);
        }

        [TestMethod()]
        public void NormalizeTest()
        {
            Process test = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "-1", "-1", "-100", "-100" });
            test.Normalize();
            Assert.AreEqual(test.X, 0);
            Assert.AreEqual(test.Y, 0);
            Assert.AreEqual(test.Width, 100);
            Assert.AreEqual(test.Height, 100);

            Process test2 = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "100", "100" });
            test2.Normalize();
            Assert.AreEqual(test2.X, 1);
            Assert.AreEqual(test2.Y, 1);
            Assert.AreEqual(test2.Width, 100);
            Assert.AreEqual(test2.Height, 100);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Process process1 = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "100", "100" });
            Process process2 = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "100", "100" });

            List<Shape> shapes = new List<Shape>();
            shapes.Add(process1);

            Assert.AreEqual(process1.CompareTo(shapes[0]), 0);
            Assert.AreEqual(process1.CompareTo(null), 1);
            Assert.AreEqual(process1.CompareTo(process2), -1);

        }

        [TestMethod()]
        public void DrawTextBoxFrameTest()
        {
            MockGraphics mock = new MockGraphics();
            process.DrawTextBoxFrame(mock);
        }

        [TestMethod()]
        public void IsTouchMovePointTest()
        {
            Process process1 = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "1000", "1000" });
            Assert.IsTrue(process1.IsTouchMovePoint(501, 494));
            Assert.IsFalse(process1.IsTouchMovePoint(1, 1));
        }

        [TestMethod()]
        public void DrawTextBoxMovePointTest()
        {
            MockGraphics mock = new MockGraphics();
            process.DrawTextBoxMovePoint(mock);
        }

        [TestMethod()]
        public void DrawConnectionPointsTest()
        {
            MockGraphics mock = new MockGraphics();
            process.DrawConnectionPoints(mock);
            process.Touched = true;
            process.DrawConnectionPoints(mock);
            process.Touched = process.Touched;
        }

        [TestMethod()]
        public void TouchConnectPointTest()
        {
            process = new Process(ShapeType.PROCESS, new string[] { "ProcessTest", "1", "1", "100", "100" });
            process.TouchConnectPoint(1 + 50 - 5, 1 - 5);
            process.TouchConnectPoint(114, 514);
        }
    }
}