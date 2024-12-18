using DrawingModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DrawingShape;
using DrawingShapeTests;
using System.Collections.Generic;
using System.Linq;

namespace DrawingModel.Tests
{
    [TestClass()]
    public class ModelTests
    {
        [TestMethod()]
        public void ModelTest()
        {
            Model model = new Model();
        }

        [TestMethod()]
        public void EnterPointerStateTest()
        {
            Model model = new Model();
            model.EnterPointerState();
        }

        [TestMethod()]
        public void EnterDrawingStateTest()
        {
            Model model = new Model();
            model.EnterDrawingState(ShapeType.START);
        }

        [TestMethod()]
        public void EnterLineStateTest()
        {
            Model model = new Model();
            model.EnterLineState();
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            Model model = new Model();
            model.MouseDown(114, 514);
        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            Model model = new Model();
            model.MouseMove(114, 514);
        }

        [TestMethod()]
        public void MouseUpTest()
        {
            Model model = new Model();
            model.MouseUp(114, 514);
        }

        [TestMethod()]
        public void MouseDoubleClickTest()
        {
            Model model = new Model();
            model.MouseDoubleClick(114, 514);
        }

        [TestMethod()]
        public void OnPaintTest()
        {
            Model model = new Model();
            model.AddShape(new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" }));
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            model.AddLine(line);
            model.OnPaint(new MockGraphics());
        }

        [TestMethod()]
        public void AddShapeTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            Assert.AreEqual(1, model.ShapesSize);
            Assert.AreEqual(model.Shapes[0].Text, "DecisionTest");
        }

        [TestMethod()]
        public void AddShapeTest1()
        {
            Model model = new Model();
            model.AddShape(new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" }));
            Assert.AreEqual(1, model.ShapesSize);
        }

        [TestMethod()]
        public void RemoveShapeTest()
        {
            Model model = new Model();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => model.RemoveShape(114514));
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" }));
            model.RemoveShape(1);
            Assert.AreEqual(model.RemovedShapeIndex, 1);
        }
        [TestMethod()]
        public void AddLineTest()
        {
            Model model = new Model();
            Line line = new Line();
            line.FirstX = 114;
            line.FirstY = 514;
            line.SecondX = 191;
            line.SecondY = 861;
            model.AddLine(line);
            PrivateObject privateObject = new PrivateObject(model);
            List<Line> lines = (List<Line>)privateObject.GetFieldOrProperty("_lines");
            Assert.AreEqual(1, lines.Count());
        }

        [TestMethod()]
        public void RemoveShapeTest1()
        {
            Model model = new Model();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => model.RemoveShape(114514));
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            var shape = new Decision(ShapeType.DECISION, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(shape);
            model.RemoveShape(shape);
            Assert.AreEqual(model.RemovedShapeIndex, 1);
            model.RemoveShape(shape);
            Assert.AreEqual(model.RemovedShapeIndex, 1);
        }

        [TestMethod()]
        public void InsertShapeTest()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            model.InsertShape(1, ShapeType.START, new string[] { "STARTTest", "2", "1", "100", "100" });

            Assert.AreEqual(4, model.ShapesSize);
            Assert.AreEqual(model.Shapes[1].Text, "STARTTest");

            Assert.AreEqual(1, model.InsertedShapeIndex);
        }

        [TestMethod()]
        public void InsertShapeTest1()
        {
            Model model = new Model();
            model.AddShape(ShapeType.START, new string[] { "DecisionTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.PROCESS, new string[] { "PROCESSTest", "1", "1", "100", "100" });
            model.AddShape(ShapeType.TERMINATOR, new string[] { "TERMINATORTest", "1", "1", "100", "100" });
            var shape = new Start(ShapeType.START, new string[] { "STARTTest", "2", "1", "100", "100" });
            model.InsertShape(1, shape);

            Assert.AreEqual(4, model.ShapesSize);
            Assert.AreEqual(model.Shapes[1].Text, "STARTTest");
        }

        [TestMethod()]
        public void ExeCommandTest()
        {
            Assert.Fail();
        }
    }
}